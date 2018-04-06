using System;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events.Message.Extensions
{
	public static partial class Extensions
	{
		public static MessageMovedIn AddMessageMovedInEventHandler<T>(this RoomWatcher<T> rw, Action<MovedMessage> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new MessageMovedIn();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}

		public static MessageMovedIn AddMessageMovedInEventHandler<T>(this RoomWatcher<T> rw, Action<Chat.User, Chat.Message> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new MessageMovedIn();

			eventProcessor.OnEvent += mm =>
			{
				var movedBy = new Chat.User(rw.Host, mm.MovedBy);
				var message = new Chat.Message(rw.Host, mm.MessageId, rw.AuthCookies);

				callback(movedBy, message);
			};

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}
	}
}
