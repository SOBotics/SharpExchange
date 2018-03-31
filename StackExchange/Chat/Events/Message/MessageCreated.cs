using System;
using Newtonsoft.Json.Linq;

namespace StackExchange.Chat.Events.Message
{
	public class MessageCreated : IChatEventDataProcessor, IChatEventHandler<int>
	{
		public EventType Event => EventType.MessagePosted;

		public event Action<int> OnEvent;

		public void ProcessEventData(JToken data)
		{
			var msgId = data.Value<int>("message_id");

			OnEvent?.Invoke(msgId);
		}
	}
}
