using SharpExchange.Api.V22.Types;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SharpExchange.Api.V22.Endpoints
{
	public static class Tags
	{
		/// <summary>
		/// Gets all the tags on a site.
		/// </summary>
		public static Task<Result<Tag[]>> GetAllAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/tags";

			return ApiRequestScheduler.ScheduleRequestAsync<Tag[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the tags on the site that only moderators can use.
		/// </summary>
		public static Task<Result<Tag[]>> GetModeratorOnlyAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/tags/moderator-only";

			return ApiRequestScheduler.ScheduleRequestAsync<Tag[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the tags on the site that fulfil required tag constraints.
		/// </summary>
		public static Task<Result<Tag[]>> GetRequiredAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/tags/required";

			return ApiRequestScheduler.ScheduleRequestAsync<Tag[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the tags on a site by their names.
		/// </summary>
		public static Task<Result<Tag[]>> GetByNameAsync(IEnumerable<string> tagNames, QueryOptions options = null)
		{
			tagNames.ThrowIfNullOrEmpty(nameof(tagNames));
			options = options.GetDefaultIfNull();

			var tagList = tagNames
				.Select(WebUtility.UrlEncode)
				.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/tags/{tagList}/info";

			return ApiRequestScheduler.ScheduleRequestAsync<Tag[]>(endpoint, options);
		}

		/// <summary>
		/// Gets related tags, based on common tag pairings.
		/// </summary>
		public static Task<Result<Tag[]>> GetRelatedAsync(IEnumerable<string> tagNames, QueryOptions options = null)
		{
			tagNames.ThrowIfNullOrEmpty(nameof(tagNames));
			options = options.GetDefaultIfNull();

			var tagList = tagNames
				.Select(WebUtility.UrlEncode)
				.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/tags/{tagList}/related";

			return ApiRequestScheduler.ScheduleRequestAsync<Tag[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the tags that the users (identified by a set of ids) have been active in.
		/// </summary>
		public static Task<Result<Tag[]>> GetByUserIdsAsync(IEnumerable<int> userIds, QueryOptions options = null)
		{
			userIds.ThrowIfNullOrEmpty(nameof(userIds));
			options = options.GetDefaultIfNull();

			var idsStr = userIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/users/{idsStr}/tags";

			return ApiRequestScheduler.ScheduleRequestAsync<Tag[]>(endpoint, options);
		}
	}
}
