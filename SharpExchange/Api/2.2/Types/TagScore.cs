using Newtonsoft.Json;

namespace SharpExchange.Api.V22.Types
{
	public class TagScore
	{
		[JsonProperty("post_count")]
		public int? PostCount { get; internal set; }

		[JsonProperty("score")]
		public int? Score { get; internal set; }

		[JsonProperty("user")]
		public ShallowUser User { get; internal set; }
	}
}
