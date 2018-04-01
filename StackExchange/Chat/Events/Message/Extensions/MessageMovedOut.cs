using System;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events.Message.Extensions
{
	public static partial class Extensions
	{
		public static MessageMovedOut AddMessageMovedOutEventHandler<T>(this RoomWatcher<T> rw, Action<MovedMessage> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new MessageMovedOut();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.EventProcessors.Add(eventProcessor);

			return eventProcessor;
		}

		public static MessageMovedOut AddMessageMovedOutEventHandler<T>(this RoomWatcher<T> rw, Action<Chat.User, Chat.Message> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new MessageMovedOut();

			eventProcessor.OnEvent += mm =>
			{
				var movedBy = new Chat.User(rw.Host, mm.MovedBy);
				var message = new Chat.Message(rw.Host, mm.MessageId, rw.AuthCookies);

				callback(movedBy, message);
			};

			rw.EventRouter.EventProcessors.Add(eventProcessor);

			return eventProcessor;
		}
	}
}
