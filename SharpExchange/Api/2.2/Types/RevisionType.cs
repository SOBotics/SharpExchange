using System.Runtime.Serialization;

namespace SharpExchange.Api.V22.Types
{
	public enum RevisionType
	{
		[EnumMember(Value = "single_user")]
		SingleUser,

		[EnumMember(Value = "vote_based")]
		VoteBased
	}
}
