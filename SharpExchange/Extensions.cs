using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SharpExchange.Api.V22;
using SharpExchange.Api.V22.Endpoints;

namespace SharpExchange
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

		public static void ThrowIfNullOrEmpty<T>(this IEnumerable<T> e, string argName)
		{
			argName.ThrowIfNullOrEmpty(nameof(argName));

			if (e == null)
			{
				throw new ArgumentNullException(argName);
			}

			if (e.Count() == 0)
			{
				throw new ArgumentException($"'{argName}' cannot be empty.");
			}
		}

		public static string ToQueryString(this Dictionary<string, string> d)
		{
			if ((d?.Count ?? 0) == 0)
			{
				return "";
			}

			var builder = new StringBuilder();

			foreach (var kv in d)
			{
				var encoded = WebUtility.UrlEncode(kv.Value);

				_ = builder.Append(kv.Key);
				_ = builder.Append('=');
				_ = builder.Append(encoded);
				_ = builder.Append('&');
			}

			_ = builder.Remove(builder.Length - 1, 1);

			return builder.ToString();
		}

		/// <summary>
		/// Merges the source dictionary into the target dictionary. Any duplicate keys in the target will be overwritten.
		/// </summary>
		public static Dictionary<TKey, TValue> MergeInto<TKey, TValue>(this Dictionary<TKey, TValue> source, Dictionary<TKey, TValue> target)
		{
			if (source == null && target == null)
			{
				return null;
			}

			if (source == null && target != null)
			{
				return target;
			}

			if (source != null && target == null)
			{
				return source;
			}

			foreach (var kv in source)
			{
				target[kv.Key] = kv.Value;
			}

			return target;
		}

		public static QueryOptions GetDefaultIfNull(this QueryOptions o)
		{
			if (o == null)
			{
				return new QueryOptions
				{
					Site = Constants.DefaultSite
				};
			}

			if (string.IsNullOrEmpty(o.Site))
			{
				o.Site = Constants.DefaultSite;

				return o;
			}

			return o;
		}

		public static string ToDelimitedList<T>(this IEnumerable<T> ids, string delmiter = ";")
		{
			return ids
				.Select(x => x.ToString())
				.Aggregate((a, b) => $"{a}{delmiter}{b}");
		}

		public static string GetApiQueryValueAttributeValue(this object o)
		{
			var enumVal = o.ToString();

			return o
				.GetType()
				.GetMember(enumVal)[0]
				.GetCustomAttribute<ApiQueryValueAttribute>()
				.Value;
		}
	}
}