using System.Runtime.Serialization;

namespace SharpExchange.Api.V22.Types
{
	public enum UserType
	{
		[EnumMember(Value = "unregistered")]
		Unregistered,

		[EnumMember(Value = "registered")]
		Registered,

		[EnumMember(Value = "moderator")]
		Moderator,

		[EnumMember(Value = "team_admin")]
		TeamAdmin,

		[EnumMember(Value = "does_not_exist")]
		Deleted
	}
}
