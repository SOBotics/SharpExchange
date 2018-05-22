using System;
using Newtonsoft.Json.Linq;

namespace SharpExchange.Chat.Events.Room
{
	public class RoomNameChanged : ChatEventDataProcessor, IChatEventHandler
	{
		public override EventType Event => EventType.RoomNameChanged;

		public event Action OnEvent;

		public override void ProcessEventData(JToken data)
		{
			OnEvent?.Invoke();
		}
	}
}
