using Newtonsoft.Json;

namespace SharpExchange.Api.V22.Types
{
	public class RelatedSite
	{
		[JsonProperty("api_site_parameter")]
		public string ApiSiteParameter { get; internal set; }

		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("relation")]
		public string Relation { get; internal set; }

		[JsonProperty("site_url")]
		public string Url { get; internal set; }
	}
}
