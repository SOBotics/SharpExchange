using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace StackExchange.Net
{
	public class CookieManager
	{
		private Dictionary<string, RestResponseCookie> cookies;

		public RestResponseCookie this[string name] => cookies[name];

		public RestResponseCookie[] Cookies => cookies.Values.ToArray();



		public CookieManager()
		{
			cookies = new Dictionary<string, RestResponseCookie>();
		}



		public void Add(IEnumerable<RestResponseCookie> cookies)
		{
			foreach (var cookie in cookies)
			{
				Add(cookie);
			}
		}

		public void Add(RestResponseCookie cookie)
		{
			if (cookies.ContainsKey(cookie.Name) && (cookie.Expired || cookie.Discard))
			{
				cookies.Remove(cookie.Name);
			}
			else
			{
				cookies[cookie.Name] = cookie;
			}
		}
	}
}
