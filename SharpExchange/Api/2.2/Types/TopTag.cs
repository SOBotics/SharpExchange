using Newtonsoft.Json;

namespace SharpExchange.Api.V22.Types
{
	public class TopTag
	{
		[JsonProperty("answer_count")]
		public int? AnswerCount { get; internal set; }

		[JsonProperty("answer_score")]
		public int? AnswerScore { get; internal set; }

		[JsonProperty("question_count")]
		public int? QuestionCount { get; internal set; }

		[JsonProperty("question_score")]
		public int? QuestionScore { get; internal set; }

		[JsonProperty("tag_name")]
		public string TagName { get; internal set; }

		[JsonProperty("user_id")]
		public int? UserId { get; internal set; }
	}
}
