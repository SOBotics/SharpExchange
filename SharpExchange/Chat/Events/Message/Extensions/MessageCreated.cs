using System;
using SharpExchange.Net.WebSockets;

namespace SharpExchange.Chat.Events.Message.Extensions
{
	public static partial class Extensions
	{
		public static MessageCreated AddMessageCreatedEventHandler<T>(
			this RoomWatcher<T> rw,
			Action<int> callback)
			where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new MessageCreated();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}

		public static MessageCreated AddMessageCreatedEventHandler<T>(
			this RoomWatcher<T> rw,
			Action<Chat.Message> callback)
			where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new MessageCreated();

			eventProcessor.OnEvent += id =>
			{
				var message = new Chat.Message(rw.Host, id, rw.Auth);

				callback(message);
			};

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}
	}
}