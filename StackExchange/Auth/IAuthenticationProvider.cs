using System.Collections.Generic;
using StackExchange.Net;

namespace StackExchange
{
	public interface IAuthenticationProvider
	{
		CookieManager this[string host] { get; }

		void InvalidateHostCache(string host);
		void InvalidateAllCache();
	}
}
