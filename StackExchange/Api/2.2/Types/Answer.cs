using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StackExchange.Api.v22.Types
{
	public class Answer
	{
		[JsonProperty("answer_id")]
		public int? Id { get; set; }

		[JsonProperty("awarded_bounty_amount")]
		public int? TotalBounty { get; set; }

		[JsonProperty("awarded_bounty_users")]
		public ShallowUser[] BountyUsers { get; set; }

		[JsonProperty("body")]
		public string Body { get; set; }

		[JsonProperty("body_markdown")]
		public string Markdown { get; set; }

		[JsonProperty("can_flag")]
		public bool? CanFlag { get; set; }

		[JsonProperty("comment_count")]
		public int? CommentCount { get; set; }

		[JsonProperty("comments")]
		public Comment[] Comments { get; set; }

		[JsonProperty("community_owned_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? TurnedIntoCommunityWikiOn;

		[JsonProperty("creation_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? CreatedOn { get; set; }

		[JsonProperty("down_vote_count")]
		public int? Downvotes { get; set; }

		[JsonProperty("is_accepted")]
		public bool? Accepted { get; set; }

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

		[JsonProperty("locked_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? LockedOn { get; set; }

		[JsonProperty("owner")]
		public ShallowUser Owner { get; set; }

		[JsonProperty("question_id")]
		public int? QuestionId { get; set; }

		[JsonProperty("score")]
		public int? Score { get; set; }

		[JsonProperty("share_link")]
		public string ShareLink { get; set; }

		[JsonProperty("tags")]
		public string[] Tags { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("up_vote_count")]
		public int? Upvotes { get; set; }
	}
}
