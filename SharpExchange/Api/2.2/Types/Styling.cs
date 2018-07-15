using Newtonsoft.Json;

namespace SharpExchange.Api.V22.Types
{
	public class Styling
	{
		[JsonProperty("link_color")]
		public string LinkColor { get; internal set; }

		[JsonProperty("tag_background_color")]
		public string TagBackgroundColor { get; internal set; }

		[JsonProperty("tag_foreground_color")]
		public string TagForegroundColor { get; internal set; }
	}
}
