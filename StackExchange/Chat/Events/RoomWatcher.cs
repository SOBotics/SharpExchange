using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using StackExchange.Auth;
using StackExchange.Net;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events
{
	public class RoomWatcher<T> where T : IWebSocket
	{
		private IAuthenticationProvider auth;

		public string Host { get; private set; }

		public int RoomId { get; private set; }

		public EventRouter EventRouter { get; private set; }
		
		public IWebSocket WebSocket { get; private set; }



		public RoomWatcher(IAuthenticationProvider authProvider, string roomUrl)
		{
			if (authProvider == null)
			{
				throw new ArgumentNullException(nameof(authProvider));
			}

			if (string.IsNullOrEmpty(roomUrl))
			{
				throw new ArgumentException($"'{nameof(roomUrl)}' cannot be null or empty.", nameof(roomUrl));
			}

			roomUrl.GetHostAndIdFromRoomUrl(out var host, out var roomId);

			Initialise(host, roomId);
		}

		public RoomWatcher(IAuthenticationProvider authProvider, string host, int roomId)
		{
			if (authProvider == null)
			{
				throw new ArgumentNullException(nameof(authProvider));
			}

			if (string.IsNullOrEmpty(host))
			{
				throw new ArgumentException($"'{nameof(host)}' cannot be null or empty.");
			}

			if (roomId < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(roomId), $"'{nameof(roomId)}' cannot be negative.");
			}

			Initialise(host, roomId);
		}



		private void Initialise(string host, int roomId)
		{
			Host = host;
			RoomId = roomId;

			WebSocket = GetWebSocket();
			EventRouter = new EventRouter(roomId, WebSocket);
		}

		private IWebSocket GetWebSocket()
		{
			var wsType = typeof(T);
			var url = GetWebSocketUrl();
			var headers = new Dictionary<string, string> { ["Origin"] = $"https://{Host}" };
			var ws = (IWebSocket)Activator.CreateInstance(wsType, url, headers);

			ws.OnReconnectFailed += () =>
			{
				auth.InvalidateHostCache(Host);

				WebSocket = GetWebSocket();

				EventRouter.SetWebSocket(WebSocket);
			};

			ws.Connect();

			return ws;
		}

		private string GetWebSocketUrl()
		{
			var baseUrl = GetBaseWebSocketUrl();
			var eventCount = GetGlobalEventCount();

			return $"{baseUrl}?l={eventCount}";
		}

		private string GetBaseWebSocketUrl()
		{
			var response = new HttpRequest
			{
				Verb = RestSharp.Method.POST,
				Endpoint = $"https://{Host}/ws-auth",
				Cookies = auth[Host],
				Data = new Dictionary<string, object>
				{
					["roomid"] = RoomId,
					["fkey"] = FKeyAccessor.Get($"https://{Host}/rooms/{RoomId}", auth[Host])
				}
			}.Send();

			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				throw new Exception("Failed to get base WebSocket URL.");
			}

			dynamic data = JObject.Parse(response.Content);

			return data.url;
		}

		private int GetGlobalEventCount()
		{
			var response = new HttpRequest
			{
				Verb = RestSharp.Method.POST,
				Endpoint = $"https://{Host}/chats/{RoomId}/events",
				Cookies = auth[Host],
				Data = new Dictionary<string, object>
				{
					["mode"] = "events",
					["msgCount"] = 0,
					["fkey"] = FKeyAccessor.Get($"https://{Host}/rooms/{RoomId}", auth[Host])
				}
			}.Send();

			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				throw new Exception("Failed to get global chat event count.");
			}

			dynamic data = JObject.Parse(response.Content);

			return data.time;
		}
	}
}
