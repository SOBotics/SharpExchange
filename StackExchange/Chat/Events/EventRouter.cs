using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StackExchange.Net;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events
{
	public class EventRouter : IDisposable
	{
		private bool dispose;

		public IWebSocket WebSocket { get; private set; }

		public List<IChatEventDataProcessor> EventProcessors { get; private set; }



		public EventRouter(IWebSocket webSocket, IEnumerable<IChatEventDataProcessor> eventProcessors = null)
		{
			if (webSocket == null)
			{
				throw new ArgumentNullException(nameof(webSocket));
			}

			if (eventProcessors == null)
			{
				EventProcessors = new List<IChatEventDataProcessor>();
			}
			else
			{
				EventProcessors = new List<IChatEventDataProcessor>(eventProcessors);
			}

			WebSocket = webSocket;

			WebSocket.OnTextMessage += HandleNewMessage;
		}

		~EventRouter()
		{
			Dispose();
		}



		public void Dispose()
		{
			if (dispose) return;
			dispose = true;

			EventProcessors?.Clear();
			WebSocket?.Dispose();

			GC.SuppressFinalize(this);
		}


		private void HandleNewMessage(string json)
		{
			foreach (var processor in EventProcessors)
			{
				//TODO: Only process events from the current room.
				//TODO: Only invoke processors that match their respective event.

				processor.ProcessEventData(json);
			}
		}
	}
}