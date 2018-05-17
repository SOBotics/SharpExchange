namespace StackExchange.Api.V22
{
	public enum SortBy
	{
		[ApiQueryValue("activity")]
		PostAcivity,

		[ApiQueryValue("creation")]
		PostCreation,

		[ApiQueryValue("votes")]
		PostVotes,

		[ApiQueryValue("rank")]
		BadgeRank,

		[ApiQueryValue("name")]
		BadgeName,

		[ApiQueryValue("type")]
		BadgeType
	}
}
