using System;
using System.Collections.Generic;
using System.Reflection;

namespace SharpExchange.Api.V22
{
	public class QueryOptions
	{
		private const BindingFlags propBindings = BindingFlags.Instance | BindingFlags.Public;
		private readonly DateTime unixEpoch = new DateTime(1970, 1, 1);
		private readonly HashSet<Type> enumTypes = new HashSet<Type>
		{
			typeof(OrderBy), typeof(SortBy)
		};

		[ApiQueryValue("site")]
		public string Site { get; set; }

		[ApiQueryValue("filter")]
		public string Filter { get; set; }

		[ApiQueryValue("key")]
		public string Key { get; set; }

		[ApiQueryValue("access_token")]
		public string AccessToken { get; set; }

		[ApiQueryValue("page")]
		public int? Page { get; set; }

		[ApiQueryValue("pagesize")]
		public int? PageSize { get; set; }

		[ApiQueryValue("fromdate")]
		public DateTime? FromData { get; set; }

		[ApiQueryValue("todate")]
		public DateTime? ToDate { get; set; }

		/// <summary>
		/// Defines which field Min/Max (and Order) apply to.
		/// </summary>
		[ApiQueryValue("sort")]
		public SortBy? SortBy { get; set; }

		/// <summary>
		/// How the results should be ordered.
		/// </summary>
		[ApiQueryValue("order")]
		public OrderBy? OrderBy { get; set; }

		/// <summary>
		/// The inclusive lower bound of a range that
		/// a field must fall in (field is specified
		/// by the Sort property).
		/// </summary>
		[ApiQueryValue("min")]
		public int? Min { get; set; }

		/// <summary>
		/// The inclusive upper bound of a range that
		/// a field must fall in (field is specified
		/// by the Sort property).
		/// </summary>
		[ApiQueryValue("max")]
		public int? Max { get; set; }

		/// <summary>
		/// Allows the caller to add custom query values
		/// that are not exposed by the library.
		/// </summary>
		public Dictionary<string, string> Custom { get; set; } = new Dictionary<string, string>();

		public string Query => Custom.MergeInto(Options)?.ToQueryString();

		public Dictionary<string, string> Options
		{
			get
			{
				var props = GetType().GetProperties(propBindings);
				var opts = new Dictionary<string, string>();

				foreach (var p in props)
				{
					var name = p.GetCustomAttribute<ApiQueryValueAttribute>()?.Value;

					if (name == null) continue;

					var val = p.GetValue(this);

					if (val == null) continue;

					var vType = val.GetType();

					if (enumTypes.Contains(vType))
					{
						opts[name] = val.GetApiQueryValueAttributeValue();
					}
					else if (vType == typeof(DateTime))
					{
						var secs = ((DateTime)val - unixEpoch).TotalSeconds;

						opts[name] = Math.Round(secs).ToString();
					}
					else
					{
						opts[name] = val.ToString();
					}
				}

				return opts;
			}
		}
	}
}
