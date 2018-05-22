using System;
using Newtonsoft.Json.Linq;

namespace SharpExchange.Chat.Events.Message
{
	public class MessageMovedIn : ChatEventDataProcessor, IChatEventHandler<MovedMessage>
	{
		public override EventType Event => EventType.MessageMovedIn;

		public event Action<MovedMessage> OnEvent;

		public override void ProcessEventData(JToken data)
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
