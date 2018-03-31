using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events
{
	public class EventRouter : IDisposable
	{
		private bool dispose;

		public int RoomId { get; private set; }

		public IWebSocket WebSocket { get; private set; }

		public List<IChatEventDataProcessor> EventProcessors { get; private set; }



		public EventRouter(int roomId, IWebSocket webSocket, IEnumerable<IChatEventDataProcessor> eventProcessors = null)
		{
			if (roomId < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(roomId), $"'{nameof(roomId)}' cannot be negative.");
			}

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

			RoomId = roomId;

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
			var path = $"r{RoomId}.e";
			var roomEvents = JObject.Parse(json).SelectToken(path);

			if (roomEvents == null) return;

			foreach (var ev in roomEvents)
			{
				var eventType = ev.Value<int>("event_type");

				foreach (var processor in EventProcessors)
				{
					if (processor.Event != EventType.All && processor.Event != (EventType)eventType)
					{
						continue;
					}

					Task.Run(() => processor.ProcessEventData(ev));
				}
			}

		}
	}
}