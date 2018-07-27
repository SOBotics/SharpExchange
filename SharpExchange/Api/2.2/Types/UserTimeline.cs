using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SharpExchange.Api.V22.Types
{
	public class UserTimeline
	{
		[JsonProperty("badge_id")]
		public int? BadgeId { get; internal set; }

		[JsonProperty("comment_id")]
		public int? CommentId { get; internal set; }

		[JsonProperty("creation_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? CreatedOn { get; internal set; }

		[JsonProperty("detail")]
		public string Details { get; internal set; }

		[JsonProperty("link")]
		public string Link { get; internal set; }

		[JsonProperty("post_id")]
		public int? PostId { get; internal set; }

		[JsonProperty("post_type")]
		public PostType PostType { get; internal set; }

		[JsonProperty("suggested_edit_id")]
		public int? SuggestedEditid { get; internal set; }

		[JsonProperty("timeline_type")]
		public UserTimelineType? TimelineType { get; internal set; }

		[JsonProperty("title")]
		public string Title { get; internal set; }

		[JsonProperty("user_id")]
		public int? UserId { get; internal set; }
	}
}
