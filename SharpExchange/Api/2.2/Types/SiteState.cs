using System.Runtime.Serialization;

namespace SharpExchange.Api.V22.Types
{
	public enum SiteState
	{
		[EnumMember(Value = "normal")]
		Normal,

		[EnumMember(Value = "closed_beta")]
		ClosedBeta,

		[EnumMember(Value = "open_beta")]
		OpenBeta,

		[EnumMember(Value = "linked_meta")]
		Meta
	}
}
