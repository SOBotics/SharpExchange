using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using SharpExchange.Net;

namespace SharpExchange.Auth
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
				host.ThrowIfNullOrEmpty(nameof(host));

				if (host.StartsWith("chat.", StringComparison.Ordinal))
				{
					host = host.Remove(0, 5);
				}

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



		/// <summary>
		/// Forces the authentication provider to login to a given community.
		/// Automatically called when cookies are requested that aren't in the cache yet.
		/// </summary>
		/// <param name="host">Community to log in to, e.g. "stackexchange.com".</param>
		/// <returns>Whether or not the login was successful.</returns>
		public bool Login(string host)
		{
			try
			{
				FetchCookies(host);
				return true;
			}

			catch (InvalidCredentialsException)
			{
				return false;
			}
		}

		public void InvalidateHostCache(string host) => cache.Remove(host);

		public void InvalidateAllCache() => cache.Clear();



		private void FetchCookies(string host)
		{
			var endpoint = "";

			//TODO: Temporary until SE stop using openid to
			// authenticate email/pwd logins.
			if (host == "stackexchange.com")
			{
				endpoint = $"https://meta.stackexchange.com/users/login";
			}
			else
			{
				endpoint = $"https://{host}/users/login";
			}

			var fkey = FKeyAccessor.GetAsync().Result;
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
			}.SendAsync().Result;

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
