using SharpExchange.Api.V22.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpExchange.Api.V22.Endpoints
{
	public static class Revisions
	{
		/// <summary>
		/// Gets the revisions on the posts identified by a set of ids.
		/// </summary>
		public static Task<Result<Revision[]>> GetByPostIdsAsync(IEnumerable<int> postIds, QueryOptions options = null)
		{
			postIds.ThrowIfNullOrEmpty(nameof(postIds));
			options = options.GetDefaultIfNull();

			var idsStr = postIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/posts/{idsStr}/revisions";

			return ApiRequestScheduler.ScheduleRequestAsync<Revision[]>(endpoint, options);
		}

		/// <summary>
		/// Gets all revisions identified by a set of ids.
		/// </summary>
		public static Task<Result<Revision[]>> GetByRevisionGuidsAsync(IEnumerable<string> revGuids, QueryOptions options = null)
		{
			revGuids.ThrowIfNullOrEmpty(nameof(revGuids));
			options = options.GetDefaultIfNull();

			var idsStr = revGuids.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/revisions/{idsStr}";

			return ApiRequestScheduler.ScheduleRequestAsync<Revision[]>(endpoint, options);
		}
	}
}
