using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace SharpExchange.Chat.Actions.User
{
	public class UserKickMuter : ChatAction
	{
		internal override Method RequestMethod => Method.POST;

		internal override string Endpoint => $"https://{Host}/rooms/kickmute/{RoomId}";

		internal override bool RequiresFKey => true;

		internal override bool RequiresAuthCookies => true;

		internal override ActionPermissionLevel RequiredPermissionLevel => ActionPermissionLevel.RoomOwner;



		public UserKickMuter(int userId)
		{
			Data = new Dictionary<string, object>
			{
				["userID"] = userId
			};
		}



		internal override object ProcessResponse(HttpStatusCode status, string json)
		{
			if (status != HttpStatusCode.OK)
			{
				return false;
			}

			var typeDef = new { message = "" };
			var data = JsonConvert.DeserializeAnonymousType(json, typeDef);
			var msg = data?.message?.ToUpperInvariant() ?? "";

			return msg.StartsWith("THE USER HAS NEEB KICKED AND CANNOT RETURN");
		}
	}
}
