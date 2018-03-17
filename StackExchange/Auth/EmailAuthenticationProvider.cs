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
		private readonly string email;
		private readonly string password;



		public EmailAuthenticationProvider(string email, string password)
		{
			authCookieNames = new List<string> { "prov", "uauth", "acct" };
			this.email = email;
			this.password = password;
		}



		public IEnumerable<Cookie> GetAuthCookies(string host, bool includeChatCookie = false)
		{
			var fkey = FKeyAccessor.Get();
			var endpoint = $"https://{host}/users/login";
			var cookies = new CookieManager();

			var response = new HttpRequest
			{
				Verb = Method.POST,
				Endpoint = endpoint,
				Cookies = cookies,
				Data = new Dictionary<string, object>
				{
					["fkey"] = fkey,
					["email"] = email,
					["password"] = password
				}
			}.Send();

			if (cookies.Cookies.Where(x => authCookieNames.Contains(x.Name)).Count() != 3)
			{
				throw new InvalidCredentialsException();
			}

			var allCookies = cookies.Cookies.ToList();

			if (includeChatCookie)
			{
				var chatCookie = GetChatCookie(host, cookies);
				allCookies.Add(chatCookie);
			}

			return allCookies;
		}



		private Cookie GetChatCookie(string host, CookieManager cookies)
		{
			var endpoint = "https://chat.{0}.com/faq";

			if (host == "stackoverflow.com")
			{
				endpoint = string.Format(endpoint, "stackoverflow");
			}
			else
			{
				endpoint = string.Format(endpoint, "stackexchange");
			}

			var cookiesCopy = new CookieManager();
			cookiesCopy.Add(cookies.Cookies.ToArray());

			var response = new HttpRequest
			{
				Verb = Method.GET,
				Endpoint = endpoint,
				Cookies = cookiesCopy
			}.Send();

			var chatCookie = cookiesCopy.Cookies.SingleOrDefault(x => x.Name.Contains("chatusr"));

			if (chatCookie == null)
			{
				throw new Exception("Unable to get chat cookie.");
			}

			return chatCookie;
		}
	}
}
