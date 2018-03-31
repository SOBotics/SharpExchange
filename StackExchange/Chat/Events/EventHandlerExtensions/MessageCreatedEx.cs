using System;
using StackExchange.Chat.Events.Message;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events.EventHandlerExtensions
{
	public static class MessageCreatedEx
	{
		/// <summary></summary>
		/// <param name="callback">
		/// A function that accepts the new message's ID which
		/// will be invoked when this event is triggered.
		/// </param>
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