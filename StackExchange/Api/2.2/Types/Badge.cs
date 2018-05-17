using Newtonsoft.Json;

namespace StackExchange.Api.V22.Types
{
	public class Badge
	{
		[JsonProperty("award_count")]
		public int? AwardCount { get; set; }

		[JsonProperty("badge_id")]
		public int Id { get; set; }

		[JsonProperty("badge_type")]
		public BadgeType? Type { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("link")]
		public string Link { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("rank")]
		public BadgeRank? Rank { get; set; }

		[JsonProperty("user")]
		public ShallowUser User { get; set; }
	}
}
