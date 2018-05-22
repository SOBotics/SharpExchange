using System;
using SharpExchange.Net.WebSockets;

namespace SharpExchange.Chat.Events.User.Extensions
{
	public static partial class Extensions
	{
		public static UserEntered AddUserEnteredEventHandler<T>(
			this RoomWatcher<T> rw,
			Action<int> callback)
			where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new UserEntered();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}

		public static UserEntered AddUserEnteredEventHandler<T>(
			this RoomWatcher<T> rw,
			Action<Chat.User> callback)
			where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new UserEntered();

			eventProcessor.OnEvent += id =>
			{
				var user = new Chat.User(rw.Host, id,  rw.Auth);

				callback(user);
			};

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}
	}
}