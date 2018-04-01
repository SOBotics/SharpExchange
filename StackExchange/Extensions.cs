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

		public static int ParseFriendlyNumber(this string str)
		{
			var strClean = str
				.Replace(",", "")
				.Trim()
				.ToUpperInvariant();
			var multiplier = 1;

			if (strClean.EndsWith("K"))
			{
				multiplier = 1000;
				strClean = strClean.Substring(0, strClean.Length - 1).Trim();
			}

			if (!float.TryParse(strClean, out var baseNum))
			{
				return 0;
			}

			return (int)Math.Round(baseNum * multiplier);
		}
	}
}