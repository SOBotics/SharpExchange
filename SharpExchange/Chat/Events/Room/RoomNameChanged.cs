using System;
using Newtonsoft.Json.Linq;

namespace SharpExchange.Chat.Events.Room
{
	public class RoomNameChanged : ChatEventDataProcessor, IChatEventHandler
	{
		private EventType[] eventType = new[] { EventType.RoomNameChanged  };

		public override EventType[] Events => eventType;

		public event Action OnEvent;

		public override void ProcessEventData(EventType _, JToken data)
		{
			OnEvent?.Invoke();
		}
	}
}
