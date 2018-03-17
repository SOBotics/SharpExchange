using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using StackExchange.Net;

namespace StackExchange.Auth
{
	public class EmailAuthenticationProvider : IAuthenticationProvider
	{
		private readonly List<string> authCookieNames;
		private readonly List<string> chatusrCookieHosts;
		private readonly string email;
		private readonly string password;



		public EmailAuthenticationProvider(string email, string password)
		{
			authCookieNames = new List<string>
			{
				"prov",
				"acct"
			};

			chatusrCookieHosts = new List<string>
			{
				"stackoverflow.com",
				"meta.stackexchange.com"
			};

			this.email = email;
			this.password = password;
		}



		public IEnumerable<Cookie> GetAuthCookies(string host, bool includeChatCookie = false)
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

			if (includeChatCookie)
			{
				var chatCookie = GetChatCookie(host, cMan);
				authCookies.Add(chatCookie);
			}

			return authCookies;
		}



		private Cookie GetChatCookie(string host, CookieManager cookies)
		{
			var endpoint = "https://chat.{0}/faq";

			// If we're on any of the sites in chatusrCookieHosts,
			// fetch the chatusr cookie. Otherwise, we'll need to
			// get the sechatusr cookie instead.
			if (chatusrCookieHosts.Contains(host))
			{
				endpoint = string.Format(endpoint, host);
			}
			else
			{
				endpoint = string.Format(endpoint, "stackexchange.com");
			}

			var cookiesCopy = new CookieManager();
			cookiesCopy.Add(cookies.Cookies.ToArray());

			var response = new HttpRequest
			{
				Verb = Method.GET,
				Endpoint = endpoint,
				Cookies = cookiesCopy
			}.Send();

			var chatCookie = cookiesCopy.Cookies
				.SingleOrDefault(x => x.Name.Contains("chatusr"));

			if (chatCookie == null)
			{
				throw new Exception("Unable to get chat cookie.");
			}

			return chatCookie;
		}
	}
}
