using System;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using StackExchange.Net;

namespace StackExchange.Auth
{
	public static class FKeyAccessor
	{
		// All sites appear to use the same fkey value, except for chat.
		private const string stackExchangeLogin = "http://stackexchange.com/users/login";

		public static string Get() => Get(stackExchangeLogin);

		public static string Get(string url)
		{
			var html = HttpRequest.Get(url);
			var dom = new HtmlParser().Parse(html);

			return Get(dom);
		}

		public static string Get(IHtmlDocument dom)
		{
			if (dom == null)
			{
				throw new ArgumentNullException(nameof(dom));
			}

			return dom.QuerySelector("input[name=fkey]")?.Attributes["value"]?.Value;
		}
	}
}
