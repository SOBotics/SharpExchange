using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace SharpExchange.Chat.Actions.Message
{
	public class MessageCreator : ChatAction
	{
		internal override Method RequestMethod => Method.POST;

		internal override string Endpoint => $"https://{Host}/chats/{RoomId}/messages/new";

		internal override bool RequiresFKey => true;

		internal override bool RequiresAuthCookies => true;

		internal override ActionPermissionLevel RequiredPermissionLevel => ActionPermissionLevel.Anyone;



		public MessageCreator(string text)
		{
			text.ThrowIfNullOrEmpty(nameof(text));

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

			var data = JObject.Parse(json);
			var id = data.Value<int?>("id") ?? -1;

			return id;
		}
	}
}
