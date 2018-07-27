using System.Collections.Generic;
using System.Threading.Tasks;
using SharpExchange.Api.V22.Types;

namespace SharpExchange.Api.V22.Endpoints
{
	public static class Posts
	{
		/// <summary>
		/// Gets all posts on a site.
		/// </summary>
		public static Task<Result<Post[]>> GetAllAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/posts";

			return ApiRequestScheduler.ScheduleRequestAsync<Post[]>(endpoint, options);
		}

		/// <summary>
		/// Gets posts identified by a set of ids.
		/// </summary>
		public static Task<Result<Post[]>> GetByIdsAsync(IEnumerable<int> ids, QueryOptions options = null)
		{
			ids.ThrowIfNullOrEmpty(nameof(ids));
			options = options.GetDefaultIfNull();

			var idsStr = ids.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/posts/{idsStr}";

			return ApiRequestScheduler.ScheduleRequestAsync<Post[]>(endpoint, options);
		}

		/// <summary>
		/// Gets all posts owned by the users identified by a set of ids.
		/// </summary>
		public static Task<Result<Post[]>> GetByUserIdsAsync(IEnumerable<int> userIds, QueryOptions options = null)
		{
			userIds.ThrowIfNullOrEmpty(nameof(userIds));
			options = options.GetDefaultIfNull();

			var idsStr = userIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/users/{idsStr}/posts";

			return ApiRequestScheduler.ScheduleRequestAsync<Post[]>(endpoint, options);
		}
	}
}
