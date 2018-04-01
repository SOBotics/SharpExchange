using System;
using Newtonsoft.Json.Linq;

namespace StackExchange.Chat.Events.Message
{
	public class MessageDelete
	{
		public int DeletedBy { get; internal set; }
		public int MessageId { get; internal set; }
	}

	public class MessageDeleted : IChatEventDataProcessor, IChatEventHandler<MessageDelete>
	{
		public EventType Event => EventType.MessageDeleted;

		public event Action<MessageDelete> OnEvent;

		public void ProcessEventData(JToken data)
		{
			var msgId = data.Value<int>("message_id");
			var deletedBy = data.Value<int>("user_id");

			OnEvent?.Invoke(new MessageDelete
			{
				DeletedBy = deletedBy,
				MessageId = msgId
			});
		}
	}
}
