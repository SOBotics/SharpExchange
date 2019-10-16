using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json.Linq;
using SharpExchange.Auth;
using SharpExchange.Net;
using SharpExchange.Net.WebSockets;

namespace SharpExchange.Chat.Events
{
	public class RoomWatcher<T> : IDisposable where T : IWebSocket
	{
		private bool dispose;
		private DateTime lastReconnectFailure;
		private int reconnectWait;

		public IAuthenticationProvider Auth { get; private set; }
		public string Host { get; private set; }
		public int RoomId { get; private set; }
		public EventRouter EventRouter { get; private set; }
		public IWebSocket WebSocket { get; private set; }



		public RoomWatcher(IAuthenticationProvider authProvider, string roomUrl)
		{
			authProvider.ThrowIfNull(nameof(authProvider));
			roomUrl.ThrowIfNullOrEmpty(nameof(roomUrl));

			Auth = authProvider;

			roomUrl.GetHostAndIdFromRoomUrl(out var host, out var roomId);

			Initialise(host, roomId);
		}

		public RoomWatcher(IAuthenticationProvider authProvider, string host, int roomId)
		{
			authProvider.ThrowIfNull(nameof(authProvider));
			host.ThrowIfNullOrEmpty(nameof(host));

			if (roomId < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(roomId), $"'{nameof(roomId)}' cannot be negative.");
			}

			Auth = authProvider;

			Initialise(host, roomId);
		}

		~RoomWatcher()
		{
			Dispose();
		}



		public void Dispose()
		{
			if (dispose) return;
			dispose = true;

			EventRouter?.Dispose();
			WebSocket?.Dispose();

			GC.SuppressFinalize(this);
		}



		private void Initialise(string host, int roomId)
		{
			Host = host.GetChatHost();
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

			ws.OnReconnectFailed += HandleReconnectFailure;

			ws.ConnectAsync().Wait();

			return ws;
		}

		private void HandleReconnectFailure()
		{
			if (dispose) return;

			if ((DateTime.UtcNow - lastReconnectFailure).TotalSeconds < reconnectWait * 2)
			{
				reconnectWait += 10;

				reconnectWait = Math.Min(reconnectWait, 100);
			}
			else
			{
				reconnectWait = 10;
			}

			lastReconnectFailure = DateTime.UtcNow;

			Thread.Sleep(reconnectWait * 1000);

			FKeyAccessor.ClearCache();
			Auth.InvalidateHostCache(Host);

			WebSocket = GetWebSocket();

			EventRouter.SetWebSocket(WebSocket);
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
				Cookies = Auth[Host],
				Data = new Dictionary<string, object>
				{
					["roomid"] = RoomId,
					["fkey"] = FKeyAccessor.GetAsync($"https://{Host}/rooms/{RoomId}", Auth[Host]).Result
				}
			}.SendAsync().Result;

			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				throw new Exception("Failed to get base WebSocket URL.");
			}

			var data = JObject.Parse(response.Content);

			return data.Value<string>("url");
		}

		private int GetGlobalEventCount()
		{
			var response = new HttpRequest
			{
				Verb = RestSharp.Method.POST,
				Endpoint = $"https://{Host}/chats/{RoomId}/events",
				Cookies = Auth[Host],
				Data = new Dictionary<string, object>
				{
					["mode"] = "events",
					["msgCount"] = 0,
					["fkey"] = FKeyAccessor.GetAsync($"https://{Host}/rooms/{RoomId}", Auth[Host]).Result
				}
			}.SendAsync().Result;

			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				throw new Exception("Failed to get global chat event count.");
			}

			var data = JObject.Parse(response.Content);

			return data.Value<int>("time");
		}
	}
}
