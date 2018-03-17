using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		

		internal override object ProcessResponse(string response)
		{
			//TODO: Finish this off.
			return -1;
		}
	}
}
