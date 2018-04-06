using System;
using Newtonsoft.Json.Linq;

namespace StackExchange.Chat.Events.User
{
	public class MentionedUser
	{
		public int MessageId { get; internal set; }
		public int PingerId { get; internal set; }
	}

	public class UserMentioned : ChatEventDataProcessor, IChatEventHandler<MentionedUser>
	{
		public override EventType Event => EventType.UserMentioned;

		public event Action<MentionedUser> OnEvent;

		public override void ProcessEventData(JToken data)
		{
			var roomOrigin = data.Value<int>("room_id");

			if (roomOrigin != RoomId) return;

			var msgId = data.Value<int>("message_id");
			var pingerId = data.Value<int>("user_id");

			OnEvent?.Invoke(new MentionedUser
			{
				MessageId = msgId,
				PingerId = pingerId
			});
		}
	}
}
