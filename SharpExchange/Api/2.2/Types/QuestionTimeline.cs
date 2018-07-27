using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SharpExchange.Api.V22.Types
{
	public class QuestionTimeline
	{
		[JsonProperty("comment_id")]
		public int? CommentId { get; internal set; }

		[JsonProperty("creation_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? CreatedOn { get; internal set; }

		[JsonProperty("down_vote_count")]
		public int? DownVoteCount { get; internal set; }

		[JsonProperty("owner")]
		public ShallowUser Owner { get; internal set; }

		[JsonProperty("post_id")]
		public int? PostId { get; internal set; }

		[JsonProperty("question_id")]
		public int? QuestionId { get; internal set; }

		[JsonProperty("revision_guid")]
		public string RevisionGuid { get; internal set; }

		[JsonProperty("timeline_type")]
		public QuestionTimelineType? TimelineType { get; internal set; }

		[JsonProperty("up_vote_count")]
		public int? UpVoteCount { get; internal set; }

		[JsonProperty("user")]
		public ShallowUser User { get; internal set; }
	}
}
