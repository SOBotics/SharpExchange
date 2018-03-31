using System;
using StackExchange.Net.WebSocket;

namespace StackExchange.Net.WebSockets
{
	public interface IWebSocket : IDisposable
	{
		event Action OnOpen;
		event Action<string> OnTextMessage;
		event Action<byte[]> OnBinaryMessage;
		event Action OnClose;
		event Action<Exception> OnError;

		void Connect(string endpoint, string origin = null);
		void Send(object message);
		void Send(byte[] message, MessageType messageType);
		void Stop();
	}
}
