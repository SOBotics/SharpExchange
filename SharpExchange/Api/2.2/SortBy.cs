namespace SharpExchange.Api.V22
{
	public enum SortBy
	{
		[ApiQueryValue("creation")]
		Creation,

		[ApiQueryValue("votes")]
		Votes,

		[ApiQueryValue("activity")]
		PostAcivity,

		[ApiQueryValue("rank")]
		BadgeRank,

		[ApiQueryValue("name")]
		BadgeName,

		[ApiQueryValue("type")]
		BadgeType
	}
}
