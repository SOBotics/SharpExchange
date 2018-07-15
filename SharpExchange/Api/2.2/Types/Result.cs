using Newtonsoft.Json;

namespace SharpExchange.Api.V22.Types
{
	public class Result<T>
	{
		[JsonProperty("backoff")]
		public int? BackOff { get; internal set; }

		[JsonProperty("error_id")]
		public int? ErrorId { get; internal set; }

		[JsonProperty("error_message")]
		public string ErrorMessage { get; internal set; }

		[JsonProperty("error_name")]
		public string ErrorName { get; internal set; }

		[JsonProperty("has_more")]
		public bool? HasMore { get; internal set; }

		[JsonProperty("items")]
		public T Items { get; internal set; }

		[JsonProperty("page")]
		public int? Page { get; internal set; }

		[JsonProperty("page_size")]
		public int? PageSize { get; internal set; }

		[JsonProperty("quota_max")]
		public int? QuotaMaximum { get; internal set; }

		[JsonProperty("quota_remaining")]
		public int? QuotaRemaining { get; internal set; }

		[JsonProperty("total")]
		public int? TotalItems { get; internal set; }

		[JsonProperty("type")]
		public string Type { get; internal set; }
	}
}
