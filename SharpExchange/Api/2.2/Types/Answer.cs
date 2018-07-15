using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SharpExchange.Api.V22.Types
{
	public class Answer
	{
		[JsonProperty("answer_id")]
		public int? Id { get; internal set; }

		[JsonProperty("awarded_bounty_amount")]
		public int? TotalBounty { get; internal set; }

		[JsonProperty("awarded_bounty_users")]
		public ShallowUser[] BountyUsers { get; internal set; }

		[JsonProperty("body")]
		public string Body { get; internal set; }

		[JsonProperty("body_markdown")]
		public string Markdown { get; internal set; }

		[JsonProperty("can_flag")]
		public bool? CanFlag { get; internal set; }

		[JsonProperty("comment_count")]
		public int? CommentCount { get; internal set; }

		[JsonProperty("comments")]
		public Comment[] Comments { get; internal set; }

		[JsonProperty("community_owned_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? TurnedIntoCommunityWikiOn { get; internal set; }

		[JsonProperty("creation_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? CreatedOn { get; internal set; }

		[JsonProperty("down_vote_count")]
		public int? DownvoteCount { get; internal set; }

		[JsonProperty("is_accepted")]
		public bool? Accepted { get; internal set; }

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

		[JsonProperty("locked_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? LockedOn { get; internal set; }

		[JsonProperty("owner")]
		public ShallowUser Owner { get; internal set; }

		[JsonProperty("question_id")]
		public int? QuestionId { get; internal set; }

		[JsonProperty("score")]
		public int? Score { get; internal set; }

		[JsonProperty("share_link")]
		public string ShareLink { get; internal set; }

		[JsonProperty("tags")]
		public string[] Tags { get; internal set; }

		[JsonProperty("title")]
		public string Title { get; internal set; }

		[JsonProperty("up_vote_count")]
		public int? UpvoteCount { get; internal set; }
	}
}
