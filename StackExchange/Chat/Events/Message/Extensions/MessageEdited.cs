using System;
using System.Threading.Tasks;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events.Message.Extensions
{
	public static partial class Extensions
	{
		public static MessageEdited AddMessageEditedEventHandler<T>(
			this RoomWatcher<T> rw,
			Action<EditedMessage> callback)
			where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new MessageEdited();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}

		public static MessageEdited AddMessageEditedEventHandler<T>(
			this RoomWatcher<T> rw,
			Action<Chat.Message,
				Chat.User> callback)
			where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new MessageEdited();

			eventProcessor.OnEvent += me =>
			{
				Chat.Message message = null;
				Chat.User user = null;

				var tasks = new[]
				{
					Task.Run(() =>
					{
						message = new Chat.Message(rw.Host, me.Message, rw.Auth);
					}),
					Task.Run(() =>
					{
						user = new Chat.User(rw.Host, me.EditedBy, rw.Auth);
					})
				};

				Task.WhenAll(tasks).Wait();

				callback(message, user);
			};

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}
	}
}