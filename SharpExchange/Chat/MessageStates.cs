using System;

namespace SharpExchange.Chat
{
	[Flags]
	public enum MessageStates
	{
		None = 0,
		PubliclyVisible = 1,
		Starred = 2,
		Pinned = 4,
		Edited = 8,
		Deleted = 16,
		Purged = 32
	}
}
