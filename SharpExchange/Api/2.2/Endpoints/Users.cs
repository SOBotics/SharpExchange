using SharpExchange.Api.V22.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpExchange.Api.V22.Endpoints
{
	public static class Users
	{
		/// <summary>
		/// Gets all the users on a site.
		/// </summary>
		public static Task<Result<User[]>> GetAllAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/users";

			return ApiRequestScheduler.ScheduleRequestAsync<User[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the users identified by a set of ids.
		/// </summary>
		public static Task<Result<User[]>> GetByIdsAsync(IEnumerable<int> userIds, QueryOptions options = null)
		{
			userIds.ThrowIfNullOrEmpty(nameof(userIds));
			options = options.GetDefaultIfNull();

			var idsStr = userIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/users/{idsStr}";

			return ApiRequestScheduler.ScheduleRequestAsync<User[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the users who have moderation powers on the site.
		/// </summary>
		public static Task<Result<User[]>> GetModeratorsAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/users/moderators";

			return ApiRequestScheduler.ScheduleRequestAsync<User[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the users who are active moderators who have also won a moderator election.
		/// </summary>
		public static Task<Result<User[]>> GetElectedModeratorsAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/users/moderators/elected";

			return ApiRequestScheduler.ScheduleRequestAsync<User[]>(endpoint, options);
		}
	}
}
