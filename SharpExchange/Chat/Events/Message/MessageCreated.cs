using System;
using Newtonsoft.Json.Linq;

namespace SharpExchange.Chat.Events.Message
{
	public class MessageCreated : ChatEventDataProcessor, IChatEventHandler<int>
	{
		private EventType[] eventType = new[] { EventType.MessagePosted };

		public override EventType[] Events => eventType;

		public event Action<int> OnEvent;

		public override void ProcessEventData(EventType _, JToken data)
		{
			var msgId = data.Value<int>("message_id");

			OnEvent?.Invoke(msgId);
		}
	}
}
