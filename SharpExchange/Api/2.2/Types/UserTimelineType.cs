using System.Runtime.Serialization;

namespace SharpExchange.Api.V22.Types
{
	public enum UserTimelineType
	{
		[EnumMember(Value = "commented")]
		Commented,

		[EnumMember(Value = "asked")]
		Asked,

		[EnumMember(Value = "answered")]
		Answered,

		[EnumMember(Value = "badge")]
		Badge,

		[EnumMember(Value = "revision")]
		Revision,

		[EnumMember(Value = "accepted")]
		Accepted,

		[EnumMember(Value = "reviewed")]
		Reviewed,

		[EnumMember(Value = "suggested")]
		Suggested
	}
}
