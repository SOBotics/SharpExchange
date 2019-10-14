using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SharpExchange.Net.WebSocket;

#pragma warning disable CS4014

namespace SharpExchange.Net.WebSockets
{
	public class DefaultWebSocket : IWebSocket
	{
		private const int bufferSize = 4 * 1024;
		private ClientWebSocket socket;
		private readonly CancellationTokenSource socketTokenSource;
		private bool dispose;

		public event Action OnOpen;
		public event Action<string> OnTextMessage;
		public event Action<byte[]> OnBinaryMessage;
		public event Action OnClose;
		public event Action<Exception> OnError;
		public event Action OnReconnectFailed;

		public Uri Endpoint { get; private set; }
		public IReadOnlyDictionary<string, string> Headers { get; private set; }
		public bool AutoReconnect { get; set; } = true;



		public DefaultWebSocket(string endpoint, Dictionary<string, string> headers = null)
		{
			endpoint.ThrowIfNullOrEmpty(nameof(endpoint));

			socketTokenSource = new CancellationTokenSource();

			Endpoint = new Uri(endpoint);
			Headers = headers;
		}

		~DefaultWebSocket()
		{
			Dispose();
		}



		public void Dispose()
		{
			if (dispose) return;
			dispose = true;

			if (socket?.State == WebSocketState.Open)
			{
				socketTokenSource.Cancel();
			}

			socket.Dispose();

			GC.SuppressFinalize(this);
		}

		public async Task ConnectAsync()
		{
			if (dispose) return;

			if (socket?.State == WebSocketState.Open || socket?.State == WebSocketState.Connecting)
			{
				throw new Exception("WebSocket is already open/connecting.");
			}

			socket = new ClientWebSocket();

			if (Headers != null)
			{
				foreach (var kv in Headers)
				{
					socket.Options.SetRequestHeader(kv.Key, kv.Value);
				}
			}

			await socket.ConnectAsync(Endpoint, socketTokenSource.Token);

			_ = Task.Run(() => Listen());
			_ = OnOpen.InvokeAsync();
		}

		public async Task SendAsync(string message)
		{
			if (dispose) return;

			if (socket?.State != WebSocketState.Open)
			{
				throw new Exception("The WebSocket must be open before attempting to send a message.");
			}

			message.ThrowIfNullOrEmpty(nameof(message));

			var bytes = Encoding.UTF8.GetBytes(message);

			await SendAsync(bytes, MessageType.Text);
		}

		public async Task SendAsync(byte[] bytes, MessageType messageType)
		{
			if (dispose) return;

			if (socket?.State != WebSocketState.Open)
			{
				throw new Exception("The WebSocket must be open before attempting to send a message.");
			}

			bytes.ThrowIfNull(nameof(bytes));

			var bytesSegment = new ArraySegment<byte>(bytes);
			var mType = (WebSocketMessageType)(int)messageType;

			await socket.SendAsync(bytesSegment, mType, true, socketTokenSource.Token);
		}



		private void Listen()
		{
			while (!dispose)
			{
				var buffers = new List<byte[]>();
				WebSocketReceiveResult msgInfo = null;

				try
				{
					while (!msgInfo?.EndOfMessage ?? true)
					{
						var b = new ArraySegment<byte>(new byte[bufferSize]);

						msgInfo = socket.ReceiveAsync(b, socketTokenSource.Token).Result;

						var bArray = b.Array;

						Array.Resize(ref bArray, msgInfo.Count);

						buffers.Add(bArray);
					}
				}
				catch (AggregateException ex)
				when (ex.InnerException?.GetType() == typeof(TaskCanceledException))
				{
					_ = OnClose.InvokeAsync();

					return;
				}
				catch (Exception e1)
				{
					OnError?.Invoke(e1);

					if (!AutoReconnect) return;

					try
					{
						_ = socketTokenSource.Token.WaitHandle.WaitOne(1000);

						ConnectAsync().Wait();
					}
					catch (Exception e2)
					{
						_ = OnReconnectFailed.InvokeAsync();
						_ = OnError.InvokeAsync(e2);
						_ = OnClose.InvokeAsync();
					}

					return;
				}

				var buffer = new List<byte>();

				foreach (var b in buffers)
				{
					buffer.AddRange(b);
				}

				_ = Task.Run(() => HandleNewMessage(msgInfo, buffer.ToArray()));
			}

			_ = OnClose.InvokeAsync();
		}

		private void HandleNewMessage(WebSocketReceiveResult msgInfo, byte[] buffer)
		{
			if (msgInfo == null) return;

			try
			{
				if (msgInfo.MessageType == WebSocketMessageType.Text)
				{
					var text = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

					OnTextMessage?.Invoke(text);
				}
				else if (msgInfo.MessageType == WebSocketMessageType.Binary)
				{
					OnBinaryMessage?.Invoke(buffer);
				}
			}
			catch (Exception ex)
			{
				OnError?.Invoke(ex);
			}
		}
	}
}
