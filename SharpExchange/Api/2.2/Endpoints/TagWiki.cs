using SharpExchange.Api.V22.Types;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SharpExchange.Api.V22.Endpoints
{
	public static class TagWikis
	{
		/// <summary>
		/// Gets the wiki entries for a set of tags.
		/// </summary>
		public static Task<Result<TagWiki[]>> GetByTagNamesAsync(IEnumerable<string> tagNames, QueryOptions options = null)
		{
			tagNames.ThrowIfNullOrEmpty(nameof(tagNames));
			options = options.GetDefaultIfNull();

			var tagList = tagNames
				.Select(WebUtility.UrlEncode)
				.ToDelimitedList();
			var endpoint = $"{Constants.BaseApiUrl}/tags/{tagList}/wikis";

			return ApiRequestScheduler.ScheduleRequestAsync<TagWiki[]>(endpoint, options);
		}
	}
}
