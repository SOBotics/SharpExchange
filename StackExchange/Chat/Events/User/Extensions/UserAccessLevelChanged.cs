using System;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events.User.Extensions
{
	public static partial class Extensions
	{
		public static UserAccessLevelChanged AddUserAccessLevelChangedEventHandler<T>(this RoomWatcher<T> rw, Action<ChangedUserAccessLevel> callback) where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new UserAccessLevelChanged();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}

		public static UserAccessLevelChanged AddUserAccessLevelChangedEventHandler<T>(this RoomWatcher<T> rw, Action<Chat.User, Chat.User, UserAccessLevel> callback) where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new UserAccessLevelChanged();

			eventProcessor.OnEvent += ualc =>
			{
				var changedBy = new Chat.User(rw.Host, ualc.ChangedBy);
				var targetUser = new Chat.User(rw.Host, ualc.TargetUser);

				callback(changedBy, targetUser, ualc.NewLevel);
			};

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}
	}
}
