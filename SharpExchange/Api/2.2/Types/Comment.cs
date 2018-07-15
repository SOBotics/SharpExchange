using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SharpExchange.Api.V22.Types
{
	public class Comment
	{
		[JsonProperty("body")]
		public string Body { get; internal set; }

		[JsonProperty("body_markdown")]
		public string Markdown { get; internal set; }

		[JsonProperty("can_flag")]
		public bool? CanFlag { get; internal set; }

		[JsonProperty("comment_id")]
		public int? Id { get; internal set; }

		[JsonProperty("creation_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime CreatedOn { get; internal set; }

		[JsonProperty("edited")]
		public bool? Edited { get; internal set; }

		[JsonProperty("link")]
		public string Link { get; internal set; }

		[JsonProperty("owner")]
		public ShallowUser Owner { get; internal set; }

		[JsonProperty("post_id")]
		public int? ParentPostId { get; internal set; }

		[JsonProperty("post_type")]
		public PostType? ParentPostType { get; internal set; }

		[JsonProperty("reply_to_user")]
		public ShallowUser RepliesTo { get; internal set; }

		[JsonProperty("score")]
		public int? Score { get; internal set; }
	}
}
