using System;
using System.Linq;

namespace StackExchange
{
	internal static class Extensions
	{
		public static void GetHostAndIdFromRoomUrl(this string roomUrl, out string host, out int id)
		{
			if (string.IsNullOrEmpty(roomUrl))
			{
				throw new ArgumentNullException(nameof(roomUrl), $"'{nameof(roomUrl)}' cannot be null or empty.");
			}

			var uri = new Uri(roomUrl);
			var split = uri.LocalPath.Split('/');

			if (split.Length < 3 || !split[2].All(char.IsDigit))
			{
				throw new ArgumentException($"'{nameof(roomUrl)}' is not a valid room URL.");
			}

			id = int.Parse(split[2]);
			host = uri.Host;
		}
	}
}