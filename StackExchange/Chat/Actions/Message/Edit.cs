using System.Collections.Generic;
using System.Net;
using RestSharp;

namespace StackExchange.Chat.Actions.Message
{
	public class Edit : ChatAction
	{
		private readonly int messageId;

		internal override Method RequestMethod => Method.POST;

		internal override string Endpoint => $"https://{Host}/messages/{messageId}";

		internal override bool RequiresFKey => true;

		internal override bool RequiresAuthCookies => true;

		internal override ActionPermissionLevel RequiredPermissionLevel => ActionPermissionLevel.Anyone;



		public Edit(int messageId, string text)
		{
			this.messageId = messageId;

			Data = new Dictionary<string, object>
			{
				["text"] = text
			};
		}



		internal override object ProcessResponse(HttpStatusCode status, string response)
		{
			return status == HttpStatusCode.OK &&
				response?.ToUpperInvariant() == "\"OK\"";
		}
	}
}
