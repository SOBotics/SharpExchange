using Newtonsoft.Json;

namespace SharpExchange.Api.V22.Types
{
	public class ClosedDetails
	{
		[JsonProperty("by_users")]
		public ShallowUser[] ClosedBy { get; internal set; }

		[JsonProperty("description")]
		public string Description { get; internal set; }

		[JsonProperty("reason")]
		public string Reason { get; internal set; }

		[JsonProperty("on_hold")]
		public bool? OnHold { get; internal set; }

		[JsonProperty("original_questions")]
		public OriginalQuestion[] DuplicateOf { get; internal set; }
	}
}
