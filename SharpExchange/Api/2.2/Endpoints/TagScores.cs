using SharpExchange.Api.V22.Types;
using System.Net;
using System.Threading.Tasks;

namespace SharpExchange.Api.V22.Endpoints
{
	public static class TagScores
	{
		public enum Period
		{
			[ApiQueryValue("all_time")]
			AllTime,

			[ApiQueryValue("month")]
			Month
		}

		/// <summary>
		/// Gets the top answer posters in a specific tag, either in the last month or for all time.
		/// </summary>
		public static Task<Result<TagScore[]>> GetTopAnswerersAsync(string tag, Period period, QueryOptions options = null)
		{
			tag.ThrowIfNullOrEmpty(nameof(tag));
			options = options.GetDefaultIfNull();

			var t = WebUtility.UrlEncode(tag);
			var p = period.GetApiQueryValueAttributeValue();
			var endpoint = $"{Constants.BaseApiUrl}/tags/{t}/top-answerers/{p}";

			return ApiRequestScheduler.ScheduleRequestAsync<TagScore[]>(endpoint, options);
		}

		/// <summary>
		/// the top question askers in a specific tag, either in the last month or for all time.
		/// </summary>
		public static Task<Result<TagScore[]>> GetTopAskersAsync(string tag, Period period, QueryOptions options = null)
		{
			tag.ThrowIfNullOrEmpty(nameof(tag));
			options = options.GetDefaultIfNull();

			var t = WebUtility.UrlEncode(tag);
			var p = period.GetApiQueryValueAttributeValue();
			var endpoint = $"{Constants.BaseApiUrl}/tags/{t}/top-askers/{p}";

			return ApiRequestScheduler.ScheduleRequestAsync<TagScore[]>(endpoint, options);
		}
	}
}
