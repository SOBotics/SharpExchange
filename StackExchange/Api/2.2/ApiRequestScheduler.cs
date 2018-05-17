using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using StackExchange.Api.V22.Types;

namespace StackExchange.Api.V22
{
	internal static partial class ApiRequestScheduler
	{
		private const string endpointCleanerPtn = @"(?:(2\.2/users)/\d+(/tags)/\S+?(/top-answers))|(?:(2\.2/tags)/\S+?(/(?:faq|info|related|synonyms|wikis|top-a(?:nswer|sk)ers))(?:/\S+)?)|(?:(2\.2/revisions)(?:/[\d;]+))";
		private static readonly Dictionary<string, Sheduler> schedulers = new Dictionary<string, Sheduler>();
		private static readonly Regex endpointCleaner = new Regex(endpointCleanerPtn, RegexOptions.Compiled);

		public static TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(60);



		public static async Task<Result<T>> ScheduleRequestAsync<T>(string endpoint, QueryOptions options = null)
		{
			endpoint.ThrowIfNullOrEmpty(nameof(endpoint));

			var fullUrl = $"{endpoint}?{options.Query}";
			var endpointId = GetEndpointId(endpoint);

			if (!schedulers.ContainsKey(endpointId))
			{
				schedulers[endpointId] = new Sheduler();
			}

			return await schedulers[endpointId].ScheduleAsync<T>(fullUrl);
		}



		private static string GetEndpointId(string url)
		{
			if (endpointCleaner.IsMatch(url))
			{
				return endpointCleaner.Replace(url, "$1$2$3$4$5$6");
			}

			var split = url.Split('/');
			var id = new StringBuilder();

			foreach (var s in split)
			{
				if (string.IsNullOrEmpty(s) || (s.Any(char.IsDigit) && s != "2.2")) continue;

				id.Append('/');
				id.Append(s);
			}

			return id.ToString();
		}
	}
}