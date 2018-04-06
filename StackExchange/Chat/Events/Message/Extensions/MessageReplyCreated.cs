using System;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events.Message.Extensions
{
	public static partial class Extensions
	{
		public static MessageReplyCreated AddMessageReplyCreatedEventHandler<T>(this RoomWatcher<T> rw, Action<MessageReply> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new MessageReplyCreated();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}

		public static MessageReplyCreated AddMessageReplyCreatedEventHandler<T>(this RoomWatcher<T> rw, Action<Chat.User, Chat.Message, Chat.Message> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new MessageReplyCreated();

			eventProcessor.OnEvent += mr =>
			{
				var author = new Chat.User(rw.Host, mr.AuthorId);
				var message = new Chat.Message(rw.Host, mr.MessageId, rw.AuthCookies);
				var targetMsg = new Chat.Message(rw.Host, mr.TargetMessageId, rw.AuthCookies);

				callback(author, message, targetMsg);
			};

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}
	}
}
