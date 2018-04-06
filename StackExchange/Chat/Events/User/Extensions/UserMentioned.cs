using System;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events.User.Extensions
{
	public static partial class Extensions
	{
		public static UserMentioned AddUserMentionedEventHandler<T>(this RoomWatcher<T> rw, Action<MentionedUser> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new UserMentioned();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.EventProcessors.Add(eventProcessor);

			return eventProcessor;
		}

		public static UserMentioned AddUserMentionedEventHandler<T>(this RoomWatcher<T> rw, Action<Chat.Message, Chat.User> callback) where T : IWebSocket
		{
			if (callback == null)
			{
				throw new ArgumentNullException(nameof(callback));
			}

			var eventProcessor = new UserMentioned();

			eventProcessor.OnEvent += um =>
			{
				var msg = new Chat.Message(rw.Host, um.MessageId);
				var pinger = new Chat.User(rw.Host, um.PingerId);

				callback(msg, pinger);
			};

			rw.EventRouter.EventProcessors.Add(eventProcessor);

			return eventProcessor;
		}
	}
}
