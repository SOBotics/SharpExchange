using System.Net;

namespace StackExchange.Net
{
	public class GetWithStatusResult
	{
		public string Body { get; private set; }

		public HttpStatusCode Status { get; private set; }

		public GetWithStatusResult(string b, HttpStatusCode s)
		{
			Body = b;
			Status = s;
		}
	}
}
