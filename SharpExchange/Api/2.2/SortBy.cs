namespace SharpExchange.Api.V22
{
	public enum SortBy
	{
		[ApiQueryValue("creation")]
		CreationDate,

		[ApiQueryValue("votes")]
		Votes,

		[ApiQueryValue("activity")]
		PostActivity,

		[ApiQueryValue("name")]
		BadgeName,

		[ApiQueryValue("rank")]
		BadgeRank,

		[ApiQueryValue("type")]
		BadgeType,

		[ApiQueryValue("hot")]
		QuestionHot,

		[ApiQueryValue("week")]
		QuestionWeek,

		[ApiQueryValue("month")]
		QuestionMonth,

		[ApiQueryValue("approval")]
		SuggestedEditApprovalDate,

		[ApiQueryValue("rejection")]
		SuggestedEditRejectionDate,

		[ApiQueryValue("count")]
		TagPopularity,

		[ApiQueryValue("activity")]
		TagActivity,

		[ApiQueryValue("name")]
		TagName,

		[ApiQueryValue("applied")]
		TagSynonymAppliedCount,

		[ApiQueryValue("activity")]
		TagSynonymActivity,

		[ApiQueryValue("reputation")]
		UserReputation,

		[ApiQueryValue("name")]
		UserName,

		[ApiQueryValue("modified")]
		UserLastModifiedDate,
	}
}
