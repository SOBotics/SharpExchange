using SharpExchange.Api.V22.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpExchange.Api.V22.Endpoints
{
	public static class Revisions
	{
		public static Task<Result<Revision[]>> GetByPostIds(IEnumerable<int> postIds, QueryOptions options = null)
		{
			postIds.ThrowIfNullOrEmpty(nameof(postIds));
			options = options.GetDefaultIfNull();

			var idsStr = postIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/posts/{idsStr}/revisions";

			return ApiRequestScheduler.ScheduleRequestAsync<Revision[]>(endpoint, options);
		}

		public static Task<Result<Revision[]>> GetByRevisionGuids(IEnumerable<string> revGuids, QueryOptions options = null)
		{
			revGuids.ThrowIfNullOrEmpty(nameof(revGuids));
			options = options.GetDefaultIfNull();

			var idsStr = revGuids.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/revisions/{idsStr}";

			return ApiRequestScheduler.ScheduleRequestAsync<Revision[]>(endpoint, options);
		}
	}
}
