using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StackExchange.Api.Types;

namespace StackExchange.Api.Endpoints
{
	public static class Answers
	{
		private static readonly Dictionary<string, string> validSortFields = new Dictionary<string, string>
		{
			["activity"] = "last_activity_date",
			["creation"] = "creation_date",
			["votes"] = "score"
		};

		public static void Get(string site, PageQuery pq = null, ComplexQuery cq = null)
		{
			site.ThrowIfNullOrEmpty(nameof(site));

			if (cq != null &&
				!validSortFields.ContainsKey(cq.Sort) &&
				!validSortFields.ContainsValue(cq.Sort))
			{
				var supportFields = validSortFields.Keys.Aggregate((a, b) => a + ", " + b);
				var errorText = $"Invalid Sort value. Support sort fields: {supportFields}.";
				throw new ArgumentException(errorText, nameof(cq));
			}
			
			//TODO: Finish implemention.
		}
	}
}
