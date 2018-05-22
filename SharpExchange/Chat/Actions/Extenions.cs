using System;
using System.Threading.Tasks;
using SharpExchange.Chat.Actions.Message;
using SharpExchange.Chat.Actions.Room;
using SharpExchange.Chat.Actions.User;

namespace SharpExchange.Chat.Actions
{
	public static class Extenions
	{
		#region Message extensions

		public static Task<int> CreateMessageAsync(this ActionScheduler actionScheduler, string message)
		{
			message.ThrowIfNullOrEmpty(nameof(message));

			var action = new MessageCreator(message);

			return actionScheduler.ScheduleActionAsync<int>(action);
		}

		public static Task<int> CreatePingAsync(this ActionScheduler actionScheduler, string message, int userId)
		{
			message.ThrowIfNullOrEmpty(nameof(message));

			var userToPing = new Chat.User(actionScheduler.Host, userId);

			return CreatePingAsync(actionScheduler, message, userToPing.Username);
		}

		public static Task<int> CreatePingAsync(this ActionScheduler actionScheduler, string message, Chat.User userToPing)
		{
			message.ThrowIfNullOrEmpty(nameof(message));
			userToPing.ThrowIfNull(nameof(userToPing));

			return CreatePingAsync(actionScheduler, message, userToPing.Username);
		}

		public static Task<int> CreatePingAsync(this ActionScheduler actionScheduler, string message, string username)
		{
			message.ThrowIfNullOrEmpty(nameof(message));
			username.ThrowIfNullOrEmpty(nameof(username));

			var name = username.Replace(" ", "").Trim();
			var txt = $"@{name} {message}";

			return CreateMessageAsync(actionScheduler, txt);
		}

		public static Task<int> CreateReplyAsync(this ActionScheduler actionScheduler, string message, Chat.Message messageToReplyTo)
		{
			message.ThrowIfNullOrEmpty(nameof(message));
			messageToReplyTo.ThrowIfNull(nameof(messageToReplyTo));

			return CreateReplyAsync(actionScheduler, message, messageToReplyTo.Id);
		}

		public static Task<int> CreateReplyAsync(this ActionScheduler actionScheduler, string message, int messageId)
		{
			message.ThrowIfNullOrEmpty(nameof(message));

			var txt = $":{messageId} {message}";

			return CreateMessageAsync(actionScheduler, txt);
		}

		public static Task<bool> DeleteMessageAsync(this ActionScheduler actionScheduler, Chat.Message message)
		{
			message.ThrowIfNull(nameof(message));

			return DeleteMessageAsync(actionScheduler, message.Id);
		}

		public static Task<bool> DeleteMessageAsync(this ActionScheduler actionScheduler, int messageId)
		{
			var action = new MessageDeleter(messageId);

			return actionScheduler.ScheduleActionAsync<bool>(action);
		}

		public static Task<bool> EditMessageAsync(this ActionScheduler actionScheduler, Chat.Message message, string newText)
		{
			message.ThrowIfNull(nameof(message));
			newText.ThrowIfNullOrEmpty(nameof(newText));

			return EditMessageAsync(actionScheduler, message.Id, newText);
		}

		public static Task<bool> EditMessageAsync(this ActionScheduler actionScheduler, int messageId, string newText)
		{
			newText.ThrowIfNullOrEmpty(nameof(newText));

			var action = new MessageEditor(messageId, newText);

			return actionScheduler.ScheduleActionAsync<bool>(action);
		}

		#endregion

		#region Star/pin extensions

		public static Task<bool> TogglePinAsync(this ActionScheduler actionScheduler, Chat.Message message)
		{
			message.ThrowIfNull(nameof(message));

			return TogglePinAsync(actionScheduler, message.Id);
		}

		public static Task<bool> TogglePinAsync(this ActionScheduler actionScheduler, int messageId)
		{
			var action = new MessagePinToggler(messageId);

			return actionScheduler.ScheduleActionAsync<bool>(action);
		}

		public static Task<bool> ClearStarsAsync(this ActionScheduler actionScheduler, Chat.Message message)
		{
			message.ThrowIfNull(nameof(message));

			return ClearStarsAsync(actionScheduler, message.Id);
		}

		public static Task<bool> ClearStarsAsync(this ActionScheduler actionScheduler, int messageId)
		{
			var action = new MessageStarsClearer(messageId);

			return actionScheduler.ScheduleActionAsync<bool>(action);
		}

		public static Task<bool> ToggleStarAsync(this ActionScheduler actionScheduler, Chat.Message message)
		{
			message.ThrowIfNull(nameof(message));

			return ToggleStarAsync(actionScheduler, message.Id);
		}

		public static Task<bool> ToggleStarAsync(this ActionScheduler actionScheduler, int messageId)
		{
			var action = new MessageStarToggler(messageId);

			return actionScheduler.ScheduleActionAsync<bool>(action);
		}

		#endregion

		#region Misc room owner actions extensions

		public static Task<bool> TimeoutRoomAsync(this ActionScheduler actionScheduler, TimeSpan duration, string reason)
		{
			var seconds = (int)Math.Round(duration.TotalSeconds);

			if (seconds < 5)
			{
				throw new ArgumentOutOfRangeException(nameof(duration), "Must total more than or be equal to 5 seconds.");
			}

			reason.ThrowIfNullOrEmpty(nameof(reason));

			return TimeoutRoomAsync(actionScheduler, seconds, reason);
		}

		public static Task<bool> TimeoutRoomAsync(this ActionScheduler actionScheduler, int durationSeconds, string reason)
		{
			if (durationSeconds < 5)
			{
				throw new ArgumentOutOfRangeException(nameof(durationSeconds), "Must be more than or equal to 5.");
			}

			reason.ThrowIfNullOrEmpty(nameof(reason));

			var action = new RoomTimeout(durationSeconds, reason);

			return actionScheduler.ScheduleActionAsync<bool>(action);
		}

		public static Task ChangeUserAccessLevelAsync(this ActionScheduler actionScheduler, Chat.User user, UserAccessLevel newLevel)
		{
			user.ThrowIfNull(nameof(user));

			return ChangeUserAccessLevelAsync(actionScheduler, user.Id, newLevel);
		}

		public static Task ChangeUserAccessLevelAsync(this ActionScheduler actionScheduler, int userId, UserAccessLevel newLevel)
		{
			var action = new UserAccessLevelEditor(userId, newLevel);

			return actionScheduler.ScheduleActionAsync<bool>(action);
		}

		public static Task<bool> KickMuteUser(this ActionScheduler actionScheduler, Chat.User user)
		{
			user.ThrowIfNull(nameof(user));

			return KickMuteUserAsync(actionScheduler, user.Id);
		}

		public static Task<bool> KickMuteUserAsync(this ActionScheduler actionScheduler, int userId)
		{
			var action = new UserKickMuter(userId);

			return actionScheduler.ScheduleActionAsync<bool>(action);
		}

		#endregion
	}
}
