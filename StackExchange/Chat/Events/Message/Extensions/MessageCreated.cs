using System;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events.Message.Extensions
{
	public static partial class Extensions
	{
		public static MessageCreated AddMessageCreatedEventHandler<T>(this RoomWatcher<T> rw, Action<int> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new MessageCreated();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.EventProcessors.Add(eventProcessor);

			return eventProcessor;
		}

		public static MessageCreated AddMessageCreatedEventHandler<T>(this RoomWatcher<T> rw, Action<Chat.Message> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new MessageCreated();

			eventProcessor.OnEvent += id =>
			{
				var message = new Chat.Message(rw.Host, id, rw.AuthCookies);

				callback(message);
			};

			rw.EventRouter.EventProcessors.Add(eventProcessor);

			return eventProcessor;
		}
	}
}