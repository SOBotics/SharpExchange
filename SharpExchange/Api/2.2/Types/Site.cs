using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SharpExchange.Api.V22.Types
{
	public class Site
	{
		[JsonProperty("aliases")]
		public string[] Aliases { get; internal set; }

		[JsonProperty("api_site_parameter")]
		public string ApiSiteParameter { get; internal set; }

		[JsonProperty("audience")]
		public string Audience { get; internal set; }

		[JsonProperty("closed_beta_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? ClosedBetaStartedOn { get; internal set; }

		[JsonProperty("favicon_url")]
		public string FaviconUrl { get; internal set; }

		[JsonProperty("high_resolution_icon_url")]
		public string HighResolutionIconUrl { get; internal set; }

		[JsonProperty("icon_url")]
		public string IconUrl { get; internal set; }

		[JsonProperty("launch_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? LaunchedOn { get; internal set; }

		[JsonProperty("logo_url")]
		public string LogoUrl { get; internal set; }

		[JsonProperty("markdown_extensions")]
		public string[] MarkdownExtensions { get; internal set; }

		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("open_beta_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? OpenBetaStartedOn { get; internal set; }

		[JsonProperty("related_sites")]
		public RelatedSite[] RelatedSites { get; internal set; }

		[JsonProperty("site_state")]
		public SiteState? State { get; internal set; }

		[JsonProperty("site_type")]
		public string Type { get; internal set; }

		[JsonProperty("site_url")]
		public string Url { get; internal set; }

		[JsonProperty("styling")]
		public Styling Styling { get; internal set; }

		[JsonProperty("twitter_account")]
		public string TwitterHandle { get; internal set; }
	}
}
