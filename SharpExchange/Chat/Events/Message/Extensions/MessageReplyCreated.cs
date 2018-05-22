using System;
using System.Threading.Tasks;
using SharpExchange.Net.WebSockets;

namespace SharpExchange.Chat.Events.Message.Extensions
{
	public static partial class Extensions
	{
		public static MessageReplyCreated AddMessageReplyCreatedEventHandler<T>(
			this RoomWatcher<T> rw,
			Action<MessageReply> callback)
			where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new MessageReplyCreated();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}

		public static MessageReplyCreated AddMessageReplyCreatedEventHandler<T>(
			this RoomWatcher<T> rw,
			Action<Chat.User, Chat.Message, Chat.Message> callback)
			where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new MessageReplyCreated();

			eventProcessor.OnEvent += mr =>
			{
				Chat.User author = null;
				Chat.Message message = null;
				Chat.Message targetMsg = null;

				var tasks = new[]
				{
					Task.Run(() =>
					{
						author = new Chat.User(rw.Host, mr.AuthorId, rw.Auth);
					}),
					Task.Run(() =>
					{
						message = new Chat.Message(rw.Host, mr.MessageId, rw.Auth);
					}),
					Task.Run(() =>
					{
						targetMsg = new Chat.Message(rw.Host, mr.TargetMessageId, rw.Auth);
					})
				};

				Task.WaitAll(tasks);

				callback(author, message, targetMsg);
			};

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}
	}
}
