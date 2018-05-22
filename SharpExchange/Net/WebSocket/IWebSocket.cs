using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharpExchange.Net.WebSocket;

namespace SharpExchange.Net.WebSockets
{
	public interface IWebSocket : IDisposable
	{
		event Action OnOpen;
		event Action<string> OnTextMessage;
		event Action<byte[]> OnBinaryMessage;
		event Action OnClose;
		event Action<Exception> OnError;
		event Action OnReconnectFailed;

		Uri Endpoint { get; }
		IReadOnlyDictionary<string, string> Headers { get; }
		bool AutoReconnect { get; set; }

		Task ConnectAsync();
		Task SendAsync(string message);
		Task SendAsync(byte[] message, MessageType messageType);
	}
}
