using System.Runtime.Serialization;

namespace SharpExchange.Api.V22.Types
{
	public enum VoteType
	{
		[EnumMember(Value = "accepts")]
		Accepts,

		[EnumMember(Value = "up_votes")]
		UpVotes,

		[EnumMember(Value = "down_votes")]
		DownVotes,

		[EnumMember(Value = "bounties_offered")]
		BountiesOffered,

		[EnumMember(Value = "bounties_won")]
		BountiesWon,

		[EnumMember(Value = "spam")]
		Spam,

		[EnumMember(Value = "suggested_edits")]
		SuggestedEdit
	}
}