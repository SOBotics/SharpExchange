using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace StackExchange.Chat.Events.User
{
	public class UserAccessLevelChange
	{
		public int ChangedBy { get; internal set; }
		public int TargetUser { get; internal set; }
		public UserAccessLevel NewLevel { get; internal set; }
	}

	public class UserAccessLevelChanged : IChatEventDataProcessor, IChatEventHandler<UserAccessLevelChange>
	{
		public EventType Event => EventType.UserAccessLevelChanged;

		public event Action<UserAccessLevelChange> OnEvent;

		public void ProcessEventData(JToken data)
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

			OnEvent?.Invoke(new UserAccessLevelChange
			{
				ChangedBy = userId,
				TargetUser = targetUserId,
				NewLevel = newLevel
			});
		}
	}
}
