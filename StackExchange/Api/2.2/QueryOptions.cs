using System;
using System.Collections.Generic;
using System.Reflection;

namespace StackExchange.Api.v22
{
	public class QueryOptions
	{
		private const BindingFlags propBindings = BindingFlags.Instance | BindingFlags.Public;
		private readonly HashSet<Type> enumTypes = new HashSet<Type>
		{
			typeof(OrderBy), typeof(SortBy)
		};

		#region Properties.

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

		/// <summary>
		/// Defines which field Min/Max (and Order) apply to.
		/// </summary>
		[ApiQueryValue("sort")]
		public SortBy? Sort { get; set; }

		/// <summary>
		/// How the results should be ordered.
		/// </summary>
		[ApiQueryValue("order")]
		public OrderBy? Order { get; set; }

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
		/// Defines the start of a range to apply to the CreationDate field.
		/// </summary>
		[ApiQueryValue("fromdate")]
		public DateTime? FromData { get; set; }

		/// <summary>
		/// Defines the end of a range to apply to the CreationDate field.
		/// </summary>
		[ApiQueryValue("todate")]
		public DateTime? ToDate { get; set; }

		#endregion

		public Dictionary<string, string> GetOptions()
		{
			var props = GetType().GetProperties(propBindings);
			var opts = new Dictionary<string, string>();

			foreach (var p in props)
			{
				var val = p.GetValue(this);

				if (val == null) continue;

				var name = p.GetCustomAttribute<ApiQueryValueAttribute>().Value;
				var vType = val.GetType();

				opts[name] = val.ToString();

				if (enumTypes.Contains(vType))
				{
					opts[name] = vType
						.GetMember(opts[name])[0]
						.GetCustomAttribute<ApiQueryValueAttribute>()
						.Value;
				}
			}

			return opts;
		}
	}
}
