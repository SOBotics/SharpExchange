using System.Collections.Generic;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using StackExchange.Net;

namespace StackExchange.Auth
{
	internal static class FKeyAccessor
	{
		// All sites appear to use the same fkey value, except for chat.
		private const string stackExchangeLogin = "http://stackexchange.com/users/login";
		private static readonly Dictionary<string, string> cache = new Dictionary<string, string>();

		public static string Get() => Get(stackExchangeLogin);

		public static string Get(string url,  CookieManager cMan = null)
		{
			if (cache.ContainsKey(url))
			{
				return cache[url];
			}

			var html = HttpRequest.Get(url, cMan);
			var dom = new HtmlParser().Parse(html);
			var fkey = Get(dom);

			cache[url] = fkey;

			return fkey;
		}

		//TODO: Cache returned value.
		public static string Get(IHtmlDocument dom)
		{
			dom.ThrowIfNull(nameof(dom));


			return dom.QuerySelector("input[name=fkey]")?.Attributes["value"]?.Value;
		}
	}
}
