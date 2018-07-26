using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SharpExchange.Api.V22.Types
{
	public class TagWiki
	{
		[JsonProperty("body")]
		public string Body { get; internal set; }

		[JsonProperty("body_last_edit_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? BodyLastEditedOn { get; internal set; }

		[JsonProperty("excerpt")]
		public string Excerpt { get; internal set; }

		[JsonProperty("excerpt_last_edit_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? ExcerptLastEditedOn { get; internal set; }

		[JsonProperty("last_body_editor")]
		public ShallowUser LastBodyEditor { get; internal set; }

		[JsonProperty("last_excerpt_editor")]
		public ShallowUser LastExceprtEditor { get; internal set; }

		[JsonProperty("tag_name")]
		public string TagName { get; internal set; }
	}
}
