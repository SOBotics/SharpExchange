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

		public List<ChatEvent> Events { get; private set; }



		public EventRouter()
		{
			//WebSocket = ??? get default websocket implementation
			Events = GetAllStandardEvents();
		}

		public EventRouter(IEnumerable<ChatEvent> events)
		{
			Events = new List<ChatEvent>(events);
		}

		public EventRouter(IWebSocket webSocket)
		{
			WebSocket = webSocket;
		}

		public EventRouter(IWebSocket webSocket, IEnumerable<ChatEvent> events)
		{
			WebSocket = webSocket;
			Events = new List<ChatEvent>(events);
		}

		~EventRouter()
		{
			Dispose();
		}



		public static List<ChatEvent> GetAllStandardEvents() =>
			typeof(EventRouter).Assembly
				.GetTypes()
				.Where(x => x.IsClass)
				.Where(x => x.IsPublic)
				.Where(x => !x.IsAbstract)
				.Where(x => x.IsSubclassOf(typeof(ChatEvent)))
				.Select(Activator.CreateInstance)
				.Cast<ChatEvent>()
				.ToList();

		public void Dispose()
		{
			if (dispose) return;
			dispose = true;

			Events?.Clear();
			WebSocket?.Dispose();

			GC.SuppressFinalize(this);
		}
	}
}