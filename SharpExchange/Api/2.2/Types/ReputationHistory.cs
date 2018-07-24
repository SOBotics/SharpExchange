using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SharpExchange.Api.V22.Types
{
	public class ReputationHistory
	{
		[JsonProperty("creation_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? Timestamp { get; internal set; }

		[JsonProperty("post_id")]
		public int? PostId { get; internal set; }

		[JsonProperty("reputation_change")]
		public int? ChangeAmount { get; internal set; }

		[JsonProperty("user_id")]
		public int? UserId { get; internal set; }
	}
}
