using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StackExchange.Api.v22.Types
{
	public class Comment
	{
		[JsonProperty("body")]
		public string Body { get; set; }

		[JsonProperty("body_markdown")]
		public string Markdown { get; set; }

		[JsonProperty("can_flag")]
		public bool? CanFlag { get; set; }

		[JsonProperty("comment_id")]
		public int? Id { get; set; }

		[JsonProperty("creation_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime CreatedOn { get; set; }

		[JsonProperty("edited")]
		public bool? Edited { get; set; }

		[JsonProperty("link")]
		public string Link { get; set; }

		[JsonProperty("owner")]
		public ShallowUser Owner { get; set; }

		[JsonProperty("post_id")]
		public int? ParentPostId { get; set; }

		[JsonProperty("post_type")]
		public PostType? ParentPostType { get; set; }

		[JsonProperty("reply_to_user")]
		public ShallowUser RepliesTo { get; set; }

		[JsonProperty("score")]
		public int? Score { get; set; }
	}
}
