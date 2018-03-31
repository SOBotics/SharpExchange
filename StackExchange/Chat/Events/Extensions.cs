using System;
using StackExchange.Chat;
using StackExchange.Chat.Events.Message;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events
{
	public static class Extensions
	{
		public static void AddEventHandler<T>(this RoomWatcher<T> rw, IChatEventDataProcessor eventProcessor) where T : IWebSocket
		{
			if (eventProcessor == null)
			{
				throw new ArgumentNullException(nameof(eventProcessor));
			}

			rw.EventRouter.EventProcessors.Add(eventProcessor);
		}

		public static bool RemoveEventHanlder<T>(this RoomWatcher<T> rw, IChatEventDataProcessor eventProcessor) where T : IWebSocket
		{
			if (eventProcessor == null)
			{
				throw new ArgumentNullException(nameof(eventProcessor));
			}

			return rw.EventRouter.EventProcessors.Remove(eventProcessor);
		}

		public static MessageCreated AddMessageCreatedEventHandler<T>(this RoomWatcher<T> rw, Action<Chat.Message> callback) where T : IWebSocket
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
	}
}