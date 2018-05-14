namespace StackExchange.Api.v22
{
	public enum SortBy
	{
		[ApiQueryValue("activity")]
		Acivity,

		[ApiQueryValue("creation")]
		Creation,

		[ApiQueryValue("vote")]
		Votes
	}
}
