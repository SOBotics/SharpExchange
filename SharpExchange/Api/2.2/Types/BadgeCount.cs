using Newtonsoft.Json;

namespace SharpExchange.Api.V22.Types
{
	public class BadgeCount
	{
		[JsonProperty("gold")]
		public int? Gold { get; internal set; }

		[JsonProperty("silver")]
		public int? Silver { get; internal set; }

		[JsonProperty("bronze")]
		public int? Bronze { get; internal set; }
	}
}
