using Newtonsoft.Json;

namespace SharpExchange.Api.V22.Types
{
	public class Privilege
	{
		[JsonProperty("description")]
		public string Description { get; internal set; }

		[JsonProperty("short_description")]
		public string ShortDescription { get; internal set; }

		[JsonProperty("reputation")]
		public int RequiredReputation { get; internal set; }
	}
}
