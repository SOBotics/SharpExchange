using System;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events.User.Extensions
{
	public static partial class Extensions
	{
		public static UserInvitedToRoom AddUserInvitedToRoomEventHandler<T>(this RoomWatcher<T> rw, Action<RoomInvite> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new UserInvitedToRoom();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}

		public static UserInvitedToRoom AddUserInvitedToRoomEventHandler<T>(this RoomWatcher<T> rw, Action<Chat.User,  Chat.User, Chat.Room> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new UserInvitedToRoom();

			eventProcessor.OnEvent += ri =>
			{
				var inviter = new Chat.User(rw.Host, ri.Inviter);
				var invitee = new Chat.User(rw.Host, ri.Invitee);
				var room = new Chat.Room(rw.Host, ri.Room);

				callback(inviter, invitee, room);
			};

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}
	}
}
