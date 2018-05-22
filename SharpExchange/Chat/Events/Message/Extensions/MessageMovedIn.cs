using System;
using System.Threading.Tasks;
using SharpExchange.Net.WebSockets;

namespace SharpExchange.Chat.Events.Message.Extensions
{
	public static partial class Extensions
	{
		public static MessageMovedIn AddMessageMovedInEventHandler<T>(this RoomWatcher<T> rw, Action<MovedMessage> callback) where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new MessageMovedIn();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}

		public static MessageMovedIn AddMessageMovedInEventHandler<T>(this RoomWatcher<T> rw, Action<Chat.User, Chat.Message> callback) where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new MessageMovedIn();

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

				Task.WaitAll(tasks);

				callback(movedBy, message);
			};

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}
	}
}
