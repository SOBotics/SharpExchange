using System.Runtime.Serialization;

namespace StackExchange.Api.v22.Types
{
	public enum PostType
	{
		[EnumMember(Value = "question")]
		Question,

		[EnumMember(Value = "answer")]
		Answer
	}
}
