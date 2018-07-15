using Newtonsoft.Json;

namespace SharpExchange.Api.V22.Types
{
	public class OriginalQuestion
	{
		[JsonProperty("accepted_answer_id")]
		public int? AcceptedAnswerId { get; internal set; }

		[JsonProperty("answer_count")]
		public int? AnswerCount { get; internal set; }

		[JsonProperty("question_id")]
		public int? QuestionId { get; internal set; }

		[JsonProperty("title")]
		public string Title { get; internal set; }
	}
}
