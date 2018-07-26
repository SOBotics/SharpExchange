using SharpExchange.Api.V22.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpExchange.Api.V22.Endpoints
{
	public static class SuggestedEdits
	{
		/// <summary>
		/// Gets all the suggested edits on a site.
		/// </summary>
		public static Task<Result<SuggestedEdit[]>> GetAllAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/suggested-edits";

			return ApiRequestScheduler.ScheduleRequestAsync<SuggestedEdit[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the suggested edits identified by a set of ids.
		/// </summary>
		public static Task<Result<SuggestedEdit[]>> GetBySuggestedEditIdsAsync(IEnumerable<int> suggestedEditIds, QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var idsStr = suggestedEditIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/suggested-edits/{idsStr}";

			return ApiRequestScheduler.ScheduleRequestAsync<SuggestedEdit[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the suggested edits on the set of posts identified by a set of ids.
		/// </summary>
		public static Task<Result<SuggestedEdit[]>> GetByPostIdsAsync(IEnumerable<int> postIds, QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var idsStr = postIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/posts/{idsStr}/suggested-edits";

			return ApiRequestScheduler.ScheduleRequestAsync<SuggestedEdit[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the suggested edits provided by users identified by a set of ids.
		/// </summary>
		public static Task<Result<SuggestedEdit[]>> GetByUserIdsAsync(IEnumerable<int> userIds, QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var idsStr = userIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/users/{idsStr}/suggested-edits";

			return ApiRequestScheduler.ScheduleRequestAsync<SuggestedEdit[]>(endpoint, options);
		}
	}
}
