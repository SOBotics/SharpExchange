using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Net.WebSocket;

namespace StackExchange.Net.WebSockets
{
	public class DefaultWebSocket : IWebSocket
	{
		private const int bufferSize = 4 * 1024;
		private ClientWebSocket socket;
		private CancellationTokenSource socketTokenSource;
		private bool dispose;

		public event Action OnOpen;
		public event Action<string> OnTextMessage;
		public event Action<byte[]> OnBinaryMessage;
		public event Action OnClose;
		public event Action<Exception> OnError;



		public DefaultWebSocket()
		{
			socketTokenSource = new CancellationTokenSource();

			socket = new ClientWebSocket();
		}

		~DefaultWebSocket()
		{
			Dispose();
		}



		public void Dispose()
		{
			if (dispose) return;
			dispose = true;

			Stop();
			socket.Dispose();

			GC.SuppressFinalize(this);
		}

		public void Connect(string endpoint, string origin = null)
		{
			if (string.IsNullOrEmpty(endpoint))
			{
				throw new ArgumentException($"'{nameof(endpoint)}' cannot be null or empty.");
			}

			if (socket.State == WebSocketState.Open || socket.State == WebSocketState.Connecting)
			{
				throw new Exception("WebSocket is already open/connecting.");
			}

			if (!string.IsNullOrEmpty(origin))
			{
				socket.Options.SetRequestHeader("Origin", origin);
			}

			socket.ConnectAsync(new Uri(endpoint), socketTokenSource.Token).Wait();

			Task.Run(() => Listen());

			OnOpen?.Invoke();
		}

		public void Send(object message)
		{
			if (socket?.State != WebSocketState.Open)
			{
				throw new Exception("The WebSocket must be open before attempting to send a message.");
			}

			if (message == null)
			{
				throw new ArgumentNullException(nameof(message));
			}

			var messageText = message.ToString();

			if (string.IsNullOrEmpty(messageText))
			{
				throw new Exception($"The returned value from '{nameof(message)}.ToString()' cannot be null or empty.");
			}

			var bytes = Encoding.UTF8.GetBytes(messageText);

			Send(bytes, MessageType.Text);
		}

		public void Send(byte[] bytes, MessageType messageType)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException(nameof(bytes));
			}

			var bytesSegment = new ArraySegment<byte>(bytes);
			var mType = (WebSocketMessageType)(int)messageType;

			socket.SendAsync(bytesSegment, mType, true, socketTokenSource.Token).Wait();
		}

		public void Stop()
		{
			if (socket?.State != WebSocketState.Open)
			{
				return;
			}

			try
			{
				socketTokenSource.Cancel();
			}
			catch
			{
				// We don't care about any exceptions being raised
				// as a result of cancelling the running operations.
			}

			OnClose?.Invoke();
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
				catch (AggregateException)
				{
					// During normal operation, this is only
					// ever raised during task cancellation.
				}
				catch (Exception ex)
				{
					OnError?.Invoke(ex);

					// Wait a second before continuing.
					socketTokenSource.Token.WaitHandle.WaitOne(1000);

					continue;
				}

				var buffer = new List<byte>();

				foreach (var b in buffers)
				{
					buffer.AddRange(b);
				}

				Task.Run(() => HandleNewMessage(msgInfo, buffer.ToArray()));
			}
		}

		private void HandleNewMessage(WebSocketReceiveResult msgInfo, byte[] buffer)
		{
			try
			{
				switch (msgInfo.MessageType)
				{
					case WebSocketMessageType.Text:
					{
						var text = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

						OnTextMessage?.Invoke(text);

						break;
					}
					case WebSocketMessageType.Binary:
					{
						OnBinaryMessage?.Invoke(buffer);

						break;
					}
					case WebSocketMessageType.Close:
					{
						Stop();

						break;
					}
				}
			}
			catch (Exception ex)
			{
				OnError?.Invoke(ex);
			}
		}
	}
}
