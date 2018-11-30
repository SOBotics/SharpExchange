namespace SharpExchange.Chat.Events
{
	public enum EventType
	{
		/// <summary>
		/// SUBSCRIBE TO ALL DEH EVENTZ!!!11!
		/// </summary>
		All = -1,

		/// <summary>
		/// A new message has been posted.
		/// </summary>
		MessagePosted = 1,

		/// <summary>
		/// A message has been edited.
		/// </summary>
		MessageEdited = 2,

		/// <summary>
		/// A user has entered the room.
		/// </summary>
		UserEntered = 3,

		/// <summary>
		/// A user has left the room.
		/// </summary>
		UserLeft = 4,

		/// <summary>
		/// The room's name, description and/or tags have been changed.
		/// </summary>
		RoomNameChanged = 5,

		/// <summary>
		/// Someone has (un)starred a message.
		/// </summary>
		MessageStarToggled = 6,

		/// <summary>
		/// No idea.
		/// </summary>
		DebugMessage = 7,

		/// <summary>
		/// The current account has been mentioned (@Username) in a message.
		/// </summary>
		UserMentioned = 8,

		/// <summary>
		/// A message has been flagged as spam/offensive.
		/// </summary>
		MessageFlagged = 9,

		/// <summary>
		/// A message has been deleted.
		/// </summary>
		MessageDeleted = 10,

		/// <summary>
		/// A file has been uploaded to the room.
		/// Dev note: as far as I know, only one room supports this publicly,
		/// the Android SE testing app room.
		/// </summary>
		FileAdded = 11,

		/// <summary>
		/// A message has been flagged for moderator attention.
		/// </summary>
		ModeratorFlag = 12,

		/// <summary>
		/// No idea.
		/// </summary>
		UserSettingsChanged = 13,

		/// <summary>
		/// No idea.
		/// </summary>
		GlobalNotification = 14,

		/// <summary>
		/// A user's room access level has been changed.
		/// </summary>
		UserAccessLevelChanged = 15,

		/// <summary>
		/// No idea.
		/// </summary>
		UserNotification = 16,

		/// <summary>
		/// The current account has been invited to join another room.
		/// </summary>
		RoomInvitation = 17,

		/// <summary>
		/// Someone has posted a direct reply to a message posted by this account.
		/// </summary>
		MessageReply = 18,

		/// <summary>
		/// A room owner/moderator has moved a message out of the room.
		/// </summary>
		MessageMovedOut = 19,

		/// <summary>
		/// A room owner/moderator has moved a message into the room.
		/// </summary>
		MessageMovedIn = 20,

		/// <summary>
		/// No idea.
		/// </summary>
		TimeBreak = 21,

		/// <summary>
		/// New item in the feeds received.
		/// </summary>
		FeedTicker = 22,

		/// <summary>
		/// A user has been suspended.
		/// </summary>
		UserSuspended = 29,

		/// <summary>
		/// Two user accounts have been merged.
		/// </summary>
		UserMerged = 30,

		/// <summary>
		/// A user's name or avatar has been updated.
		/// </summary>
		UserNameOrAvatarChanged = 34
	}
}
