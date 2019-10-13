using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using SharpExchange.Net;

namespace SharpExchange.Auth
{
	internal static class FKeyAccessor
	{
		// All sites appear to use the same fkey value, except for chat.
		private const string stackExchangeLogin = "http://stackexchange.com/users/login";
		private static readonly Dictionary<string, string> cache = new Dictionary<string, string>();

		public static Task<string> GetAsync() => GetAsync(stackExchangeLogin);

		public static async Task<string> GetAsync(string url, CookieManager cMan = null)
		{
			if (cache.ContainsKey(url))
			{
				return cache[url];
			}

			var html = await HttpRequest.GetAsync(url, cMan);
			var dom = await new HtmlParser().ParseDocumentAsync(html);
			var fkey = Get(dom);

			cache[url] = fkey;

			return fkey;
		}

		public static string Get(IHtmlDocument dom)
		{
			dom.ThrowIfNull(nameof(dom));

			return dom.QuerySelector("input[name=fkey]")
				?.Attributes["value"]
				?.Value;
		}

		public static void ClearCache() => cache.Clear();
	}
}
