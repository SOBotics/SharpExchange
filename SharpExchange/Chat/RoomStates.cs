using System;

namespace SharpExchange.Chat
{
	[Flags]
	public enum RoomStates
	{
		None = 0,
		Normal = 1,
		Gallery = 2,
		Private = 4,
		Frozen = 8
	}
}
