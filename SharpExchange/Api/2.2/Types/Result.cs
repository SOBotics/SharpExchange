using Newtonsoft.Json;

namespace SharpExchange.Api.V22.Types
{
	public class Result<T>
	{
		[JsonProperty("backoff")]
		public int? BackOff { get; set; }

		[JsonProperty("error_id")]
		public int? ErrorId { get; set; }

		[JsonProperty("error_message")]
		public string ErrorMessage { get; set; }

		[JsonProperty("error_name")]
		public string ErrorName { get; set; }

		[JsonProperty("has_more")]
		public bool? HasMore { get; set; }

		[JsonProperty("items")]
		public T Items { get; set; }

		[JsonProperty("page")]
		public int? Page { get; set; }

		[JsonProperty("page_size")]
		public int? PageSize { get; set; }

		[JsonProperty("quota_max")]
		public int? QuotaMaximum { get; set; }

		[JsonProperty("quota_remaining")]
		public int? QuotaRemaining { get; set; }

		[JsonProperty("total")]
		public int? TotalItems { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }
	}
}
