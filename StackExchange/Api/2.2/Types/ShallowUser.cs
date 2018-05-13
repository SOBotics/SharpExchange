using Newtonsoft.Json;

namespace StackExchange.Api.v22.Types
{
	public class ShallowUser
	{
		[JsonProperty("accept_rate")]
		public int? AcceptRate { get; set; }

		[JsonProperty("badge_counts ")]
		public BadgeCount Badges { get; set; }

		[JsonProperty("display_name")]
		public string Name { get; set; }

		[JsonProperty("link")]
		public string ProfileLink { get; set; }

		[JsonProperty("profile_image")]
		public string AvatarLink { get; set; }

		[JsonProperty("reputation")]
		public int? Reputation { get; set; }

		[JsonProperty("user_id")]
		public int? Id { get; set; }

		[JsonProperty("user_type")]
		public UserType? UserType { get; set; }
	}
}
