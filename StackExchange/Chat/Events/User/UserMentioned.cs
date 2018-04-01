using System;
using Newtonsoft.Json.Linq;

namespace StackExchange.Chat.Events.User
{
	public class UserMention
	{
		public int MessageId { get; internal set; }
		public int PingerId { get; internal set; }
		public int PingeeId { get; internal set; }
	}

	public class UserMentioned : IChatEventDataProcessor, IChatEventHandler<UserMention>
	{
		public EventType Event => EventType.UserMentioned;

		public event Action<UserMention> OnEvent;

		public void ProcessEventData(JToken data)
		{
			var msgId = data.Value<int>("message_id");
			var pingerId = data.Value<int>("user_id");
			var pingeeId = data.Value<int>("target_user_id");

			OnEvent?.Invoke(new UserMention
			{
				MessageId = msgId,
				PingerId = pingerId,
				PingeeId = pingeeId
			});
		}
	}
}
