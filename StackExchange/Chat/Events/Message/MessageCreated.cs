using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace StackExchange.Chat.Events.Message
{
	public class MessageCreated : IChatEventDataProcessor, IChatEventHandler<Chat.Message>
	{
		public EventType Event => EventType.MessagePosted;

		public event Action<Chat.Message> OnEvent;

		public void ProcessEventData(string json)
		{
			//TODO: Finish off implementation.
			return;

			dynamic data = JObject.Parse(json);
			var msgId = data.message_id;
			var msg = new Chat.Message("", msgId);

			OnEvent(msg);
		}
	}
}
