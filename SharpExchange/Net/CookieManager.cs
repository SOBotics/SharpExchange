using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace SharpExchange.Net
{
	public class CookieManager
	{
		private readonly Dictionary<string, Cookie> cookies;

		public Cookie this[string name] => cookies[name];

		public Cookie[] Cookies => cookies.Values.ToArray();



		public CookieManager()
		{
			cookies = new Dictionary<string, Cookie>();
		}

		public CookieManager(IEnumerable<Cookie> cookies)
		{
			this.cookies = cookies.ToDictionary(x => x.Name, x => x);
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
			if (cookies.ContainsKey(cookie.Name))
			{
				if (cookie.Expired)
				{
					_ = cookies.Remove(cookie.Name);
				}
				else
				{
					cookies[cookie.Name].Value = cookie.Value;
					cookies[cookie.Name].Expires = cookie.Expires;
				}
			}
			else
			{
				cookies[cookie.Name] = cookie;
			}
		}
	}
}
