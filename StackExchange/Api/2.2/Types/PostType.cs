using System.Runtime.Serialization;

namespace StackExchange.Api.V22.Types
{
	public enum PostType
	{
		[EnumMember(Value = "question")]
		Question,

		[EnumMember(Value = "answer")]
		Answer
	}
}
