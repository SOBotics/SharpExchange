using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SharpExchange.Api.V22.Types
{
	public class Notice
	{
		[JsonProperty("body")]
		public string Body { get; internal set; }

		[JsonProperty("creation_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? CreatedOn { get; internal set; }

		[JsonProperty("owner_user_id")]
		public int? OwnerId { get; internal set; }
	}
}
