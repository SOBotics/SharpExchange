using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SharpExchange.Api.V22.Types
{
	public class MigrationInfo
	{
		[JsonProperty("on_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? MigratedOn { get; internal set; }

		[JsonProperty("other_site")]
		public Site TargetSite { get; internal set; }

		[JsonProperty("question_id")]
		public int? QuestionId { get; internal set; }
	}
}
