using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharpExchange.Api.V22.Types;

#pragma warning disable CS4014

namespace SharpExchange.Api.V22
{
	internal static partial class ApiRequestScheduler
	{
		private class MethodSheduler
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

			private const string backoffField = "backoff";
			private static readonly ManualResetEvent queueMre = new ManualResetEvent(false);
			private readonly Queue<QueuedRequest> reqs;
			private bool dispose;



			public MethodSheduler()
			{
				reqs = new Queue<QueuedRequest>();

				_ = Task.Run(() => QueueProcessorLoop());
			}

			~MethodSheduler()
			{
				Dispose();
			}



			public void Dispose()
			{
				if (dispose) return;
				dispose = true;

				reqs.Clear();
				_ = queueMre.Set();
				queueMre.Dispose();

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

					_ = mre.Set();
				});

				reqs.Enqueue(new QueuedRequest(callback, fullUrl));

				_ = queueMre.Set();

				_ = await Task.Run(() => mre.WaitOne(Timeout));

				return result;
			}



			private void QueueProcessorLoop()
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
					var json = MasterSheduler.Schedule(req.Url);

					_ = req.Callback.InvokeAsync(json);

					var backoff = 0;

					try
					{
						backoff = JObject.Parse(json).Value<int?>(backoffField) ?? 0;
					}
					catch
					{
						//TODO: Log this somewhere.
					}

					if (backoff != 0)
					{
						_ = mre.WaitOne(backoff * 1000);
					}
				}
			}
		}
	}
}
