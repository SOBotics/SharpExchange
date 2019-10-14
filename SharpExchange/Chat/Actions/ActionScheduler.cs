using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using SharpExchange.Auth;
using SharpExchange.Net;

namespace SharpExchange.Chat.Actions
{
	public class ActionScheduler : IDisposable
	{
		private readonly IAuthenticationProvider auth;
		private readonly ManualResetEvent queueMre;
		private readonly Queue<ChatAction> actionQueue;
		private bool dispose;

		public string Host { get; private set; }

		public int RoomId { get; private set; }



		public ActionScheduler(IAuthenticationProvider authProvider, string roomUrl)
		{
			authProvider.ThrowIfNull(nameof(authProvider));
			roomUrl.ThrowIfNullOrEmpty(nameof(roomUrl));

			auth = authProvider;
			queueMre = new ManualResetEvent(false);
			actionQueue = new Queue<ChatAction>();

			roomUrl.GetHostAndIdFromRoomUrl(out var host, out var id);

			Host = host.GetChatHost();
			RoomId = id;

			_ = Task.Run(new Action(ProcessQueue));
		}

		public ActionScheduler(IAuthenticationProvider authProvider, string host, int roomId)
		{
			authProvider.ThrowIfNull(nameof(authProvider));
			host.ThrowIfNullOrEmpty(nameof(host));

			if (roomId < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(roomId), $"'{nameof(roomId)}' cannot be negative.");
			}

			auth = authProvider;
			queueMre = new ManualResetEvent(false);
			actionQueue = new Queue<ChatAction>();

			Host = host.GetChatHost();
			RoomId = roomId;

			_ = Task.Run(new Action(ProcessQueue));
		}

		~ActionScheduler()
		{
			Dispose();
		}



		public void Dispose()
		{
			if (dispose) return;
			dispose = true;

			_ = queueMre.Set();
			queueMre.Dispose();
			actionQueue.Clear();

			GC.SuppressFinalize(this);
		}

		public Task<T> ScheduleActionAsync<T>(ChatAction act)
		{
			return ScheduleActionAsync<T>(act, Timeout.InfiniteTimeSpan);
		}

		public async Task<T> ScheduleActionAsync<T>(ChatAction act, TimeSpan timeout)
		{
			act.ThrowIfNull(nameof(act));

			var wait = new AutoResetEvent(false);
			object data = null;

			act.CallBack = new Action<object>(x =>
			{
				data = x;
				_ = wait.Set();
			});

			actionQueue.Enqueue(act);

			_ = queueMre.Set();

			_ = await Task.Run(() => wait.WaitOne(timeout));

			return (T)data;
		}



		private void ProcessQueue()
		{
			while (!dispose)
			{
				if (actionQueue.Count == 0)
				{
					_ = queueMre.Reset();
				}

				if (queueMre.WaitOne() && dispose)
				{
					break;
				}

				var act = actionQueue.Dequeue();
				act.Host = Host;
				act.RoomId = RoomId;

				var response = GetResponse(act);
				var data = act.ProcessResponse(response.StatusCode, response.Content);

				act.CallBack?.Invoke(data);
			}
		}

		private HttpRequest GetRequest(ChatAction act)
		{
			var data = act.Data;

			if (act.RequiresFKey)
			{
				var roomUrl = $"https://{Host}/rooms/{RoomId}";

				if (data == null)
				{
					data = new Dictionary<string, object>
					{
						["fkey"] = FKeyAccessor.GetAsync(roomUrl, auth[Host]).Result
					};
				}
				else
				{
					data["fkey"] = FKeyAccessor.GetAsync(roomUrl, auth[Host]).Result;
				}
			}

			var req = new HttpRequest
			{
				Verb = act.RequestMethod,
				Endpoint = act.Endpoint,
				Data = data
			};

			if (act.RequiresAuthCookies)
			{
				req.Cookies = auth[Host];
			}

			return req;
		}

		private RestResponse GetResponse(ChatAction act)
		{
			RestResponse response = null;

			while (!dispose)
			{
				response = GetRequest(act).SendAsync().Result;

				if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
				{
					var waitStr = new string(response.Content.Where(char.IsDigit).ToArray());

					if (int.TryParse(waitStr, out var wait))
					{
						Thread.Sleep(wait * 1000);
					}
					else
					{
						// If we can't find out how long we need to wait for whatever reason,
						// fallback to the maximum wait time of 20 seconds.
						Thread.Sleep(20000);
					}
				}
				else
				{
					break;
				}
			}

			return response;
		}
	}
}
