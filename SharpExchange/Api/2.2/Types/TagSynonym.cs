using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SharpExchange.Api.V22.Types
{
	public class TagSynonym
	{
		[JsonProperty("applied_count")]
		public int? AppliedCount { get; internal set; }

		[JsonProperty("creation_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? CreatedOn { get; internal set; }

		[JsonProperty("from_tag")]
		public string FromTag { get; internal set; }

		[JsonProperty("last_applied_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? LastAppliedOn { get; internal set; }

		[JsonProperty("to_tag")]
		public string ToTag { get; internal set; }
	}
}
