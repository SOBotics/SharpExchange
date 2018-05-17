namespace StackExchange.Api.V22
{
	public enum SortBy
	{
		[ApiQueryValue("activity")]
		Acivity,

		[ApiQueryValue("creation")]
		Creation,

		[ApiQueryValue("votes")]
		Votes
	}
}
