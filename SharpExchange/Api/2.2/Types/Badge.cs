using Newtonsoft.Json;

namespace SharpExchange.Api.V22.Types
{
	public class Badge
	{
		[JsonProperty("award_count")]
		public int? AwardCount { get; internal set; }

		[JsonProperty("badge_id")]
		public int Id { get; internal set; }

		[JsonProperty("badge_type")]
		public BadgeType? Type { get; internal set; }

		[JsonProperty("description")]
		public string Description { get; internal set; }

		[JsonProperty("link")]
		public string Link { get; internal set; }

		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("rank")]
		public BadgeRank? Rank { get; internal set; }

		[JsonProperty("user")]
		public ShallowUser User { get; internal set; }
	}
}
