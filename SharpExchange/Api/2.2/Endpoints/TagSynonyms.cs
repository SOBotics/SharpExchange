using SharpExchange.Api.V22.Types;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SharpExchange.Api.V22.Endpoints
{
	public static class TagSynonyms
	{
		/// <summary>
		/// Gets all the tag synonyms on a site.
		/// </summary>
		public static Task<Result<TagSynonym[]>> GetAllAsync(QueryOptions options = null)
		{
			options = options.GetDefaultIfNull();

			var endpoint = $"{Constants.BaseApiUrl}/tags/synonyms";

			return ApiRequestScheduler.ScheduleRequestAsync<TagSynonym[]>(endpoint, options);
		}

		/// <summary>
		/// Gets the synonyms for a specific set of tags.
		/// </summary>
		public static Task<Result<TagSynonym[]>> GetByIdsAsync(IEnumerable<string> tags, QueryOptions options = null)
		{
			tags.ThrowIfNullOrEmpty(nameof(tags));
			options = options.GetDefaultIfNull();

			var tagList = tags
				.Select(WebUtility.UrlEncode)
				.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/tags/{tagList}/synonyms";

			return ApiRequestScheduler.ScheduleRequestAsync<TagSynonym[]>(endpoint, options);
		}
	}
}
