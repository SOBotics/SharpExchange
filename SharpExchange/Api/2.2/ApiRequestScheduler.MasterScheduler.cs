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

			private static readonly TimeSpan waitTime = TimeSpan.FromSeconds(1.0 / 30);
			private static readonly ManualResetEvent queueMre = new ManualResetEvent(false);
			private static readonly Queue<QueuedRequest> reqs;
			private static bool dispose;




			static MasterSheduler()
			{
				reqs = new Queue<QueuedRequest>();

				_ = Task.Run(() => QueueProcessorLoop());
			}



			public static void Dispose()
			{
				if (dispose) return;
				dispose = true;

				reqs.Clear();
				_ = queueMre.Set();
				queueMre.Dispose();
			}

			public static string Schedule(string endpoint)
			{
				var mre = new ManualResetEvent(false);
				var result = "";

				var callback = new Action<string>(x =>
				{
					result = x;

					_ = mre.Set();
				});

				reqs.Enqueue(new QueuedRequest(callback, endpoint));

				_ = queueMre.Set();

				_ = mre.WaitOne(Timeout);

				return result;
			}



			private static void QueueProcessorLoop()
			{
				var mre = new ManualResetEvent(false);

				while (!dispose)
				{
					if (reqs.Count == 0)
					{
						_ = queueMre.Reset();
						_ = queueMre.WaitOne();

						if (dispose)
						{
							return;
						}
					}

					var req = reqs.Dequeue();
					var json = HttpRequest.GetAsync(req.Url).Result;

					_ = req.Callback.InvokeAsync(json);
					_ = mre.WaitOne(waitTime);
				}
			}
		}
	}
}
