using System;
using System.Threading.Tasks;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events.Message.Extensions
{
	public static partial class Extensions
	{
		public static MessageMovedOut AddMessageMovedOutEventHandler<T>(
			this RoomWatcher<T> rw,
			Action<MovedMessage> callback)
			where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new MessageMovedOut();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}

		public static MessageMovedOut AddMessageMovedOutEventHandler<T>(
			this RoomWatcher<T> rw,
			Action<Chat.User, Chat.Message> callback)
			where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new MessageMovedOut();

			eventProcessor.OnEvent += mm =>
			{
				Chat.User movedBy = null;
				Chat.Message message = null;

				var tasks = new[]
				{
					Task.Run(() =>
					{
						movedBy = new Chat.User(rw.Host, mm.MovedBy, rw.Auth);
					}),
					Task.Run(() =>
					{
						message = new Chat.Message(rw.Host, mm.MessageId, rw.Auth);
					})
				};

				Task.WhenAll(tasks).Wait();

				callback(movedBy, message);
			};

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}
	}
}
