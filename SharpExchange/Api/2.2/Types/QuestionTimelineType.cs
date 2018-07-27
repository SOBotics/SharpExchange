using System.Runtime.Serialization;

namespace SharpExchange.Api.V22.Types
{
	public enum QuestionTimelineType
	{
		[EnumMember(Value = "question")]
		Question,

		[EnumMember(Value = "answer")]
		Answer,

		[EnumMember(Value = "comment")]
		Comment,

		[EnumMember(Value = "unaccepted_answer")]
		UnacceptedAnswer,

		[EnumMember(Value = "accepted_answer")]
		AcceptedAnswer,

		[EnumMember(Value = "vote_aggregate")]
		VoteAggregate,

		[EnumMember(Value = "revision")]
		Revision,

		[EnumMember(Value = "post_state_changed")]
		PostStateChanged
	}
}
