using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace StackExchange.Chat.Events.Room
{
	public class RoomNameChanged : IChatEventDataProcessor, IChatEventHandler
	{
		public EventType Event => EventType.RoomNameChanged;

		public event Action OnEvent;

		public void ProcessEventData(JToken data)
		{
			OnEvent?.Invoke();
		}
	}
}
