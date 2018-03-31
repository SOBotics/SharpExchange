using System;
using Newtonsoft.Json.Linq;

namespace StackExchange.Chat.Events.Message
{
	public class MessageEdit
	{
		public int Message { get; set; }

		public int EditedBy { get; set; }
	}

	public class MessageEdited : IChatEventDataProcessor, IChatEventHandler<MessageEdit>
	{
		public EventType Event => EventType.MessageEdited;

		public event Action<MessageEdit> OnEvent;

		public void ProcessEventData(JToken data)
		{
			var msgId = data.Value<int>("message_id");
			var usrId = data.Value<int>("user_id");

			OnEvent?.Invoke(new MessageEdit
			{
				Message = msgId,
				EditedBy = usrId
			});
		}
	}
}
