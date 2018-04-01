using System;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events.Message.Extensions
{
	public static partial class Extensions
	{
		public static MessageEdited AddMessageEditedEventHandler<T>(this RoomWatcher<T> rw, Action<EditedMessage> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new MessageEdited();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.EventProcessors.Add(eventProcessor);

			return eventProcessor;
		}

		public static MessageEdited AddMessageEditedEventHandler<T>(this RoomWatcher<T> rw, Action<Chat.Message, Chat.User> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new MessageEdited();

			eventProcessor.OnEvent += me =>
			{
				var message = new Chat.Message(rw.Host, me.Message, rw.AuthCookies);
				var user = new Chat.User(rw.Host, me.EditedBy);

				callback(message, user);
			};

			rw.EventRouter.EventProcessors.Add(eventProcessor);

			return eventProcessor;
		}
	}
}