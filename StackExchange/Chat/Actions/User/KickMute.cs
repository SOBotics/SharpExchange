using System.Collections.Generic;
using System.Net;
using RestSharp;

namespace StackExchange.Chat.Actions.Message
{
	public class KickMute : ChatAction
	{
		internal override Method RequestMethod => Method.POST;

		internal override string Endpoint => $"https://{Host}/rooms/kickmute/{RoomId}";

		internal override bool RequiresFKey => true;

		internal override bool RequiresAuthCookies => true;

		internal override ActionPermissionLevel RequiredPermissionLevel => ActionPermissionLevel.RoomOwner;



		public KickMute(int userId)
		{
			Data = new Dictionary<string, object>
			{
				["userID"] = userId
			};
		}



		internal override object ProcessResponse(HttpStatusCode status, string response)
		{
			//TODO: Has yet to successfully pass testing. All attempts have so far failed with a 500 error.
			//TODO: should probably deserialise response if status == ok.
			return status == HttpStatusCode.OK &&
				response.ToUpperInvariant().Contains("HAS BEEN KICKED");
		}
	}
}
