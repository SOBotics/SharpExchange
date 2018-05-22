using System;
using RestSharp;

namespace SharpExchange.Net
{
	public class VerbNotSupportedException : Exception
	{
		public VerbNotSupportedException(Method verb) : base($"The HTTP verb {verb} is not supported.") { }

		public VerbNotSupportedException(string message) : base(message) { }

		public VerbNotSupportedException(string message, Exception innerException) : base(message, innerException) { }
	}

	public class InvalidEndpointException : Exception
	{
		public InvalidEndpointException(string message) : base(message) { }

		public InvalidEndpointException(string message, Exception innerException) : base(message, innerException) { }
	}

	public class RequestAlreadySentException : Exception
	{
		public RequestAlreadySentException() : base("This HTTP request has already been sent.") { }

		public RequestAlreadySentException(string message) : base(message) { }

		public RequestAlreadySentException(string message, Exception innerException) : base(message, innerException) { }
	}

	public class InvalidRedirectException : Exception
	{
		public InvalidRedirectException() : base("The remote server did not specify where the request should redirect to.") { }

		public InvalidRedirectException(string message) : base(message) { }

		public InvalidRedirectException(string message, Exception innerException) : base(message, innerException) { }
	}
}
