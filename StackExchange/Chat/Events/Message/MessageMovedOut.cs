using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace StackExchange.Chat.Events.Message
{
	public class MessageMovedOut : IChatEventDataProcessor, IChatEventHandler<MovedMessage>
	{
		public EventType Event => EventType.MessageMovedOut;

		public event Action<MovedMessage> OnEvent;

		public void ProcessEventData(JToken data)
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
