using System;
using Newtonsoft.Json.Linq;

namespace SharpExchange.Chat.Events.Message
{
	public class MessageReply
	{
		public int AuthorId { get; internal set; }
		public int MessageId { get; internal set; }
		public int TargetMessageId { get; internal set; }
	}

	public class MessageReplyCreated : ChatEventDataProcessor, IChatEventHandler<MessageReply>
	{
		private EventType[] eventType = new[] { EventType.MessageReply };

		public override EventType[] Events => eventType;

		public event Action<MessageReply> OnEvent;

		public override void ProcessEventData(EventType _, JToken data)
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
