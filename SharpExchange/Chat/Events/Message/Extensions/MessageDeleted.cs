using System;
using SharpExchange.Net.WebSockets;

namespace SharpExchange.Chat.Events.Message.Extensions
{
	public static partial class Extensions
	{
		public static MessageDeleted AddMessageDeletedEventHandler<T>(
			this RoomWatcher<T> rw,
			Action<DeletedMessage> callback)
			where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new MessageDeleted();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}

		public static MessageDeleted AddMessageDeletedEventHandler<T>(
			this RoomWatcher<T> rw,
			Action<Chat.User, int> callback)
			where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new MessageDeleted();

			eventProcessor.OnEvent += md =>
			{
				var deletedBy = new Chat.User(rw.Host, md.DeletedBy, rw.Auth);

				callback(deletedBy, md.MessageId);
			};

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}
	}
}