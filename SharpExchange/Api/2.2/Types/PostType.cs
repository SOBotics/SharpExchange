using System.Runtime.Serialization;

namespace SharpExchange.Api.V22.Types
{
	public enum PostType
	{
		[EnumMember(Value = "question")]
		Question,

		[EnumMember(Value = "answer")]
		Answer
	}
}
