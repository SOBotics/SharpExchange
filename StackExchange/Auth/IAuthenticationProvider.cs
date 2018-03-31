using System.Collections.Generic;
using StackExchange.Net;

namespace StackExchange
{
	public interface IAuthenticationProvider
	{
		IEnumerable<Cookie> GetAuthCookies(string host);
	}
}
