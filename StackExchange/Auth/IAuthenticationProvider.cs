using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace StackExchange
{
	public interface IAuthenticationProvider
	{
		IEnumerable<RestResponseCookie> GetAuthCookies(string host);
	}
}
