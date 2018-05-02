using System;
using StackExchange.Net.WebSockets;

namespace StackExchange.Chat.Events.User.Extensions
{
	public static partial class Extensions
	{
		public static UserMentioned AddUserMentionedEventHandler<T>(this RoomWatcher<T> rw, Action<MentionedUser> callback) where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new UserMentioned();

			eventProcessor.OnEvent += callback;

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}

		public static UserMentioned AddUserMentionedEventHandler<T>(this RoomWatcher<T> rw, Action<Chat.Message> callback) where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new UserMentioned();

			eventProcessor.OnEvent += um =>
			{
				var msg = new Chat.Message(rw.Host, um.MessageId);

				callback(msg);
			};

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}

		public static UserMentioned AddUserMentionedEventHandler<T>(this RoomWatcher<T> rw, Action<Chat.Message, Chat.User> callback) where T : IWebSocket
		{
			callback.ThrowIfNull(nameof(callback));

			var eventProcessor = new UserMentioned();

			eventProcessor.OnEvent += um =>
			{
				var msg = new Chat.Message(rw.Host, um.MessageId);
				var pinger = new Chat.User(rw.Host, um.PingerId);

				callback(msg, pinger);
			};

			rw.EventRouter.AddProcessor(eventProcessor);

			return eventProcessor;
		}
	}
}
