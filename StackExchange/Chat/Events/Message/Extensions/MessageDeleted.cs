using System;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events.Message.Extensions
{
	public static partial class Extensions
	{
		public static MessageDeleted AddMessageDeletedEventHandler<T>(this RoomWatcher<T> rw, Action<DeletedMessage> callback) where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new MessageDeleted();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}

		public static MessageDeleted AddMessageDeletedEventHandler<T>(this RoomWatcher<T> rw, Action<Chat.User, int> callback) where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new MessageDeleted();

			eventProcessor.OnEvent += md =>
			{
				var deletedBy = new Chat.User(rw.Host, md.DeletedBy);

				callback(deletedBy, md.MessageId);
			};

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}
	}
}