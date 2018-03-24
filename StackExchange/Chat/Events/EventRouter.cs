using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StackExchange.Net;

namespace StackExchange.Chat.Events
{
	public class EventRouter : IDisposable
	{
		private bool dispose;

		public IWebSocket WebSocket { get; private set; }

		public List<IChatEventDataProcessor> EventProcessors { get; private set; }



		public EventRouter()
		{
			//WebSocket = ??? get default websocket implementation
			EventProcessors = new List<IChatEventDataProcessor>();
		}

		public EventRouter(IWebSocket webSocket, IEnumerable<IChatEventDataProcessor> eventProcessors = null)
		{
			WebSocket = webSocket;

			if (eventProcessors == null)
			{
				EventProcessors = new List<IChatEventDataProcessor>();
			}
			else
			{
				EventProcessors = new List<IChatEventDataProcessor>(eventProcessors);
			}
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
	}
}