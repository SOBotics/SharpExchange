using System.Collections.Generic;
using System.Threading.Tasks;
using SharpExchange.Api.V22.Types;

namespace SharpExchange.Api.V22.Endpoints
{
	public static class Comments
	{
		/// <summary>
		/// Gets all comments on a site.
		/// </summary>
		public static Task<Result<Comment[]>> GetAllAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/comments";

			return ApiRequestScheduler.ScheduleRequestAsync<Comment[]>(endpoint, options);
		}

		/// <summary>
		/// Gets comments identified by a set of ids.
		/// </summary>
		public static Task<Result<Comment[]>> GetByIdsAsync(IEnumerable<int> ids, QueryOptions options = null)
		{
			ids.ThrowIfNullOrEmpty(nameof(ids));
			options = options.GetDefaultIfNull();

			var idsStr = ids.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/comments/{idsStr}";

			return ApiRequestScheduler.ScheduleRequestAsync<Comment[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the comments on the posts identified by a set of ids.
		/// </summary>
		public static Task<Result<Comment[]>> GetByPostIdsAsync(IEnumerable<int> postIds, QueryOptions options = null)
		{
			postIds.ThrowIfNullOrEmpty(nameof(postIds));
			options = options.GetDefaultIfNull();

			var idsStr = postIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/posts/{idsStr}/comments";

			return ApiRequestScheduler.ScheduleRequestAsync<Comment[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the comments posted by the users identified by a set of ids.
		/// </summary>
		public static Task<Result<Comment[]>> GetByUserIdsAsync(IEnumerable<int> userIds, QueryOptions options = null)
		{
			userIds.ThrowIfNullOrEmpty(nameof(userIds));
			options = options.GetDefaultIfNull();

			var idsStr = userIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/users/{idsStr}/comments";

			return ApiRequestScheduler.ScheduleRequestAsync<Comment[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the comments that mention one of the users identified by a set of ids.
		/// </summary>
		public static Task<Result<Comment[]>> GetMentionsByUserIdsAsync(IEnumerable<int> userIds, QueryOptions options = null)
		{
			userIds.ThrowIfNullOrEmpty(nameof(userIds));
			options = options.GetDefaultIfNull();

			var idsStr = userIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/users/{idsStr}/mentioned";

			return ApiRequestScheduler.ScheduleRequestAsync<Comment[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the comments posted by a set of users in reply to another user.
		/// </summary>
		public static Task<Result<Comment[]>> GetRepliesByUserIdsAsync(IEnumerable<int> authorIds, int recipientUser, QueryOptions options = null)
		{
			authorIds.ThrowIfNullOrEmpty(nameof(authorIds));
			options = options.GetDefaultIfNull();

			var idsStr = authorIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/users/{idsStr}/comments/{recipientUser}";

			return ApiRequestScheduler.ScheduleRequestAsync<Comment[]>(endpoint, options);
		}
	}
}
