using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SharpExchange.Api.V22.Types;

namespace SharpExchange.Api.V22
{
	internal static partial class ApiRequestScheduler
	{
		private const string endpointCleanerPtn = @"(?:(2\.2/users)/\d+(/tags)/\S+?(/top-answers))|(?:(2\.2/tags)/\S+?(/(?:faq|info|related|synonyms|wikis|top-a(?:nswer|sk)ers))(?:/\S+)?)|(?:(2\.2/revisions)(?:/[\d;]+))";
		private static readonly Dictionary<string, MethodSheduler> schedulers = new Dictionary<string, MethodSheduler>();
		private static readonly Regex endpointCleaner = new Regex(endpointCleanerPtn, RegexOptions.Compiled);

		public static TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(60);



		public static Task<Result<T>> ScheduleRequestAsync<T>(string endpoint, QueryOptions options = null)
		{
			endpoint.ThrowIfNullOrEmpty(nameof(endpoint));

			var url = endpoint + (options != null ? "?" + options.Query : "");
			var endpointId = GetEndpointId(endpoint);

			if (!schedulers.ContainsKey(endpointId))
			{
				schedulers[endpointId] = new MethodSheduler();
			}

			return schedulers[endpointId].ScheduleAsync<T>(url);
		}



		private static string GetEndpointId(string url)
		{
			if (endpointCleaner.IsMatch(url))
			{
				return endpointCleaner.Replace(url, "$1$2$3$4$5$6");
			}

			var split = url.Split('/').Skip(2);
			var id = new StringBuilder();

			foreach (var s in split)
			{
				if (string.IsNullOrEmpty(s) || (s.Any(char.IsDigit) && s != "2.2")) continue;

				_ = id.Append(s);
				_ = id.Append('/');
			}

			return id.ToString();
		}
	}
}