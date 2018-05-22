using System.Collections.Generic;
using System.Net;
using RestSharp;

namespace SharpExchange.Chat.Actions.User
{
	public class UserAccessLevelEditor : ChatAction
	{
		internal override Method RequestMethod => Method.POST;

		internal override string Endpoint => $"https://{Host}/rooms/setuseraccess/{RoomId}";

		internal override bool RequiresFKey => true;

		internal override bool RequiresAuthCookies => true;

		internal override ActionPermissionLevel RequiredPermissionLevel => ActionPermissionLevel.RoomOwner;



		public UserAccessLevelEditor(int targetUserId, UserAccessLevel newAccessLevel)
		{
			Data = new Dictionary<string, object>
			{
				["aclUserId"] = targetUserId
			};

			switch (newAccessLevel)
			{
				case UserAccessLevel.Normal:
				{
					Data["userAccess"] = "remove";
					break;
				}
				case UserAccessLevel.ReadOnly:
				{
					Data["userAccess"] = "read-only";
					break;
				}
				case UserAccessLevel.ReadWrite:
				{
					Data["userAccess"] = "read-write";
					break;
				}
				case UserAccessLevel.Owner:
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
