using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace StackExchange.Net.WebSockets
{
	public enum MessageType
	{
		Text,
		Binary,
		Close
	}

	public interface IWebSocket : IDisposable
	{
		string Endpoint { get; set; }
		string Origin { get; set; }

		event Action OnOpen;
		event Action<string> OnTextMessage;
		event Action<byte[]> OnBinaryMessage;
		event Action OnClose;
		event Action<Exception> OnError;

		void Connect();
		void Send(object message);
		void Send(byte[] message, MessageType messageType);
		void Stop();
	}
}
