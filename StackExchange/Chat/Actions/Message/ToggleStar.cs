using System.Collections.Generic;
using System.Net;
using RestSharp;

namespace StackExchange.Chat.Actions.Message
{
	public class ToggleStar : ChatAction
	{
		private readonly int messageId;

		internal override Method RequestMethod => Method.POST;

		internal override string Endpoint => $"https://{Host}/messages/{messageId}/star";

		internal override bool RequiresFKey => true;

		internal override bool RequiresAuthCookies => true;

		internal override ActionPermissionLevel RequiredPermissionLevel => ActionPermissionLevel.Anyone;



		public ToggleStar(int messageId)
		{
			this.messageId = messageId;
		}



		internal override object ProcessResponse(HttpStatusCode status, string response)
		{
			return status == HttpStatusCode.OK &&
				response?.ToUpperInvariant() == "\"OK\"";
		}
	}
}
