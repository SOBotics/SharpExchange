using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Api.v22.Types;

namespace StackExchange.Api.v22.Endpoints
{
	public static class Answers
	{
		private static readonly Dictionary<string, string> validSortFields = new Dictionary<string, string>
		{
			["activity"] = "last_activity_date",
			["creation"] = "creation_date",
			["votes"] = "score"
		};

		public static Result<Answer[]> Get(BasicQuery bq = null, PageQuery pq = null, ComplexQuery cq = null)
		{
			if (cq != null &&
				!validSortFields.ContainsKey(cq.Sort) &&
				!validSortFields.ContainsValue(cq.Sort))
			{
				var supportFields = validSortFields.Keys.Aggregate((a, b) => a + ", " + b);
				var errorText = $"Invalid Sort value. Support sort fields: {supportFields}.";

				throw new ArgumentException(errorText, nameof(cq));
			}

			//TODO: Finish implementation.

			throw new NotImplementedException();
		}
	}
}
