using System;
using System.Linq;
using System.Threading.Tasks;

namespace StackExchange
{
	internal static class Extensions
	{
		public static void GetHostAndIdFromRoomUrl(this string roomUrl, out string host, out int id)
		{
			roomUrl.ThrowIfNullOrEmpty(nameof(roomUrl));

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

		public static async Task InvokeAsync(this Action del)
		{
			if (del != null)
			{
				await Task.Run(del);
			}
		}

		public static async Task InvokeAsync<T>(this Action<T> del, T arg)
		{
			if (del != null)
			{
				await Task.Run(() => del.Invoke(arg));
			}
		}

		public static string GetChatHost(this string host)
		{
			if (!host.StartsWith("chat."))
			{
				return "chat." + host;
			}

			return host;
		}

		public static void ThrowIfNullOrEmpty(this string str, string argName)
		{
			if (string.IsNullOrEmpty(argName))
			{
				throw new ArgumentException($"'{argName}' cannot be null or empty.");
			}

			if (string.IsNullOrEmpty(str))
			{
				throw new ArgumentException($"'{argName}' cannot be null or empty.");
			}
		}

		public static void ThrowIfNull(this object o, string argName)
		{
			argName.ThrowIfNullOrEmpty(nameof(argName));

			if (o == null)
			{
				throw new ArgumentNullException(argName);
			}
		}
	}
}