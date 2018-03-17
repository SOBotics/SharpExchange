using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using StackExchange.Net;

namespace StackExchange
{
	public interface IAuthenticationProvider
	{
		IEnumerable<Cookie> GetAuthCookies(string host, bool includeChatCookie = false);
	}
}
