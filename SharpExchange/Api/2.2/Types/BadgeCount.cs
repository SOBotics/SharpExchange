using Newtonsoft.Json;

namespace SharpExchange.Api.V22.Types
{
	public class BadgeCount
	{
		[JsonProperty("gold")]
		public int? Gold { get; set; }

		[JsonProperty("silver")]
		public int? Silver { get; set; }

		[JsonProperty("bronze")]
		public int? Bronze { get; set; }
	}
}
