using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SharpExchange.Net.WebSockets;

namespace SharpExchange.Chat.Events
{
	public sealed class EventRouter : IDisposable
	{
		private readonly List<ChatEventDataProcessor> processors;
		private bool dispose;

		public int RoomId { get; private set; }

		public IWebSocket WebSocket { get; private set; }

		public ReadOnlyCollection<ChatEventDataProcessor> EventProcessors =>
			new ReadOnlyCollection<ChatEventDataProcessor>(processors);



		internal EventRouter(int roomId, IWebSocket ws)
		{
			if (roomId < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(roomId), $"'{nameof(roomId)}' cannot be negative.");
			}

			ws.ThrowIfNull(nameof(ws));

			processors = new List<ChatEventDataProcessor>();

			RoomId = roomId;

			SetWebSocket(ws);
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
			p.ThrowIfNull(nameof(p));
			p.Events.ThrowIfNullOrEmpty(nameof(p.Events));

			p.RoomId = RoomId;

			processors.Add(p);
		}

		public bool RemoveProcessor(ChatEventDataProcessor p)
		{
			p.ThrowIfNull(nameof(p));

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

			foreach (var eventData in roomEvents)
			{
				var eventType = (EventType)eventData.Value<int>("event_type");

				foreach (var processor in EventProcessors)
				{
					if (processor.Events[0] != EventType.All && !processor.Events.Any(x => x == eventType))
					{
						continue;
					}

					_ = Task.Run(() => processor.ProcessEventData(eventType, eventData));
				}
			}
		}
	}
}