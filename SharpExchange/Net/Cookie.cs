using System;

namespace SharpExchange.Net
{
	public class Cookie
	{
		public string Name;

		public string Value;

		public string Domain;

		public DateTime Expires;

		public bool Expired =>
			Expires.ToUniversalTime() > DateTime.MinValue &&
			Expires.ToUniversalTime() < DateTime.UtcNow;
	}
}
