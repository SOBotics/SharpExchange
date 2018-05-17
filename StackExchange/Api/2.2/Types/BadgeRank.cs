using System.Runtime.Serialization;

namespace StackExchange.Api.V22.Types
{
	public enum BadgeRank
	{
		[EnumMember(Value = "gold")]
		Gold,

		[EnumMember(Value = "silver")]
		Silver,

		[EnumMember(Value = "bronze")]
		Brozne
	}
}
