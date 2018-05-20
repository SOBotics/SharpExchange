using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using StackExchange.Api.V22.Types;

namespace StackExchange.Api.V22.Endpoints
{
	public static class Badges
	{
		/// <summary>
		/// Gets all badges on a site, in alphabetical order.
		/// </summary>
		public static Result<Badge[]> GetAll(QueryOptions options = null)
		{
			return GetAllAsync(options).Result;
		}

		/// <summary>
		/// Gets all badges on a site, in alphabetical order.
		/// </summary>
		public static async Task<Result<Badge[]>> GetAllAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/badges";

			return await ApiRequestScheduler.ScheduleRequestAsync<Badge[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the badges whose name contains the specified
		/// search term, in alphabetical order.
		/// </summary>
		public static Result<Badge[]> GetByName(string name, QueryOptions options = null)
		{
			return GetByNameAsync(name, options).Result;
		}

		/// <summary>
		/// Gets the badges whose name contains the specified
		/// search term, in alphabetical order.
		/// </summary>
		public static async Task<Result<Badge[]>> GetByNameAsync(string name, QueryOptions options = null)
		{
			name.ThrowIfNullOrEmpty(nameof(name));
			options = options.GetDefaultIfNull();

			options.Custom = new Dictionary<string, string>
			{
				["inname"] = WebUtility.UrlEncode(name)
			};

			var endpoint = $"{Constants.BaseApiUrl}/badges";

			return await ApiRequestScheduler.ScheduleRequestAsync<Badge[]>(endpoint, options);
		}

		/// <summary>
		/// Get the badges identified by a set of ids.
		/// </summary>
		public static Result<Badge[]> GetByIds(IEnumerable<int> ids, QueryOptions options = null)
		{
			return GetByIdsAsync(ids, options).Result;
		}

		/// <summary>
		/// Gets the badges identified by a set of ids.
		/// </summary>
		public static async Task<Result<Badge[]>> GetByIdsAsync(IEnumerable<int> ids, QueryOptions options = null)
		{
			ids.ThrowIfNullOrEmpty(nameof(ids));
			options = options.GetDefaultIfNull();

			var idsStr = ids.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/badges/{idsStr}";

			return await ApiRequestScheduler.ScheduleRequestAsync<Badge[]>(endpoint, options);
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
		public static Result<Badge[]> GetRecentByIds(IEnumerable<int> ids, QueryOptions options = null)
		{
			return GetRecentByIdsAsync(ids, options).Result;
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
		public static async Task<Result<Badge[]>> GetRecentByIdsAsync(IEnumerable<int> ids, QueryOptions options = null)
		{
			ids.ThrowIfNullOrEmpty(nameof(ids));
			options = options.GetDefaultIfNull();

			var idsStr = ids.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/badges/{idsStr}/recipients";

			return await ApiRequestScheduler.ScheduleRequestAsync<Badge[]>(endpoint, options);
		}

		/// <summary>
		/// Get all non-tagged-based badges in alphabetical order.
		/// </summary>
		public static Result<Badge[]> GetNamed(QueryOptions options = null)
		{
			return GetNamedAsync(options).Result;
		}

		/// <summary>
		/// Get all non-tagged-based badges in alphabetical order.
		/// </summary>
		public static async Task<Result<Badge[]>> GetNamedAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/badges/name";

			return await ApiRequestScheduler.ScheduleRequestAsync<Badge[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the non-tagged-based badges whose name contains
		/// the specified search term, in alphabetical order.
		/// </summary>
		public static Result<Badge[]> GetNamedByName(string name, QueryOptions options = null)
		{
			return GetNamedByNameAsync(name, options).Result;
		}

		/// <summary>
		/// Gets the non-tagged-based badges whose name contains
		/// the specified search term, in alphabetical order.
		/// </summary>
		public static async Task<Result<Badge[]>> GetNamedByNameAsync(string name, QueryOptions options = null)
		{
			name.ThrowIfNullOrEmpty(nameof(name));
			options = options.GetDefaultIfNull();

			options.Custom = new Dictionary<string, string>
			{
				["inname"] = WebUtility.UrlEncode(name)
			};

			var endpoint = $"{Constants.BaseApiUrl}/badges/name";

			return await ApiRequestScheduler.ScheduleRequestAsync<Badge[]>(endpoint, options);
		}

		/// <summary>
		/// Returns recently awarded badges in the system. As these
		/// badges have been awarded, they will have the
		/// <see cref="Badge.User"/> property set.
		/// 
		/// Does not support the following <see cref="QueryOptions"/> properties:
		/// <see cref="QueryOptions.Sort"/>, <see cref="QueryOptions.Order"/>,
		/// <see cref="QueryOptions.Max"/>, <see cref="QueryOptions.Min"/>.
		/// </summary>
		public static Result<Badge[]> GetRecent(QueryOptions options = null)
		{
			return GetRecentAsync(options).Result;
		}

		/// <summary>
		/// Returns recently awarded badges in the system. As these
		/// badges have been awarded, they will have the
		/// <see cref="Badge.User"/> property set.
		/// 
		/// Does not support the following <see cref="QueryOptions"/> properties:
		/// <see cref="QueryOptions.Sort"/>, <see cref="QueryOptions.Order"/>,
		/// <see cref="QueryOptions.Max"/>, <see cref="QueryOptions.Min"/>.
		/// </summary>
		public static async Task<Result<Badge[]>> GetRecentAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/badges/recipients";

			return await ApiRequestScheduler.ScheduleRequestAsync<Badge[]>(endpoint, options);
		}

		/// <summary>
		/// Get all tagged-based badges in alphabetical order.
		/// </summary>
		public static Result<Badge[]> GetTags(QueryOptions options = null)
		{
			return GetTagsAsync(options).Result;
		}

		/// <summary>
		/// Get all tagged-based badges in alphabetical order.
		/// </summary>
		public static async Task<Result<Badge[]>> GetTagsAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/badges/tags";

			return await ApiRequestScheduler.ScheduleRequestAsync<Badge[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the tagged-based badges whose name contains
		/// the specified search term, in alphabetical order.
		/// </summary>
		public static Result<Badge[]> GetTagsByName(string name, QueryOptions options = null)
		{
			return GetTagsByNameAsync(name, options).Result;
		}

		/// <summary>
		/// Gets the tagged-based badges whose name contains
		/// the specified search term, in alphabetical order.
		/// </summary>
		public static async Task<Result<Badge[]>> GetTagsByNameAsync(string name, QueryOptions options = null)
		{
			name.ThrowIfNullOrEmpty(nameof(name));
			options = options.GetDefaultIfNull();

			options.Custom = new Dictionary<string, string>
			{
				["inname"] = WebUtility.UrlEncode(name)
			};

			var endpoint = $"{Constants.BaseApiUrl}/badges/tags";

			return await ApiRequestScheduler.ScheduleRequestAsync<Badge[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the badges earned by the users identified by a set of ids.
		/// </summary>
		public static Result<Badge[]> GetByUserIds(IEnumerable<int> userIds, QueryOptions options = null)
		{
			return GetByUserIdsAsync(userIds, options).Result;
		}

		/// <summary>
		/// Gets the badges earned by the users identified by a set of ids.
		/// </summary>
		public static async Task<Result<Badge[]>> GetByUserIdsAsync(IEnumerable<int> userIds, QueryOptions options = null)
		{
			userIds.ThrowIfNullOrEmpty(nameof(userIds));
			options = options.GetDefaultIfNull();

			var idsStr = userIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/users/{idsStr}/badges";

			return await ApiRequestScheduler.ScheduleRequestAsync<Badge[]>(endpoint, options);
		}
	}
}
