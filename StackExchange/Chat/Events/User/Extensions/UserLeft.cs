using System;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events.User.Extensions
{
	public static partial class Extensions
	{
		public static UserLeft AddUserLeftEventHandler<T>(this RoomWatcher<T> rw, Action<int> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new UserLeft();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}

		public static UserLeft AddUserLeftEventHandler<T>(this RoomWatcher<T> rw, Action<Chat.User> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new UserLeft();

			eventProcessor.OnEvent += id =>
			{
				var user = new Chat.User(rw.Host, id);

				callback(user);
			};

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}
	}
}