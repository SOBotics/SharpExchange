using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events.User.Extensions
{
	public static partial class Extensions
	{
		public static UserAccessLevelChanged AddUserAccessLevelChangedEventHandler<T>(this RoomWatcher<T> rw, Action<UserAccessLevelChange> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new UserAccessLevelChanged();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.EventProcessors.Add(eventProcessor);

			return eventProcessor;
		}

		public static UserAccessLevelChanged AddUserAccessLevelChangedEventHandler<T>(this RoomWatcher<T> rw, Action<Chat.User, Chat.User, UserAccessLevel> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new UserAccessLevelChanged();

			eventProcessor.OnEvent += ualc =>
			{
				var changedBy = new Chat.User(rw.Host, ualc.ChangedBy);
				var targetUser = new Chat.User(rw.Host, ualc.TargetUser);

				callback(changedBy, targetUser, ualc.NewLevel);
			};

			rw.EventRouter.EventProcessors.Add(eventProcessor);

			return eventProcessor;
		}
	}
}
