using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using SharpExchange.Api.V22.Types;

namespace SharpExchange.Api.V22.Endpoints
{
	public static class Badges
	{
		/// <summary>
		/// Gets all badges on a site, in alphabetical order.
		/// </summary>
		public static Task<Result<Badge[]>> GetAllAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/badges";

			return ApiRequestScheduler.ScheduleRequestAsync<Badge[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the badges whose name contains the specified
		/// search term, in alphabetical order.
		/// </summary>
		public static Task<Result<Badge[]>> GetByNameAsync(string name, QueryOptions options = null)
		{
			name.ThrowIfNullOrEmpty(nameof(name));
			options = options.GetDefaultIfNull();

			options.Custom = new Dictionary<string, string>
			{
				["inname"] = WebUtility.UrlEncode(name)
			};

			var endpoint = $"{Constants.BaseApiUrl}/badges";

			return ApiRequestScheduler.ScheduleRequestAsync<Badge[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the badges identified by a set of ids.
		/// </summary>
		public static Task<Result<Badge[]>> GetByIdsAsync(IEnumerable<int> ids, QueryOptions options = null)
		{
			ids.ThrowIfNullOrEmpty(nameof(ids));
			options = options.GetDefaultIfNull();

			var idsStr = ids.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/badges/{idsStr}";

			return ApiRequestScheduler.ScheduleRequestAsync<Badge[]>(endpoint, options);
		}

		/// <summary>
		/// Returns recently awarded badges in the system, constrained
		/// to a set of badge IDs. As these badges have been awarded,
		/// they will have the <see cref="Badge.User"/> property set.
		/// 
		/// Does not support the following <see cref="QueryOptions"/> properties:
		/// <see cref="QueryOptions.Sort"/>, <see cref="QueryOptions.Order"/>,
		/// <see cref="QueryOptions.Max"/>, <see cref="QueryOptions.Min"/>.
		/// </summary>
		public static Task<Result<Badge[]>> GetRecentByIdsAsync(IEnumerable<int> ids, QueryOptions options = null)
		{
			ids.ThrowIfNullOrEmpty(nameof(ids));
			options = options.GetDefaultIfNull();

			var idsStr = ids.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/badges/{idsStr}/recipients";

			return ApiRequestScheduler.ScheduleRequestAsync<Badge[]>(endpoint, options);
		}

		/// <summary>
		/// Get all non-tagged-based badges in alphabetical order.
		/// </summary>
		public static Task<Result<Badge[]>> GetNamedAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/badges/name";

			return ApiRequestScheduler.ScheduleRequestAsync<Badge[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the non-tagged-based badges whose name contains
		/// the specified search term, in alphabetical order.
		/// </summary>
		public static Task<Result<Badge[]>> GetNamedByNameAsync(string name, QueryOptions options = null)
		{
			name.ThrowIfNullOrEmpty(nameof(name));
			options = options.GetDefaultIfNull();

			options.Custom = new Dictionary<string, string>
			{
				["inname"] = WebUtility.UrlEncode(name)
			};

			var endpoint = $"{Constants.BaseApiUrl}/badges/name";

			return ApiRequestScheduler.ScheduleRequestAsync<Badge[]>(endpoint, options);
		}

		/// <summary>
		/// Returns recently awarded badges in the system. As these
		/// badges have been awarded, they will have the
		/// <see cref="Badge.User"/> property set.
		/// </summary>
		public static Task<Result<Badge[]>> GetRecentAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/badges/recipients";

			return ApiRequestScheduler.ScheduleRequestAsync<Badge[]>(endpoint, options);
		}

		/// <summary>
		/// Get all tagged-based badges in alphabetical order.
		/// </summary>
		public static Task<Result<Badge[]>> GetTagsAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/badges/tags";

			return ApiRequestScheduler.ScheduleRequestAsync<Badge[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the tagged-based badges whose name contains
		/// the specified search term, in alphabetical order.
		/// </summary>
		public static Task<Result<Badge[]>> GetTagsByNameAsync(string name, QueryOptions options = null)
		{
			name.ThrowIfNullOrEmpty(nameof(name));
			options = options.GetDefaultIfNull();

			options.Custom = new Dictionary<string, string>
			{
				["inname"] = WebUtility.UrlEncode(name)
			};

			var endpoint = $"{Constants.BaseApiUrl}/badges/tags";

			return ApiRequestScheduler.ScheduleRequestAsync<Badge[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the badges earned by the users identified by a set of ids.
		/// </summary>
		public static Task<Result<Badge[]>> GetByUserIdsAsync(IEnumerable<int> userIds, QueryOptions options = null)
		{
			userIds.ThrowIfNullOrEmpty(nameof(userIds));
			options = options.GetDefaultIfNull();

			var idsStr = userIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/users/{idsStr}/badges";

			return ApiRequestScheduler.ScheduleRequestAsync<Badge[]>(endpoint, options);
		}
	}
}
