using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Api.v22.Types;

namespace StackExchange.Api.v22.Endpoints
{
	public static class Answers
	{
		public static Result<Answer[]> GetAll(QueryOptions options = null)
		{
			throw new NotImplementedException();
		}

		public static Result<Answer[]> GetByIds(IEnumerable<int> ids, QueryOptions options = null)
		{
			throw new NotImplementedException();
		}

		public static Result<Answer[]> GetByQuestionIds(IEnumerable<int> questionIds, QueryOptions options = null)
		{
			throw new NotImplementedException();
		}

		public static Result<Answer[]> GetByUserIds(IEnumerable<int> userIds, QueryOptions options = null)
		{
			throw new NotImplementedException();
		}

		public static Result<Answer[]> GetTopAnswersByUserByTags(int userId, IEnumerable<string> tags, QueryOptions options = null)
		{
			throw new NotImplementedException();
		}
	}
}
