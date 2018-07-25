using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SharpExchange.Api.V22.Types
{
	public class Revision
	{
		[JsonProperty("body")]
		public string Body { get; internal set; }

		[JsonProperty("comment")]
		public string Comment { get; internal set; }

		[JsonProperty("creation_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? CreatedOn { get; internal set; }

		[JsonProperty("is_rollback")]
		public bool? IsRollback { get; internal set; }

		[JsonProperty("last_body")]
		public string LastBody { get; internal set; }

		[JsonProperty("last_tags")]
		public string[] LastTags { get; internal set; }

		[JsonProperty("last_title")]
		public string LastTitle { get; internal set; }

		[JsonProperty("post_id")]
		public int? PostId { get; internal set; }

		[JsonProperty("post_type")]
		public PostType? PostType { get; internal set; }

		[JsonProperty("revision_guid")]
		public string RevisionGuid { get; internal set; }

		[JsonProperty("revision_number")]
		public int? RevisionNumber { get; internal set; }

		[JsonProperty("revision_type")]
		public RevisionType? RevisionType { get; internal set; }

		[JsonProperty("set_community_wiki")]
		public bool? SetCommunityWiki { get; internal set; }

		[JsonProperty("tags")]
		public string[] Tags { get; internal set; }

		[JsonProperty("title")]
		public string Title { get; internal set; }

		[JsonProperty("user")]
		public ShallowUser User { get; internal set; }
	}
}
