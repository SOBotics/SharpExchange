using SharpExchange.Net;

namespace SharpExchange
{
	public interface IAuthenticationProvider
	{
		CookieManager this[string host] { get; }

		void InvalidateHostCache(string host);
		void InvalidateAllCache();

		/// <summary>
		/// Log in to the given community.
		/// </summary>
		/// <param name="host">Community to log in to, e.g. "stackexchange.com".</param>
		/// <returns>Whether or not the login was successful.</returns>
		bool Login(string host);
	}
}
