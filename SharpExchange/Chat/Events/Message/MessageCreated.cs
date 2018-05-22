using System;
using Newtonsoft.Json.Linq;

namespace SharpExchange.Chat.Events.Message
{
	public class MessageCreated : ChatEventDataProcessor, IChatEventHandler<int>
	{
		public override EventType Event => EventType.MessagePosted;

		public event Action<int> OnEvent;

		public override void ProcessEventData(JToken data)
		{
			var msgId = data.Value<int>("message_id");

			OnEvent?.Invoke(msgId);
		}
	}
}
