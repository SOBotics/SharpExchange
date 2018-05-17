using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using StackExchange.Api.V22.Types;

namespace StackExchange.Api.V22.Endpoints
{
	public static class Answers
	{
		/// <summary>
		/// Gets all answers on a site.
		/// </summary>
		public static Result<Answer[]> GetAll(QueryOptions options = null)
		{
			return GetAllAsync(options).Result;
		}

		/// <summary>
		/// Gets all answers on a site.
		/// </summary>
		public static async Task<Result<Answer[]>> GetAllAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/answers";

			return await ApiRequestScheduler.ScheduleRequestAsync<Answer[]>(endpoint, options);
		}

		/// <summary>
		/// Gets answers identified by a set of ids.
		/// </summary>
		public static Result<Answer[]> GetByIds(IEnumerable<int> ids, QueryOptions options)
		{
			return GetByIdsAsync(ids, options).Result;
		}

		/// <summary>
		/// Gets answers identified by a set of ids.
		/// </summary>
		public static async Task<Result<Answer[]>> GetByIdsAsync(IEnumerable<int> ids, QueryOptions options = null)
		{
			ids.ThrowIfNullOrEmpty(nameof(ids));
			options = options.GetDefaultIfNull();

			var idsStr = ids.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/answers/{idsStr}";

			return await ApiRequestScheduler.ScheduleRequestAsync<Answer[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the answers to the questions identified by a set of ids.
		/// </summary>
		public static Result<Answer[]> GetByQuestionIds(IEnumerable<int> questionIds, QueryOptions options)
		{
			return GetByQuestionIdsAsync(questionIds, options).Result;
		}

		/// <summary>
		/// Gets the answers to the questions identified by a set of ids.
		/// </summary>
		public static async Task<Result<Answer[]>> GetByQuestionIdsAsync(IEnumerable<int> questionIds, QueryOptions options = null)
		{
			questionIds.ThrowIfNullOrEmpty(nameof(questionIds));
			options = options.GetDefaultIfNull();

			var idsStr = questionIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/questions/{idsStr}/answers";

			return await ApiRequestScheduler.ScheduleRequestAsync<Answer[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the answers posted by the users identified by a set of ids.
		/// </summary>
		public static Result<Answer[]> GetByUserIds(IEnumerable<int> userIds, QueryOptions options)
		{
			return GetByUserIdsAsync(userIds, options).Result;
		}

		/// <summary>
		/// Gets the answers posted by the users identified by a set of ids.
		/// </summary>
		public static async Task<Result<Answer[]>> GetByUserIdsAsync(IEnumerable<int> userIds, QueryOptions options = null)
		{
			userIds.ThrowIfNullOrEmpty(nameof(userIds));
			options = options.GetDefaultIfNull();

			var idsStr = userIds.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/users/{idsStr}/answers";

			return await ApiRequestScheduler.ScheduleRequestAsync<Answer[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the top answers a user has posted on questions with a set of tags.
		/// </summary>
		public static Result<Answer[]> GetTopAnswersByUserByTags(int userId, IEnumerable<string> tags, QueryOptions options = null)
		{
			return GetTopAnswersByUserByTagsAsync(userId, tags, options).Result;
		}

		/// <summary>
		/// Gets the top answers a user has posted on questions with a set of tags.
		/// </summary>
		public static async Task<Result<Answer[]>> GetTopAnswersByUserByTagsAsync(int userId, IEnumerable<string> tags, QueryOptions options = null)
		{
			tags.ThrowIfNullOrEmpty(nameof(tags));
			options = options.GetDefaultIfNull();

			var tagList = tags
				.Select(WebUtility.UrlEncode)
				.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/users/{userId}/tags/{tagList}/top-answers";

			return await ApiRequestScheduler.ScheduleRequestAsync<Answer[]>(endpoint, options);
		}
	}
}
