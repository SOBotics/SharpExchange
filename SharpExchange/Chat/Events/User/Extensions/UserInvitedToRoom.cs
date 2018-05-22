using System;
using System.Threading.Tasks;
using SharpExchange.Net.WebSockets;

namespace SharpExchange.Chat.Events.User.Extensions
{
	public static partial class Extensions
	{
		public static UserInvitedToRoom AddUserInvitedToRoomEventHandler<T>(
			this RoomWatcher<T> rw,
			Action<RoomInvite> callback) 
			where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new UserInvitedToRoom();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}

		public static UserInvitedToRoom AddUserInvitedToRoomEventHandler<T>(
			this RoomWatcher<T> rw,
			Action<Chat.User, Chat.User, Chat.Room> callback)
			where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new UserInvitedToRoom();

			eventProcessor.OnEvent += ri =>
			{
				Chat.User inviter = null;
				Chat.User invitee = null;
				Chat.Room room = null;

				var tasks = new[]
				{
					Task.Run(() =>
					{
						inviter = new Chat.User(rw.Host, ri.Inviter, rw.Auth);
					}),
					Task.Run(() =>
					{
						invitee = new Chat.User(rw.Host, ri.Invitee, rw.Auth);
					}),
					Task.Run(() =>
					{
						room = new Chat.Room(rw.Host, ri.Room, rw.Auth);
					})
				};

				Task.WaitAll(tasks);

				callback(inviter, invitee, room);
			};

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}
	}
}
