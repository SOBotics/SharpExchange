using System;
using System.Threading.Tasks;
using SharpExchange.Net.WebSockets;

namespace SharpExchange.Chat.Events.User.Extensions
{
	public static partial class Extensions
	{
		public static UserAccessLevelChanged AddUserAccessLevelChangedEventHandler<T>(
			this RoomWatcher<T> rw,
			Action<ChangedUserAccessLevel> callback)
			where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new UserAccessLevelChanged();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}

		public static UserAccessLevelChanged AddUserAccessLevelChangedEventHandler<T>(
			this RoomWatcher<T> rw,
			Action<Chat.User, Chat.User, UserAccessLevel> callback)
			where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new UserAccessLevelChanged();

			eventProcessor.OnEvent += ualc =>
			{
				Chat.User changedBy = null;
				Chat.User targetUser = null;

				var tasks = new[]
				{
					Task.Run(() =>
					{
						changedBy = new Chat.User(rw.Host, ualc.ChangedBy, rw.Auth);
					}),
					Task.Run(() =>
					{
						targetUser = new Chat.User(rw.Host, ualc.TargetUser, rw.Auth);
					})
				};

				Task.WaitAll(tasks);

				callback(changedBy, targetUser, ualc.NewLevel);
			};

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}
	}
}
