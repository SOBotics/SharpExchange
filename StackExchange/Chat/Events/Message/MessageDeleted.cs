using System;
using Newtonsoft.Json.Linq;

namespace StackExchange.Chat.Events.Message
{
	public class DeletedMessage
	{
		public int DeletedBy { get; internal set; }
		public int MessageId { get; internal set; }
	}

	public class MessageDeleted : IChatEventDataProcessor, IChatEventHandler<DeletedMessage>
	{
		public EventType Event => EventType.MessageDeleted;

		public event Action<DeletedMessage> OnEvent;

		public void ProcessEventData(JToken data)
		{
			var msgId = data.Value<int>("message_id");
			var deletedBy = data.Value<int>("user_id");

			OnEvent?.Invoke(new DeletedMessage
			{
				DeletedBy = deletedBy,
				MessageId = msgId
			});
		}
	}
}
