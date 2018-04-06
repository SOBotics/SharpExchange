using System;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events.Message.Extensions
{
	public static partial class Extensions
	{
		public static MessageStarToggled AddMessageStarToggledEventHandler<T>(this RoomWatcher<T> rw, Action<MessageStars> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new MessageStarToggled();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}

		public static MessageStarToggled AddMessageStarToggledEventHandler<T>(this RoomWatcher<T> rw, Action<Chat.Message> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new MessageStarToggled();

			eventProcessor.OnEvent += ms =>
			{
				var message = new Chat.Message(rw.Host, ms.Id, rw.AuthCookies);

				callback(message);
			};

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}
	}
}