using System;
using SharpExchange.Net.WebSockets;

namespace SharpExchange.Chat.Events.Message.Extensions
{
	public static partial class Extensions
	{
		public static MessageStarToggled AddMessageStarToggledEventHandler<T>(
			this RoomWatcher<T> rw,
			Action<MessageStars> callback)
			where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new MessageStarToggled();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}

		public static MessageStarToggled AddMessageStarToggledEventHandler<T>(
			this RoomWatcher<T> rw,
			Action<Chat.Message> callback)
			where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new MessageStarToggled();

			eventProcessor.OnEvent += ms =>
			{
				var message = new Chat.Message(rw.Host, ms.Id, rw.Auth);

				callback(message);
			};

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}
	}
}