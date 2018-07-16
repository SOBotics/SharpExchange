using SharpExchange.Api.V22.Types;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SharpExchange.Api.V22.Endpoints
{
	public static class Questions
	{
		/// <summary>
		/// Gets all questions on a site.
		/// </summary>
		public static Task<Result<Question[]>> GetAllAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/questions";

			return ApiRequestScheduler.ScheduleRequestAsync<Question[]>(endpoint, options);
		}

		/// <summary>
		/// Gets all questions on the site with active bounties.
		/// </summary>
		public static Task<Result<Question[]>> GetAllActiveBountiesAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/questions/featured";

			return ApiRequestScheduler.ScheduleRequestAsync<Question[]>(endpoint, options);
		}

		/// <summary>
		/// Gets all questions on the site that are considered unanswered.
		/// Note: just because a question has an answer, that does
		/// not mean it is considered answered. While the rules are subject
		/// to change, at this time a question must have at least one upvoted
		/// answer to be considered answered.
		/// </summary>
		public static Task<Result<Question[]>> GetAllUnansweredAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/questions/unanswered";

			return ApiRequestScheduler.ScheduleRequestAsync<Question[]>(endpoint, options);
		}

		/// <summary>
		/// Gets all questions on the site with no answers.
		/// </summary>
		public static Task<Result<Question[]>> GetAllNoAnswersAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/questions/no-answers";

			return ApiRequestScheduler.ScheduleRequestAsync<Question[]>(endpoint, options);
		}

		/// <summary>
		/// Gets questions identified by a set of ids.
		/// </summary>
		public static Task<Result<Question[]>> GetByIdsAsync(IEnumerable<int> ids, QueryOptions options = null)
		{
			ids.ThrowIfNullOrEmpty(nameof(ids));
			options = options.GetDefaultIfNull();

			var idsStr = ids.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/questions/{idsStr}";

			return ApiRequestScheduler.ScheduleRequestAsync<Question[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the questions that have been linked to the questions identified by a set of ids.
		/// </summary>
		public static Task<Result<Question[]>> GetLinkedByIdsAsync(IEnumerable<int> ids, QueryOptions options = null)
		{
			ids.ThrowIfNullOrEmpty(nameof(ids));
			options = options.GetDefaultIfNull();

			var idsStr = ids.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/questions/{idsStr}/linked";

			return ApiRequestScheduler.ScheduleRequestAsync<Question[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the questions that are related to the questions identified by a set of ids.
		/// </summary>
		public static Task<Result<Question[]>> GetRelatedByIdsAsync(IEnumerable<int> ids, QueryOptions options = null)
		{
			ids.ThrowIfNullOrEmpty(nameof(ids));
			options = options.GetDefaultIfNull();

			var idsStr = ids.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/questions/{idsStr}/related";

			return ApiRequestScheduler.ScheduleRequestAsync<Question[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the frequently asked questions in a set of tags.
		/// </summary>
		public static Task<Result<Question[]>> GetFaqByTagsAsync(IEnumerable<string> tags, QueryOptions options = null)
		{
			tags.ThrowIfNullOrEmpty(nameof(tags));
			options = options.GetDefaultIfNull();

			var tagList = tags
				.Select(WebUtility.UrlEncode)
				.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/tags/{tagList}/faq";

			return ApiRequestScheduler.ScheduleRequestAsync<Question[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the questions favourited by any of the users identified by a set of ids.
		/// </summary>
		public static Task<Result<Question[]>> GetFavoritedByUserIdsAsync(IEnumerable<int> userIds, QueryOptions options = null)
		{
			userIds.ThrowIfNullOrEmpty(nameof(userIds));
			options = options.GetDefaultIfNull();

			var idsStr = userIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/users/{idsStr}/favorites";

			return ApiRequestScheduler.ScheduleRequestAsync<Question[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the questions asked by the users identified by a set of ids.
		/// </summary>
		public static Task<Result<Question[]>> GetByUserIdsAsync(IEnumerable<int> userIds, QueryOptions options = null)
		{
			userIds.ThrowIfNullOrEmpty(nameof(userIds));
			options = options.GetDefaultIfNull();

			var idsStr = userIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/users/{idsStr}/questions";

			return ApiRequestScheduler.ScheduleRequestAsync<Question[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the questions on which a set of users have active bounties.
		/// </summary>
		public static Task<Result<Question[]>> GetActiveBountiesByUserIdsAsync(IEnumerable<int> userIds, QueryOptions options = null)
		{
			userIds.ThrowIfNullOrEmpty(nameof(userIds));
			options = options.GetDefaultIfNull();

			var idsStr = userIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/users/{idsStr}/featured";

			return ApiRequestScheduler.ScheduleRequestAsync<Question[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the questions asked by a set of users that have no answers.
		/// </summary>
		public static Task<Result<Question[]>> GetNoAnswersByUserIdsAsync(IEnumerable<int> userIds, QueryOptions options = null)
		{
			userIds.ThrowIfNullOrEmpty(nameof(userIds));
			options = options.GetDefaultIfNull();

			var idsStr = userIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/users/{idsStr}/no-answers";

			return ApiRequestScheduler.ScheduleRequestAsync<Question[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the questions asked by a set of users,
		/// which have at least one answer but no accepted answer.
		/// </summary>
		public static Task<Result<Question[]>> GetUnacceptedByUserIdsAsync(IEnumerable<int> userIds, QueryOptions options = null)
		{
			userIds.ThrowIfNullOrEmpty(nameof(userIds));
			options = options.GetDefaultIfNull();

			var idsStr = userIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/users/{idsStr}/unaccepted";

			return ApiRequestScheduler.ScheduleRequestAsync<Question[]>(endpoint, options);
		}

		/// <summary>
		/// Get the questions asked by a set of users, which are not
		/// considered to be adequately answered.
		/// </summary>
		public static Task<Result<Question[]>> GetUnAnsweredByUserIdsAsync(IEnumerable<int> userIds, QueryOptions options = null)
		{
			userIds.ThrowIfNullOrEmpty(nameof(userIds));
			options = options.GetDefaultIfNull();

			var idsStr = userIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/users/{idsStr}/unanswered";

			return ApiRequestScheduler.ScheduleRequestAsync<Question[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the top questions a user has posted with a set of tags.
		/// </summary>
		public static Task<Result<Question[]>> GetTopQuestionByUserIdByTagsAsync(int userId, IEnumerable<string> tags, QueryOptions options = null)
		{
			tags.ThrowIfNullOrEmpty(nameof(tags));
			options = options.GetDefaultIfNull();

			var tagList = tags
				.Select(WebUtility.UrlEncode)
				.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/users/{userId}/tags/{tagList}/top-questions";

			return ApiRequestScheduler.ScheduleRequestAsync<Question[]>(endpoint, options);
		}
	}
}
