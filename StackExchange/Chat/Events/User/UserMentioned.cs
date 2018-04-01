using System;
using Newtonsoft.Json.Linq;

namespace StackExchange.Chat.Events.User
{
	public class MentionedUser
	{
		public int MessageId { get; internal set; }
		public int PingerId { get; internal set; }
		public int PingeeId { get; internal set; }
	}

	public class UserMentioned : IChatEventDataProcessor, IChatEventHandler<MentionedUser>
	{
		public EventType Event => EventType.UserMentioned;

		public event Action<MentionedUser> OnEvent;

		public void ProcessEventData(JToken data)
		{
			var msgId = data.Value<int>("message_id");
			var pingerId = data.Value<int>("user_id");
			var pingeeId = data.Value<int>("target_user_id");

			OnEvent?.Invoke(new MentionedUser
			{
				MessageId = msgId,
				PingerId = pingerId,
				PingeeId = pingeeId
			});
		}
	}
}
