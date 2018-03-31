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
			authCookieNames = new List<string>
			{
				"prov",
				"acct"
			};

			this.email = email;
			this.password = password;
		}



		public IEnumerable<Cookie> GetAuthCookies(string host)
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

			return authCookies;
		}
	}
}
