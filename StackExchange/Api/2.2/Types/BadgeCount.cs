using Newtonsoft.Json;

namespace StackExchange.Api.v22.Types
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
