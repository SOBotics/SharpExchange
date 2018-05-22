using SharpExchange.Net;

namespace SharpExchange
{
	public interface IAuthenticationProvider
	{
		CookieManager this[string host] { get; }

		void InvalidateHostCache(string host);
		void InvalidateAllCache();
	}
}
