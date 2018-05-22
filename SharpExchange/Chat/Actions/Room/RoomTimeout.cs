using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;

namespace SharpExchange.Chat.Actions.Room
{
	public class RoomTimeout : ChatAction
	{
		internal override Method RequestMethod => Method.POST;

		internal override string Endpoint => $"https://{Host}/rooms/timeout/{RoomId}";

		internal override bool RequiresFKey => true;

		internal override bool RequiresAuthCookies => true;

		internal override ActionPermissionLevel RequiredPermissionLevel => ActionPermissionLevel.RoomOwner;



		public RoomTimeout(int durationSeconds, string reason)
		{
			if (durationSeconds < 5)
			{
				throw new ArgumentOutOfRangeException(nameof(durationSeconds), "Must be more than or equal to 5.");
			}

			reason.ThrowIfNullOrEmpty(nameof(reason));

			Data = new Dictionary<string, object>
			{
				["duration"] = durationSeconds,
				["reason"] = reason
			};
		}



		internal override object ProcessResponse(HttpStatusCode status, string response)
		{
			return status == HttpStatusCode.OK &&
				response?.ToUpperInvariant() == "TIMEOUT APPLIED";
		}
	}
}
