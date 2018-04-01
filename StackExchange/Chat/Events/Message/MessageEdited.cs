using System;
using Newtonsoft.Json.Linq;

namespace StackExchange.Chat.Events.Message
{
	public class EditedMessage
	{
		public int Message { get; set; }

		public int EditedBy { get; set; }
	}

	public class MessageEdited : IChatEventDataProcessor, IChatEventHandler<EditedMessage>
	{
		public EventType Event => EventType.MessageEdited;

		public event Action<EditedMessage> OnEvent;

		public void ProcessEventData(JToken data)
		{
			var msgId = data.Value<int>("message_id");
			var usrId = data.Value<int>("user_id");

			OnEvent?.Invoke(new EditedMessage
			{
				Message = msgId,
				EditedBy = usrId
			});
		}
	}
}
