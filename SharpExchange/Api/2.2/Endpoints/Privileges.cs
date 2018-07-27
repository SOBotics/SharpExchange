using SharpExchange.Api.V22.Types;
using System.Threading.Tasks;

namespace SharpExchange.Api.V22.Endpoints
{
	public static class Privileges
	{
		/// <summary>
		/// Gets all the privileges available on a site.
		/// </summary>
		public static Task<Result<Privilege[]>> GetAllAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/privileges";

			return ApiRequestScheduler.ScheduleRequestAsync<Privilege[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the privileges the given user has on a site.
		/// </summary>
		public static Task<Result<Privilege[]>> GetByUserIdAsync(int userId, QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/users/{userId}/privileges";

			return ApiRequestScheduler.ScheduleRequestAsync<Privilege[]>(endpoint, options);
		}
	}
}
