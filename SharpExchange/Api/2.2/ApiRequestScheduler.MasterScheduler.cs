using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SharpExchange.Net;

#pragma warning disable CS4014

namespace SharpExchange.Api.V22
{
	internal static partial class ApiRequestScheduler
	{
		private static class MasterSheduler
		{
			private class QueuedRequest
			{
				public Action<string> Callback { get; private set; }
				public string Url { get; private set; }

				public QueuedRequest(Action<string> c, string u)
				{
					Callback = c;
					Url = u;
				}
			}

			private static readonly TimeSpan waitTime = TimeSpan.FromSeconds(0.5);
			private static readonly ManualResetEvent queueMre = new ManualResetEvent(false);
			private static readonly Queue<QueuedRequest> reqs;
			private static bool dispose;




			static MasterSheduler()
			{
				reqs = new Queue<QueuedRequest>();

				Task.Run(() => QueueProcessorLoop());
			}



			public static void Dispose()
			{
				if (dispose) return;
				dispose = true;

				reqs.Clear();
				queueMre.Set();
				queueMre.Dispose();
			}

			public static string Schedule(string endpoint)
			{
				var mre = new ManualResetEvent(false);
				var result = "";

				var callback = new Action<string>(x =>
				{
					result = x;

					mre.Set();
				});

				reqs.Enqueue(new QueuedRequest(callback, endpoint));

				queueMre.Set();

				mre.WaitOne(Timeout);

				return result;
			}



			private static void QueueProcessorLoop()
			{
				var mre = new ManualResetEvent(false);

				while (!dispose)
				{
					if (reqs.Count == 0)
					{
						queueMre.Reset();
						queueMre.WaitOne();

						if (dispose)
						{
							return;
						}
					}

					var req = reqs.Dequeue();
					var json = HttpRequest.GetAsync(req.Url).Result;

					req.Callback.InvokeAsync(json);
					mre.WaitOne(waitTime);
				}
			}
		}
	}
}
