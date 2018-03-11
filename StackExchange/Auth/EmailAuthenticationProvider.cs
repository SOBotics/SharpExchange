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



		public IEnumerable<RestResponseCookie> GetAuthCookies(string host)
		{
			var fkey = FKeyAccessor.Get();
			var endpoint = $"https://{host}/users/login";
			var cookies = new CookieManager();

			var response = new HttpRequest
			{
				Verb = Method.POST,
				Endpoint = endpoint,
				Cookies = cookies
			}.Send(new
			{
				fkey = fkey,
				email = email,
				password = password
			});

			if (cookies.Cookies.Where(x => authCookieNames.Contains(x.Name)).Count() != 3)
			{
				throw new InvalidCredentialsException();
			}

			return cookies.Cookies;
		}
	}
}
