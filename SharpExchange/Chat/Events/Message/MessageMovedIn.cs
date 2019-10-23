using System;
using Newtonsoft.Json.Linq;

namespace SharpExchange.Chat.Events.Message
{
	public class MessageMovedIn : ChatEventDataProcessor, IChatEventHandler<MovedMessage>
	{
		private EventType[] eventType = new[] { EventType.MessageMovedIn };

		public override EventType[] Events => eventType;

		public event Action<MovedMessage> OnEvent;

		public override void ProcessEventData(EventType _, JToken data)
		{
			var msgId = data.Value<int>("message_id");
			var movedBy = data.Value<int>("user_id");

			OnEvent?.Invoke(new MovedMessage
			{
				MessageId = msgId,
				MovedBy = movedBy
			});
		}
	}
}
