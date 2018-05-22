using System;
using System.Net;
using RestSharp;

namespace SharpExchange.Chat.Actions.Message
{
	public class MessageDeleter : ChatAction
	{
		private readonly int messageId;

		internal override Method RequestMethod => Method.POST;

		internal override string Endpoint => $"https://{Host}/messages/{messageId}/delete";

		internal override bool RequiresFKey => true;

		internal override bool RequiresAuthCookies => true;

		internal override ActionPermissionLevel RequiredPermissionLevel => ActionPermissionLevel.Anyone;



		public MessageDeleter(int messageId)
		{
			if (messageId < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(messageId));
			}

			this.messageId = messageId;
		}



		internal override object ProcessResponse(HttpStatusCode status, string response)
		{
			return status == HttpStatusCode.OK &&
				response?.ToUpperInvariant() == "\"OK\"";
		}
	}
}
