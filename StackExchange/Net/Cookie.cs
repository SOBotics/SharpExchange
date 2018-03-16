using System;
using System.Collections.Generic;
using System.Text;

namespace StackExchange.Net
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
