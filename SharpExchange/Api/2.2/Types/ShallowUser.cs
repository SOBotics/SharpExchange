using Newtonsoft.Json;

namespace SharpExchange.Api.V22.Types
{
	public class ShallowUser
	{
		[JsonProperty("accept_rate")]
		public int? AcceptRate { get; internal set; }

		[JsonProperty("badge_counts")]
		public BadgeCount Badges { get; internal set; }

		[JsonProperty("display_name")]
		public string Name { get; internal set; }

		[JsonProperty("link")]
		public string ProfileLink { get; internal set; }

		[JsonProperty("profile_image")]
		public string AvatarLink { get; internal set; }

		[JsonProperty("reputation")]
		public int? Reputation { get; internal set; }

		[JsonProperty("user_id")]
		public int? Id { get; internal set; }

		[JsonProperty("user_type")]
		public UserType? UserType { get; internal set; }
	}
}
