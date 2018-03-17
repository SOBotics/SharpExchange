using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using StackExchange.Auth;
using StackExchange.Net;

namespace StackExchange.Chat.Actions
{
	public class ActionScheduler : IDisposable
	{
		private readonly ManualResetEvent queueMre;
		private readonly Queue<ChatAction> actionQueue;
		private bool dispose;

		public string Host { get; private set; }

		public int RoomId { get; private set; }

		public List<Cookie> AuthCookies { get; private set; }



		public ActionScheduler(string roomUrl, IEnumerable<Cookie> authenticationCookies)
		{
			if (string.IsNullOrEmpty(roomUrl))
			{
				throw new ArgumentException($"'{nameof(roomUrl)}' cannot be null or empty.");
			}

			if (authenticationCookies == null)
			{
				throw new ArgumentNullException(nameof(authenticationCookies));
			}

			if (authenticationCookies.Count() == 0)
			{
				throw new ArgumentException($"'{nameof(authenticationCookies)}' cannot be empty.", nameof(authenticationCookies));
			}

			roomUrl.GetHostAndId(out var host, out var id);

			Host = host;
			RoomId = id;
			AuthCookies = new List<Cookie>(authenticationCookies);

			queueMre = new ManualResetEvent(false);
			actionQueue = new Queue<ChatAction>();

			Task.Run(new Action(ProcessQueue));
		}

		public ActionScheduler(string host, int roomId, IEnumerable<Cookie> authenticationCookies)
		{
			if (string.IsNullOrEmpty(host))
			{
				throw new ArgumentException($"'{nameof(host)}' cannot be null or empty.");
			}

			if (roomId < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(roomId), $"'{nameof(roomId)}' cannot be negative.");
			}

			if (authenticationCookies == null)
			{
				throw new ArgumentNullException(nameof(authenticationCookies));
			}

			if (authenticationCookies.Count() == 0)
			{
				throw new ArgumentException($"'{nameof(authenticationCookies)}' cannot be empty.", nameof(authenticationCookies));
			}

			Host = host;
			RoomId = roomId;
			AuthCookies = new List<Cookie>(authenticationCookies);

			queueMre = new ManualResetEvent(false);
			actionQueue = new Queue<ChatAction>();

			Task.Run(new Action(ProcessQueue));
		}

		~ActionScheduler()
		{
			Dispose();
		}



		public void Dispose()
		{
			if (dispose) return;
			dispose = true;

			queueMre.Set();
			queueMre.Dispose();
			actionQueue.Clear();

			GC.SuppressFinalize(this);
		}

		public object ScheduleAction(ChatAction act) => ScheduleAction(act, Timeout.InfiniteTimeSpan);

		// Perform permissions check before scheduling action for execution.
		public object ScheduleAction(ChatAction act, TimeSpan timeout)
		{
			if (act == null)
			{
				throw new ArgumentNullException(nameof(act));
			}

			var wait = new AutoResetEvent(false);
			object data = null;

			act.CallBack = new Action<object>(x =>
			{
				data = x;
				wait.Set();
			});

			actionQueue.Enqueue(act);

			queueMre.Set();
			wait.WaitOne(timeout);

			return data;
		}



		private void ProcessQueue()
		{
			while (!dispose)
			{
				if (actionQueue.Count == 0)
				{
					queueMre.Reset();
				}

				queueMre.WaitOne();

				var act = actionQueue.Dequeue();
				act.Host = Host;
				act.RoomId = RoomId;

				var response = GetResponse(act);
				var data = act.ProcessResponse(response);

				act.CallBack?.Invoke(data);
			}
		}

		private HttpRequest GetRequest(ChatAction act)
		{
			var data = act.Data;
			var cm = new CookieManager();

			if (act.RequiresFKey)
			{
				if (data == null)
				{
					data = new Dictionary<string, object>
					{
						["fkey"] = FKeyAccessor.Get()
					};
				}
				else
				{
					data["fkey"] = FKeyAccessor.Get();
				}
			}

			if (act.RequiresAuthCookies)
			{
				cm.Add(AuthCookies);
			}

			return new HttpRequest
			{
				Verb = act.RequestMethod,
				Endpoint = act.Endpoint,
				Cookies = cm,
				Data = data
			};
		}

		private string GetResponse(ChatAction act)
		{
			RestResponse response = null;

			while (!dispose)
			{
				response = GetRequest(act).Send();

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

			return response?.Content;
		}
	}
}
