using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Api.V22.Types;
using StackExchange.Net;

#pragma warning disable CS4014

namespace StackExchange.Api.V22
{
	public static partial class ApiRequestScheduler
	{
		private class Sheduler
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

			private const double minBackoff = 0.5;
			private const string backoffField = "backoff";
			private readonly Queue<QueuedRequest> reqs;
			private bool dispose;



			public Sheduler()
			{
				reqs = new Queue<QueuedRequest>();

				Task.Run(() => QueueProcessorLoop());
			}

			~Sheduler()
			{
				Dispose();
			}



			public void Dispose()
			{
				if (dispose) return;
				dispose = true;

				reqs.Clear();

				GC.SuppressFinalize(this);
			}

			public async Task<Result<T>> ScheduleAsync<T>(string fullUrl)
			{
				var mre = new ManualResetEvent(false);
				Result<T> result = null;

				var callback = new Action<string>(x =>
				{
					try
					{
						result = JsonConvert.DeserializeObject<Result<T>>(x);
					}
					catch
					{
						//TODO: Log this somewhere.
					}

					mre.Set();
				});

				reqs.Enqueue(new QueuedRequest(callback, fullUrl));

				await Task.Run(() => mre.WaitOne(Timeout));

				return result;
			}



			private void QueueProcessorLoop()
			{
				var mre = new ManualResetEvent(false);
				var backoff = minBackoff;

				while (!dispose)
				{
					mre.WaitOne(TimeSpan.FromSeconds(backoff));

					if (reqs.Count == 0) continue;

					var req = reqs.Dequeue();
					var json = HttpRequest.Get(req.Url);
					var responseBackoff = 0.0;

					try
					{
						responseBackoff = JObject.Parse(json).Value<int?>(backoffField) ?? 0;
					}
					catch
					{
						//TODO: Log this somewhere.
					}

					backoff = Math.Max(minBackoff, responseBackoff);

					req.Callback.InvokeAsync(json);
				}
			}
		}
	}
}
