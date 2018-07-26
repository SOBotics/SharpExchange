namespace SharpExchange.Api.V22
{
	public enum SortBy
	{
		[ApiQueryValue("creation_date")]
		CreationDate,

		[ApiQueryValue("votes")]
		Votes,

		[ApiQueryValue("last_activity_date")]
		PostAcivity,

		[ApiQueryValue("rank")]
		BadgeRank,

		[ApiQueryValue("name")]
		BadgeName,

		[ApiQueryValue("type")]
		BadgeType,

		[ApiQueryValue("approval_date")]
		EditApprovalDate,

		[ApiQueryValue("rejection_date")]
		EditRejectionDate,

		[ApiQueryValue("count")]
		TagPopularity,

		[ApiQueryValue("name")]
		TagName,
	}
}
