using System;
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

		public static bool RemoveEventHandler<T>(this RoomWatcher<T> rw, IChatEventDataProcessor eventProcessor) where T : IWebSocket
		{
			if (eventProcessor == null)
			{
				throw new ArgumentNullException(nameof(eventProcessor));
			}

			return rw.EventRouter.EventProcessors.Remove(eventProcessor);
		}
	}
}
