using System;

namespace StackExchange.Chat
{
	[Flags]
	public enum RoomStates
	{
		Normal = 0,
		Gallery = 1,
		Private = 2,
		Frozen = 4
	}
}
