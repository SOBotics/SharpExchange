using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace StackExchange.Chat.Actions.Message
{
	public class Create : ChatAction
	{
		internal override Method RequestMethod => Method.POST;

		internal override string Endpoint => $"https://{Host}/chats/{RoomId}/messages/new";

		internal override bool RequiresFKey => true;

		internal override bool RequiresAuthCookies => true;

		internal override ActionPermissionLevel RequiredPermissionLevel => ActionPermissionLevel.Anyone;



		public Create(string text)
		{
			Data = new Dictionary<string, object>
			{
				["text"] = text
			};
		}

		

		internal override object ProcessResponse(HttpStatusCode status, string json)
		{
			if (status != HttpStatusCode.OK)
			{
				return -1;
			}

			var typeDef = new { id = 0, time = 0 };
			var data = JsonConvert.DeserializeAnonymousType(json, typeDef);

			return data.id;
		}
	}
}
