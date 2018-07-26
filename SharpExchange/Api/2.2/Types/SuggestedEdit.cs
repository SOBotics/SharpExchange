using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SharpExchange.Api.V22.Types
{
	public class SuggestedEdit
	{
		[JsonProperty("approval_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? ApprovedOn { get; internal set; }

		[JsonProperty("body")]
		public string Body { get; internal set; }

		[JsonProperty("comment")]
		public string Comment { get; internal set; }

		[JsonProperty("creation_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? CreatedOn { get; internal set; }

		[JsonProperty("post_id")]
		public int? PostId { get; internal set; }

		[JsonProperty("post_type")]
		public PostType? PostType { get; internal set; }

		[JsonProperty("proposing_user")]
		public ShallowUser Author { get; internal set; }

		[JsonProperty("rejection_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? RejectedOn { get; internal set; }

		[JsonProperty("suggested_edit_id")]
		public int SuggestedEditId { get; internal set; }

		[JsonProperty("tags")]
		public string[] Tags { get; internal set; }

		[JsonProperty("title")]
		public string Title { get; internal set; }
	}
}
