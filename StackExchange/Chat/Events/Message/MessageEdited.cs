using System;
using Newtonsoft.Json.Linq;

namespace StackExchange.Chat.Events.Message
{
	public class EditedMessage
	{
		public int Message { get; set; }

		public int EditedBy { get; set; }
	}

	public class MessageEdited : ChatEventDataProcessor, IChatEventHandler<EditedMessage>
	{
		public override EventType Event => EventType.MessageEdited;

		public event Action<EditedMessage> OnEvent;

		public override void ProcessEventData(JToken data)
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
