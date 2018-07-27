using SharpExchange.Api.V22.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpExchange.Api.V22.Endpoints
{
	public static class UserTimelines
	{
		/// <summary>
		/// Gets a subset of the actions of that have been taken by
		/// the users identified by a set of ids.
		/// </summary>
		public static Task<Result<UserTimeline[]>> GetByIdsAsync(IEnumerable<int> userIds, QueryOptions options = null)
		{
			userIds.ThrowIfNullOrEmpty(nameof(userIds));
			options = options.GetDefaultIfNull();

			var idsStr = userIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/users/{idsStr}/timeline";

			return ApiRequestScheduler.ScheduleRequestAsync<UserTimeline[]>(endpoint, options);
		}
	}
}
