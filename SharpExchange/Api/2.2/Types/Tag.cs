using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SharpExchange.Api.V22.Types
{
	/// <summary>
	/// This class represents a tag on a Stack Exchange site.
	/// </summary>
	public class Tag
	{
		[JsonProperty("count")]
		public int? Count { get; internal set; }

		[JsonProperty("has_synonyms")]
		public bool? HasSynonyms { get; internal set; }

		[JsonProperty("is_moderator_only")]
		public bool? IsModeratorOnly { get; internal set; }

		[JsonProperty("is_required")]
		public bool? IsRequired { get; internal set; }

		[JsonProperty("last_activity_date")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime? LastActiveOn { get; internal set; }

		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("synonyms")]
		public string[] Synonyms { get; internal set; }

		[JsonProperty("user_id")]
		public int? UserId { get; internal set; }
	}
}
