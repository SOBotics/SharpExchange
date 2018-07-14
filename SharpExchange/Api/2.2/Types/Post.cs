using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SharpExchange.Api.V22.Types
{
	public class Post
	{
		[JsonProperty("body")]
		public string Body { get; set; }

		[JsonProperty("body_markdown")]
		public string Markdown { get; set; }

		[JsonProperty("comment_count")]
		public int? CommentCount { get; set; }

		[JsonProperty("comments")]
		public Comment[] Comments { get; set; }

		[JsonProperty("creation_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? CreatedOn { get; set; }

		[JsonProperty("down_vote_count")]
		public int? Downvotes { get; set; }

		[JsonProperty("last_activity_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? LastActiveOn { get; set; }

		[JsonProperty("last_edit_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? LastEditedOn { get; set; }

		[JsonProperty("last_editor")]
		public ShallowUser LastEditor { get; set; }

		[JsonProperty("link")]
		public string Link { get; set; }

		[JsonProperty("owner")]
		public ShallowUser Owner { get; set; }

		[JsonProperty("post_id")]
		public int? Id { get; set; }

		[JsonProperty("post_type")]
		public PostType? PostType { get; set; }

		[JsonProperty("score")]
		public int? Score { get; set; }

		[JsonProperty("share_link")]
		public string ShareLink { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("up_vote_count")]
		public int? Upvotes { get; set; }
	}
}
