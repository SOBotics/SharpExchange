using SharpExchange.Api.V22.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpExchange.Api.V22.Endpoints
{
	public static class QuestionTimeline
	{
		/// <summary>
		/// Gets the timelines of the questions identified by a set of ids.
		/// </summary>
		public static Task<Result<QuestionTimelineEntry[]>> GetByIdsAsync(IEnumerable<int> ids, QueryOptions options = null)
		{
			ids.ThrowIfNullOrEmpty(nameof(ids));
			options = options.GetDefaultIfNull();

			var idsStr = ids.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/questions/{idsStr}/timeline";

			return ApiRequestScheduler.ScheduleRequestAsync<QuestionTimelineEntry[]>(endpoint, options);
		}
	}
}
