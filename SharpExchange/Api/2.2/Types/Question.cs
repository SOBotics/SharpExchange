using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SharpExchange.Api.V22.Types
{
	public class Question
	{
		[JsonProperty("accepted_answer_id")]
		public int? AceptedAnswerId { get; internal set; }

		[JsonProperty("answer_count")]
		public int? AnswerCount { get; internal set; }

		[JsonProperty("answers")]
		public Answer[] Answers { get; internal set; }

		[JsonProperty("body")]
		public string Body { get; internal set; }

		[JsonProperty("body_markdown")]
		public string Markdown { get; internal set; }

		[JsonProperty("bounty_amount")]
		public int? TotalBounty { get; internal set; }

		[JsonProperty("bounty_closes_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? BountyClosesOn { get; internal set; }

		[JsonProperty("bounty_user")]
		public ShallowUser BountyUser { get; internal set; }

		[JsonProperty("can_flag")]
		public bool? CanFlag { get; internal set; }

		[JsonProperty("can_close")]
		public bool? CanClose { get; internal set; }

		[JsonProperty("close_vote_count")]
		public int? CloseVoteCount { get; internal set; }

		[JsonProperty("closed_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? ClosedOn { get; internal set; }

		[JsonProperty("closed_details")]
		public ClosedDetails ClosedDetails { get; internal set; }

		[JsonProperty("closed_reason")]
		public string ClosedReason { get; internal set; }

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

		[JsonProperty("delete_vote_count")]
		public int? DeleteVoteCount { get; internal set; }

		[JsonProperty("down_vote_count")]
		public int? DownvoteCount { get; internal set; }

		[JsonProperty("favorite_count")]
		public int? FavoriteCount { get; internal set; }

		[JsonProperty("is_answered")]
		public bool? IsAnswered { get; internal set; }

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

		[JsonProperty("migrated_from")]
		public MigrationInfo MigratedFrom { get; internal set; }

		[JsonProperty("migrated_to")]
		public MigrationInfo MigratedTo { get; internal set; }

		[JsonProperty("notice")]
		public Notice Notice { get; internal set; }

		[JsonProperty("owner")]
		public ShallowUser Owner { get; internal set; }

		[JsonProperty("protected_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? ProtectedOn { get; internal set; }

		[JsonProperty("question_id")]
		public int? Id { get; internal set; }

		[JsonProperty("reopen_vote_count")]
		public int? ReopenVoteCount { get; internal set; }

		[JsonProperty("score")]
		public int? Score { get; internal set; }

		[JsonProperty("share_link")]
		public string ShareLink { get; internal set; }

		[JsonProperty("tags")]
		public string[] Tags { get; internal set; }

		[JsonProperty("title")]
		public string Title { get; internal set; }

		[JsonProperty("up_vote_count")]
		public int? UpVoteCount { get; internal set; }

		[JsonProperty("view_count")]
		public int? ViewCount { get; internal set; }
	}
}
