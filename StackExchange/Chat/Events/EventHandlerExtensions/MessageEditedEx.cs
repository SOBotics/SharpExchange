using System;
using StackExchange.Chat.Events.Message;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events.EventHandlerExtensions
{
	public static class MessageEditedEx
	{
		public static MessageEdited AddMessageEditedEventHandler<T>(this RoomWatcher<T> rw, Action<MessageEdit> callback) where T : IWebSocket
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

		public static MessageEdited AddMessageEditedEventHandler<T>(this RoomWatcher<T> rw, Action<Chat.Message, User> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new MessageEdited();

			eventProcessor.OnEvent += me =>
			{
				var message = new Chat.Message(rw.Host, me.MessageId, rw.AuthCookies);
				var user = new User("", 0);//TODO: implement class

				callback(message, user);
			};

			rw.EventRouter.EventProcessors.Add(eventProcessor);

			return eventProcessor;
		}
	}
}