using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace StackExchange.Chat.Actions.User
{
	public class UserEditAccessLevel : ChatAction
	{
		internal override Method RequestMethod => Method.POST;

		internal override string Endpoint => $"https://{Host}/rooms/setuseraccess/{RoomId}";

		internal override bool RequiresFKey => true;

		internal override bool RequiresAuthCookies => true;

		internal override ActionPermissionLevel RequiredPermissionLevel => ActionPermissionLevel.RoomOwner;



		public UserEditAccessLevel(int targetUserId, UserRoomAccessLevel newAccessLevel)
		{
			Data = new Dictionary<string, object>
			{
				["aclUserId"] = targetUserId
			};

			switch (newAccessLevel)
			{
				case UserRoomAccessLevel.Normal:
				{
					Data["userAccess"] = "remove";
					break;
				}
				case UserRoomAccessLevel.ReadOnly:
				{
					Data["userAccess"] = "read-only";
					break;
				}
				case UserRoomAccessLevel.ReadWrite:
				{
					Data["userAccess"] = "read-write";
					break;
				}
				case UserRoomAccessLevel.Owner:
				{
					Data["userAccess"] = "owner";
					break;
				}
			}
		}



		internal override object ProcessResponse(HttpStatusCode status, string response)
		{
			if (status != HttpStatusCode.OK)
			{
				return false;
			}

			// Sadly the response is just html, with no easy way of
			// figuring out whether our request was successful or not.
			// So until they change something, we'll just have to assume
			// everything went ok.
			return true;
		}
	}
}
