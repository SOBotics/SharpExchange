using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using StackExchange.Net;

namespace StackExchange.Chat.Actions.Message
{
	public class CreateNew : ChatAction
	{
		internal override Method RequestMethod => Method.POST;

		internal override string Endpoint => $"https://{Host}/chats/{RoomId}/messages/new";

		internal override bool RequiresFKey => true;

		internal override bool RequiresAuthCookies => true;

		internal override ActionPermissionLevel RequiredPermissionLevel => ActionPermissionLevel.Anyone;



		public CreateNew(string text)
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
