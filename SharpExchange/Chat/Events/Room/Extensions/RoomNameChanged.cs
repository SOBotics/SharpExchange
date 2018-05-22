using System;
using SharpExchange.Net.WebSockets;

namespace SharpExchange.Chat.Events.Room.Extensions
{
	public static partial class Extensions
	{
		public static RoomNameChanged AddRoomNameChangedEventHandler<T>(
			this RoomWatcher<T> rw,
			Action<Chat.Room> callback)
			where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new RoomNameChanged();

			eventProcessor.OnEvent += () =>
			{
				var room = new Chat.Room(rw.Host, rw.RoomId,  rw.Auth);

				callback(room);
			};

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}
	}
}