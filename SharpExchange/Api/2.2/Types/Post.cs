using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SharpExchange.Api.V22.Types
{
	public class Post
	{
		[JsonProperty("body")]
		public string Body { get; internal set; }

		[JsonProperty("body_markdown")]
		public string Markdown { get; internal set; }

		[JsonProperty("comment_count")]
		public int? CommentCount { get; internal set; }

		[JsonProperty("comments")]
		public Comment[] Comments { get; internal set; }

		[JsonProperty("creation_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? CreatedOn { get; internal set; }

		[JsonProperty("down_vote_count")]
		public int? DownvoteCount { get; internal set; }

		[JsonProperty("last_activity_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? LastActiveOn { get; internal set; }

		[JsonProperty("last_edit_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? LastEditedOn { get; internal set; }

		[JsonProperty("last_editor")]
		public ShallowUser LastEditor { get; internal set; }

		[JsonProperty("link")]
		public string Link { get; internal set; }

		[JsonProperty("owner")]
		public ShallowUser Owner { get; internal set; }

		[JsonProperty("post_id")]
		public int? Id { get; internal set; }

		[JsonProperty("post_type")]
		public PostType? PostType { get; internal set; }

		[JsonProperty("score")]
		public int? Score { get; internal set; }

		[JsonProperty("share_link")]
		public string ShareLink { get; internal set; }

		[JsonProperty("title")]
		public string Title { get; internal set; }

		[JsonProperty("up_vote_count")]
		public int? UpvoteCount { get; internal set; }
	}
}
