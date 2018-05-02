using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RestSharp;
using StackExchange.Net;

namespace StackExchange.Auth
{
	public class EmailAuthenticationProvider : IAuthenticationProvider
	{
		private readonly List<string> authCookieNames;
		private readonly string email;
		private readonly string password;
		private readonly Dictionary<string, IReadOnlyCollection<Cookie>> cache;

		public CookieManager this[string host]
		{
			get
			{
				if (cache.ContainsKey(host))
				{
					return new CookieManager(cache[host]);
				}
				else
				{
					FetchCookies(host);

					return new CookieManager(cache[host]);
				}
			}
		}



		public EmailAuthenticationProvider(string email, string password)
		{
			this.email = email;
			this.password = password;

			authCookieNames = new List<string>
			{
				"prov",
				"acct"
			};

			cache = new Dictionary<string, IReadOnlyCollection<Cookie>>();
		}



		public void InvalidateHostCache(string host) => cache.Remove(host);

		public void InvalidateAllCache() => cache.Clear();



		private void FetchCookies(string host)
		{
			//TODO: Temporary until SE stop using openid to
			// authenticate email/pwd logins.
			if (host == "stackexchange.com")
			{
				host = "meta." + host;
			}

			var fkey = FKeyAccessor.Get();
			var endpoint = $"https://{host}/users/login";
			var cMan = new CookieManager();

			var response = new HttpRequest
			{
				Verb = Method.POST,
				Endpoint = endpoint,
				Cookies = cMan,
				Data = new Dictionary<string, object>
				{
					["fkey"] = fkey,
					["email"] = email,
					["password"] = password
				}
			}.Send();

			var authCookies = cMan.Cookies
				.Where(x => authCookieNames.Contains(x.Name))
				.ToList();

			if (authCookies.Count != authCookieNames.Count)
			{
				throw new InvalidCredentialsException();
			}

			cache[host] = authCookies;
		}
	}
}
