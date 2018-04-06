using System;
using StackExchange.Chat.Actions.Message;
using StackExchange.Chat.Actions.Room;
using StackExchange.Chat.Actions.User;

namespace StackExchange.Chat.Actions
{
	public static class Extenions
	{
		public static int CreateMessage(this ActionScheduler actionScheduler, string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				throw new ArgumentException($"'{nameof(message)}' cannot be null or empty.");
			}

			var action = new MessageCreator(message);

			return (int)actionScheduler.ScheduleAction(action);
		}

		public static int CreatePing(this ActionScheduler actionScheduler, string message, int userId)
		{
			var userToPing = new Chat.User(actionScheduler.Host, userId);

			return CreatePing(actionScheduler, message, userToPing);
		}

		public static int CreatePing(this ActionScheduler actionScheduler, string message, Chat.User userToPing)
		{
			var name = userToPing.Username.Replace(" ", "").Trim();
			var txt = $"@{name} {message}";

			return CreateMessage(actionScheduler, txt);
		}

		public static int CreateReply(this ActionScheduler actionScheduler, string message, Chat.Message messageToReplyTo)
		{
			return CreateReply(actionScheduler, message, messageToReplyTo.Id);
		}

		public static int CreateReply(this ActionScheduler actionScheduler, string message, int messageId)
		{
			var txt = $":{messageId} {message}";

			return CreateMessage(actionScheduler, txt);
		}

		public static bool DeleteMessage(this ActionScheduler actionScheduler, int messageId)
		{
			if (messageId < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(messageId));
			}

			var action = new MessageDeleter(messageId);

			return (bool)actionScheduler.ScheduleAction(action);
		}

		public static bool DeleteMessage(this ActionScheduler actionScheduler, Chat.Message message)
		{
			if (message == null)
			{
				throw new ArgumentNullException(nameof(message));
			}

			return DeleteMessage(actionScheduler, message.Id);
		}

		public static bool EditMessage(this ActionScheduler actionScheduler, int messageId, string newText)
		{
			if (messageId < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(messageId));
			}

			if (string.IsNullOrEmpty(newText))
			{
				throw new ArgumentException($"'{nameof(newText)}' cannot be null or empty.");
			}

			var action = new MessageEditor(messageId, newText);

			return (bool)actionScheduler.ScheduleAction(action);
		}

		public static bool EditMessage(this ActionScheduler actionScheduler, Chat.Message message, string newText)
		{
			if (message == null)
			{
				throw new ArgumentNullException(nameof(message));
			}

			return EditMessage(actionScheduler, message.Id, newText);
		}

		public static bool TogglePin(this ActionScheduler actionScheduler, int messageId)
		{
			if (messageId < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(messageId));
			}

			var action = new MessagePinToggler(messageId);

			return (bool)actionScheduler.ScheduleAction(action);
		}

		public static bool TogglePin(this ActionScheduler actionScheduler, Chat.Message message)
		{
			if (message == null)
			{
				throw new ArgumentNullException(nameof(message));
			}

			return TogglePin(actionScheduler, message.Id);
		}

		public static bool ClearStars(this ActionScheduler actionScheduler, int messageId)
		{
			if (messageId < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(messageId));
			}

			var action = new MessageStarsClearer(messageId);

			return (bool)actionScheduler.ScheduleAction(action);
		}

		public static bool ClearStars(this ActionScheduler actionScheduler, Chat.Message message)
		{
			if (message == null)
			{
				throw new ArgumentNullException(nameof(message));
			}

			return ClearStars(actionScheduler, message.Id);
		}

		public static bool ToggleStar(this ActionScheduler actionScheduler, int messageId)
		{
			if (messageId < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(messageId));
			}

			var action = new MessageStarToggler(messageId);

			return (bool)actionScheduler.ScheduleAction(action);
		}

		public static bool ToggleStar(this ActionScheduler actionScheduler, Chat.Message message)
		{
			if (message == null)
			{
				throw new ArgumentNullException(nameof(message));
			}

			return ToggleStar(actionScheduler, message.Id);
		}

		public static bool TimeoutRoom(this ActionScheduler actionScheduler, int durationSeconds, string reason)
		{
			if (durationSeconds < 5)
			{
				throw new ArgumentOutOfRangeException(nameof(durationSeconds), "Must be more than or equal to 5.");
			}

			if (string.IsNullOrEmpty(reason))
			{
				throw new ArgumentException($"'{nameof(reason)}' cannot be null or empty.");
			}

			var action = new RoomTimeout(durationSeconds, reason);

			return (bool)actionScheduler.ScheduleAction(action);
		}

		public static bool TimeoutRoom(this ActionScheduler actionScheduler, TimeSpan duration, string reason)
		{
			var seconds = (int)Math.Round(duration.TotalSeconds);

			if (seconds < 5)
			{
				throw new ArgumentOutOfRangeException(nameof(duration), "Must total more than or be equal to 5 seconds.");
			}

			if (string.IsNullOrEmpty(reason))
			{
				throw new ArgumentException($"'{nameof(reason)}' cannot be null or empty.");
			}

			return TimeoutRoom(actionScheduler, seconds, reason);
		}

		public static void ChangeUserAccessLevel(this ActionScheduler actionScheduler, int userId, UserAccessLevel newLevel)
		{
			var action = new UserAccessLevelEditor(userId, newLevel);

			actionScheduler.ScheduleAction(action);
		}

		public static void ChangeUserAccessLevel(this ActionScheduler actionScheduler, Chat.User user, UserAccessLevel newLevel)
		{
			if (user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}

			ChangeUserAccessLevel(actionScheduler, user.Id, newLevel);
		}

		public static bool KickMuteUser(this ActionScheduler actionScheduler, int userId)
		{
			var action = new UserKickMuter(userId);

			return (bool)actionScheduler.ScheduleAction(action);
		}

		public static bool KickMuteUser(this ActionScheduler actionScheduler, Chat.User user)
		{
			if (user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}

			return KickMuteUser(actionScheduler, user.Id);
		}
	}
}
