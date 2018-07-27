using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SharpExchange.Api.V22.Types
{
	public class User : ShallowUser
	{
		[JsonProperty("about_me")]
		public string AboutMe { get; internal set; }

		[JsonProperty("answer_count")]
		public int? AnswerCount { get; internal set; }

		[JsonProperty("creation_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? CreatedOn { get; internal set; }

		[JsonProperty("down_vote_count")]
		public int? DownvoteCount { get; internal set; }

		[JsonProperty("is_employee")]
		public bool? IsEmployee { get; internal set; }

		[JsonProperty("last_access_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? LastAccessedOn { get; internal set; }

		[JsonProperty("last_modified_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? LastModifiedOn { get; internal set; }

		[JsonProperty("location")]
		public string Location { get; internal set; }

		[JsonProperty("question_count")]
		public int? QuestionCount { get; internal set; }

		[JsonProperty("reputation_change_day")]
		public int? ReputationChangeDay { get; internal set; }

		[JsonProperty("reputation_change_month")]
		public int? ReputationChangeMonth { get; internal set; }

		[JsonProperty("reputation_change_quarter")]
		public int? ReputationChangeQuarter { get; internal set; }

		[JsonProperty("reputation_change_week")]
		public int? ReputationChangeWeek { get; internal set; }

		[JsonProperty("reputation_change_year")]
		public int? ReputationChangeYear { get; internal set; }

		[JsonProperty("timed_penalty_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? SuspensionEndsOn { get; internal set; }

		[JsonProperty("up_vote_count")]
		public int? UpvoteCount { get; internal set; }

		[JsonProperty("view_count")]
		public int? ViewCount { get; internal set; }

		[JsonProperty("website_url")]
		public string WebsiteUrl { get; internal set; }
	}
}
