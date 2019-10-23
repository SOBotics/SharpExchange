using System;
using Newtonsoft.Json.Linq;

namespace SharpExchange.Chat.Events.User
{
	public class ChangedUserAccessLevel
	{
		public int ChangedBy { get; internal set; }
		public int TargetUser { get; internal set; }
		public UserAccessLevel NewLevel { get; internal set; }
	}

	public class UserAccessLevelChanged : ChatEventDataProcessor, IChatEventHandler<ChangedUserAccessLevel>
	{
		private EventType[] eventType = new[] { EventType.UserAccessLevelChanged };

		public override EventType[] Events => eventType;

		public event Action<ChangedUserAccessLevel> OnEvent;

		public override void ProcessEventData(EventType _, JToken data)
		{
			var userId = data.Value<int>("user_id");
			var targetUserId = data.Value<int>("target_user_id");
			var newLevelStr = data
				.Value<string>("content")
				?.Remove(0, 11)
				.ToUpperInvariant();
			var newLevel = UserAccessLevel.Normal;

			switch (newLevelStr)
			{
				case "OWNER":
				{
					newLevel = UserAccessLevel.Owner;
					break;
				}
				case "READ-WRITE":
				{
					newLevel = UserAccessLevel.ReadWrite;
					break;
				}
				case "READ-ONLY":
				{
					newLevel = UserAccessLevel.ReadOnly;
					break;
				}
			}

			OnEvent?.Invoke(new ChangedUserAccessLevel
			{
				ChangedBy = userId,
				TargetUser = targetUserId,
				NewLevel = newLevel
			});
		}
	}
}
