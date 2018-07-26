using SharpExchange.Api.V22.Types;
using System.Threading.Tasks;

namespace SharpExchange.Api.V22.Endpoints
{
	public static class TopTags
	{
		/// <summary>
		/// Gets the top tags (by score) a single user has posted answers in.
		/// </summary>
		public static Task<Result<TopTag[]>> GetByAnswersAsync(int userId, QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/users/{userId}/top-answer-tags";

			return ApiRequestScheduler.ScheduleRequestAsync<TopTag[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the top tags (by score) a single user has asked questions in.
		/// </summary>
		public static Task<Result<TopTag[]>> GetByQuestionsAsync(int userId, QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/users/{userId}/top-question-tags";

			return ApiRequestScheduler.ScheduleRequestAsync<TopTag[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the top tags (by score) a single user has posted in.
		/// </summary>
		public static Task<Result<TopTag[]>> GetAllAsync(int userId, QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/users/{userId}/top-tags";

			return ApiRequestScheduler.ScheduleRequestAsync<TopTag[]>(endpoint, options);
		}
	}
}
