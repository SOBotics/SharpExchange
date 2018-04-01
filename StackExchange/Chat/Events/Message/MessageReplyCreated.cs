using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace StackExchange.Chat.Events.Message
{
	public class MessageReply
	{
		public int AuthorId { get; internal set; }
		public int MessageId { get; internal set; }
		public int TargetMessageId { get; internal set; }
	}

	public class MessageReplyCreated : IChatEventDataProcessor, IChatEventHandler<MessageReply>
	{
		public EventType Event => EventType.MessageReply;

		public event Action<MessageReply> OnEvent;

		public void ProcessEventData(JToken data)
		{
			var msgId = data.Value<int>("message_id");
			var targetMsgId = data.Value<int>("parent_id");
			var msgAuthor = data.Value<int>("user_id");

			OnEvent?.Invoke(new MessageReply
			{
				AuthorId = msgAuthor,
				MessageId = msgId,
				TargetMessageId = targetMsgId
			});
		}
	}
}
