using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events
{
	public sealed class EventRouter : IDisposable
	{
		private List<ChatEventDataProcessor> processors;
		private bool dispose;

		public int RoomId { get; private set; }

		public IWebSocket WebSocket { get; private set; }

		public ReadOnlyCollection<ChatEventDataProcessor> EventProcessors =>
			new ReadOnlyCollection<ChatEventDataProcessor>(processors);



		internal EventRouter(int roomId, IWebSocket webSocket)
		{
			if (roomId < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(roomId), $"'{nameof(roomId)}' cannot be negative.");
			}

			if (webSocket == null)
			{
				throw new ArgumentNullException(nameof(webSocket));
			}

			processors = new List<ChatEventDataProcessor>();

			RoomId = roomId;

			SetWebSocket(webSocket);
		}

		~EventRouter()
		{
			Dispose();
		}



		public void Dispose()
		{
			if (dispose) return;
			dispose = true;

			processors?.Clear();
			WebSocket?.Dispose();

			GC.SuppressFinalize(this);
		}

		public void AddProcessor(ChatEventDataProcessor p)
		{
			if (p == null)
			{
				throw new ArgumentNullException(nameof(p));
			}

			p.RoomId = RoomId;

			processors.Add(p);
		}

		public bool RemoveProcessor(ChatEventDataProcessor p)
		{
			if (p == null)
			{
				throw new ArgumentNullException(nameof(p));
			}

			return processors.Remove(p);
		}



		internal void SetWebSocket(IWebSocket ws)
		{
			WebSocket = ws;
			WebSocket.OnTextMessage += HandleNewMessage;
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