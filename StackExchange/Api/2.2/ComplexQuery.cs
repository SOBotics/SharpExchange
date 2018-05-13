using System;

namespace StackExchange.Api.v22.Types
{
	public class ComplexQuery
	{
		public enum OrderBy
		{
			Ascending,
			Descending
		}

		/// <summary>
		/// Defines which field Min/Max (and Order) apply to.
		/// </summary>
		public string Sort { get; set; }

		/// <summary>
		/// How the results should be ordered.
		/// </summary>
		public OrderBy Order { get; set; }

		/// <summary>
		/// The inclusive lower bound of a range that
		/// a field must fall in (field is specified
		/// by the Sort property).
		/// </summary>
		public int Min { get; set; }

		/// <summary>
		/// The inclusive upper bound of a range that
		/// a field must fall in (field is specified
		/// by the Sort property).
		/// </summary>
		public int Max { get; set; }

		/// <summary>
		/// Defines the start of a range to apply to the CreationDate field.
		/// </summary>
		public DateTime FromData { get; set; }

		/// <summary>
		/// Defines the end of a range to apply to the CreationDate field.
		/// </summary>
		public DateTime ToDate { get; set; }
	}
}
