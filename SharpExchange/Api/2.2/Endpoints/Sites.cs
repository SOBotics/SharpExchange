using SharpExchange.Api.V22.Types;
using System.Threading.Tasks;

namespace SharpExchange.Api.V22.Endpoints
{
	public static class Sites
	{
		/// <summary>
		/// Gets all the sites in the Stack Exchange network.
		/// </summary>
		public static Task<Result<Site[]>> GetAllAsync(QueryOptions options = null)
		{
			var endpoint = $"{Constants.BaseApiUrl}/sites";

			return ApiRequestScheduler.ScheduleRequestAsync<Site[]>(endpoint, options);
		}
	}
}
