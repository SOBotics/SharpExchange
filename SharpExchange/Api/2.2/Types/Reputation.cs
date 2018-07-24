using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SharpExchange.Api.V22.Types
{
	public class Reputation
	{
		[JsonProperty("link")]
		public string Link { get; internal set; }

		[JsonProperty("on_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? Timestamp { get; internal set; }

		[JsonProperty("post_id")]
		public int? PostId { get; internal set; }

		[JsonProperty("post_type")]
		public PostType? PostType { get; internal set; }

		[JsonProperty("reputation_change")]
		public int? ChangeAmount { get; internal set; }

		[JsonProperty("title")]
		public string Title { get; internal set; }

		[JsonProperty("user_id")]
		public int? UserId { get; internal set; }

		[JsonProperty("vote_type")]
		public VoteType? VoteType { get; internal set; }
	}
}
