using System.Runtime.Serialization;

namespace SharpExchange.Api.V22.Types
{
	public enum BadgeType
	{
		[EnumMember(Value = "named")]
		Named,

		[EnumMember(Value = "tag_based")]
		TagBased
	}
}
