using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Api.v22.Types;
using StackExchange.Net;

namespace StackExchange.Api.v22.Endpoints
{
	public static class Answers
	{
		public static Result<Answer[]> GetAll(QueryOptions options = null)
		{
			return GetAllAsync(options).Result;
		}

		public static async Task<Result<Answer[]>> GetAllAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var url = $"{Constants.BaseApiUrl}/answers?{options.Query}";

			return await GetResultAsync(url);
		}

		public static Result<Answer[]> GetByIds(IEnumerable<int> ids, QueryOptions options)
		{
			return GetByIdsAsync(ids, options).Result;
		}

		public static async Task<Result<Answer[]>> GetByIdsAsync(IEnumerable<int> ids, QueryOptions options = null)
		{
			ids.ThrowIfNullOrEmpty(nameof(ids));
			options = options.GetDefaultIfNull();

			var idsStr = ids.ToDelimitedList();
			var url = $"{Constants.BaseApiUrl}/answers/{idsStr}?{options.Query}";

			return await GetResultAsync(url);
		}

		public static Result<Answer[]> GetByQuestionIds(IEnumerable<int> questionIds, QueryOptions options)
		{
			return GetByQuestionIdsAsync(questionIds, options).Result;
		}

		public static async Task<Result<Answer[]>> GetByQuestionIdsAsync(IEnumerable<int> questionIds, QueryOptions options = null)
		{
			questionIds.ThrowIfNullOrEmpty(nameof(questionIds));
			options = options.GetDefaultIfNull();

			var idsStr = questionIds.ToDelimitedList();
			var url = $"{Constants.BaseApiUrl}/questions/{idsStr}/answers?{options.Query}";

			return await GetResultAsync(url);
		}

		public static Result<Answer[]> GetByUserIds(IEnumerable<int> userIds, QueryOptions options)
		{
			return GetByUserIdsAsync(userIds, options).Result;
		}

		public static async Task<Result<Answer[]>> GetByUserIdsAsync(IEnumerable<int> userIds, QueryOptions options = null)
		{
			userIds.ThrowIfNullOrEmpty(nameof(userIds));
			options = options.GetDefaultIfNull();

			var idsStr = userIds.ToDelimitedList();
			var url = $"{Constants.BaseApiUrl}/users/{idsStr}/answers?{options.Query}";

			return await GetResultAsync(url);
		}

		public static Result<Answer[]> GetTopAnswersByUserByTags(int userId, IEnumerable<string> tags, QueryOptions options = null)
		{
			return GetTopAnswersByUserByTagsAsync(userId, tags, options).Result;
		}

		public static async Task<Result<Answer[]>> GetTopAnswersByUserByTagsAsync(int userId, IEnumerable<string> tags, QueryOptions options = null)
		{
			tags.ThrowIfNullOrEmpty(nameof(tags));
			options = options.GetDefaultIfNull();

			var tagList = tags
				.Select(WebUtility.UrlEncode)
				.ToDelimitedList();
			var url = $"{Constants.BaseApiUrl}/users/{userId}/tags/{tagList}/top-answers?{options.Query}";

			return await GetResultAsync(url);
		}



		private static async Task<Result<Answer[]>> GetResultAsync(string url)
		{
			var result = await HttpRequest.GetWithStatusAsync(url);

			if (string.IsNullOrEmpty(result.Body))
			{
				return new Result<Answer[]>
				{
					ErrorId = (int)result.Status,
					ErrorName = result.Status.ToString()
				};
			}

			return JsonConvert.DeserializeObject<Result<Answer[]>>(result.Body);
		}
	}
}
