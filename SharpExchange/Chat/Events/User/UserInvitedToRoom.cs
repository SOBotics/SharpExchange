using System;
using Newtonsoft.Json.Linq;

namespace SharpExchange.Chat.Events.User
{
	public class RoomInvite
	{
		public int Inviter { get; internal set; }
		public int Invitee { get; internal set; }
		public int Room { get; internal set; }
	}

	public class UserInvitedToRoom : ChatEventDataProcessor, IChatEventHandler<RoomInvite>
	{
		private EventType[] eventType = new[] { EventType.RoomInvitation };

		public override EventType[] Events => eventType;

		public event Action<RoomInvite> OnEvent;

		public override void ProcessEventData(EventType _, JToken data)
		{
			var inviter = data.Value<int>("user_id");
			var invitee = data.Value<int>("target_user_id");
			var room = data.Value<int>("room_id");

			OnEvent?.Invoke(new RoomInvite
			{
				Inviter = inviter,
				Invitee = invitee,
				Room = room
			});
		}
	}
}
