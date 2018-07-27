using System;
using Newtonsoft.Json.Linq;
using SharpExchange.Auth;
using SharpExchange.Chat.Events;
using SharpExchange.Chat.Events.User.Extensions;
using SharpExchange.Net.WebSockets;
using SharpExchange.Chat.Actions;
using System.Threading.Tasks;
using SharpExchange.Api.V22.Endpoints;
using SharpExchange.Api.V22;
using System.Collections.Generic;

public class Program
{
	private static void Main(string[] args) => Demo().Wait();

	private static async Task Demo()
	{
		// All throttling/rate limiting is handled by the library,
		// there's no need to worry about managing that.

		// Fetch some recent questions on the default site (Stack
		// Overflow).
		var questions = await Questions.GetAllAsync();

		// Here we use the API filter parameter to specify which fields
		// we're interested in. In this case, we only want the title
		// of the returned questions.
		// Note: the default filter usually does NOT include all fields
		// of data with the returned type.
		var onlyQuestionTitles = await Questions.GetAllAsync(new QueryOptions
		{
			Filter = "!C(o*VkSJvGMV1rYk-"
		});

		// The Filter param is just one of a few different ways you can
		// manipulate the returned data from the API. Here we're using
		// the OrderBy and SortBy params to get the highest scoring questions.
		// Note: not all SortBy values are valid for all endpoints, some
		// endpoints don't even support sorting.
		var highestScoringQuestions = await Questions.GetAllAsync(new QueryOptions
		{
			SortBy = SortBy.Votes,
			OrderBy = OrderBy.Descending
		});

		// However, this library currently doesn't explicitly expose every
		// param for every endpoint. You can still use these "missing" params
		// via the Custom property; just specify the name and value of the
		// param you'd like to use.
		var taggedQuestions = await Questions.GetAllAsync(new QueryOptions
		{
			Custom = new Dictionary<string, string>
			{
				["tagged"] = "c#"
			}
		});
	}
}