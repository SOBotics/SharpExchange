using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace StackExchange.Net
{
	public class CookieManager
	{
		private Dictionary<string, Cookie> cookies;

		public Cookie this[string name] => cookies[name];

		public Cookie[] Cookies => cookies.Values.ToArray();



		public CookieManager()
		{
			cookies = new Dictionary<string, Cookie>();
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
			Add(new Cookie
			{
				Name = cookie.Name,
				Value = cookie.Value,
				Domain = cookie.Domain,
				Expires = cookie.Expires
			});
		}

		public void Add(IEnumerable<Cookie> cookies)
		{
			foreach (var cookie in cookies)
			{
				Add(cookie);
			}
		}

		public void Add(Cookie cookie)
		{
			if (cookies.ContainsKey(cookie.Name) && cookie.Expired)
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
