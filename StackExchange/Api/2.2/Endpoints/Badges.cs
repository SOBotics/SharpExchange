using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Api.V22.Types;

namespace StackExchange.Api.V22.Endpoints
{
	public static class Badges
	{
		/// <summary>
		/// Gets all badges on a site, in alphabetical order.
		/// </summary>
		public static Result<Badge[]> GetAll(QueryOptions options = null)
		{
			return GetAllAsync(options).Result;
		}

		/// <summary>
		/// Gets all badges on a site, in alphabetical order.
		/// </summary>
		public static async Task<Result<Badge[]>> GetAllAsync(QueryOptions options = null)
		{

		}

		/// <summary>
		/// Gets the badges whose name contains the specified
		/// search term, in alphabetical order.
		/// </summary>
		public static Result<Badge[]> GetByName(string name, QueryOptions options = null)
		{
			return GetByNameAsync(name, options).Result;
		}

		/// <summary>
		/// Gets the badges whose name contains the specified
		/// search term, in alphabetical order.
		/// </summary>
		public static async Task<Result<Badge[]>> GetByNameAsync(string name, QueryOptions options = null)
		{

		}


	}
}
