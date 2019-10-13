using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RestSharp;

namespace SharpExchange.Net
{
	public class HttpRequest
	{
		private static readonly List<Method> supportedMethods = new List<Method>
		{
			Method.GET,
			Method.POST
		};
		private static readonly List<HttpStatusCode> handleRedirectCodes = new List<HttpStatusCode>
		{
			HttpStatusCode.Found,
			HttpStatusCode.Moved
		};
		private bool sent;

		public Method Verb { get; set; }

		public string Endpoint { get; set; }

		public CookieManager Cookies { get; set; }

		public Dictionary<string, object> Data { get; set; }



		public static async Task<GetWithStatusResult> GetWithStatusAsync(string endpoint, CookieManager cMan = null)
		{
			var response = await new HttpRequest
			{
				Verb = Method.GET,
				Endpoint = endpoint,
				Cookies = cMan
			}.SendAsync();

			return new GetWithStatusResult(response.Content, response.StatusCode);
		}

		public static async Task<string> GetAsync(string endpoint, CookieManager cookies = null)
		{
			var r = await new HttpRequest
			{
				Verb = Method.GET,
				Endpoint = endpoint,
				Cookies = cookies
			}.SendAsync();

			return r.Content;
		}

		public async Task<RestResponse> SendAsync()
		{
			if (!supportedMethods.Contains(Verb))
			{
				throw new VerbNotSupportedException(Verb);
			}

			if ((!Endpoint?.StartsWith("http")) ?? true)
			{
				throw new InvalidEndpointException($"The endpoint '{Endpoint}' is not valId.");
			}

			if (sent)
			{
				throw new RequestAlreadySentException();
			}

			var endpointUri = new Uri(Endpoint);
			var baseUrl = $"{endpointUri.Scheme}://{endpointUri.Host}";

			var client = new RestClient(baseUrl)
			{
				FollowRedirects = false
			};

			var request = new RestRequest(endpointUri.PathAndQuery, Verb);

			if (Cookies != null)
			{
				var valIdCookies = Cookies.Cookies.Where(x =>
				{
					var domain = x.Domain;

					if (x.Domain.StartsWith("."))
					{
						domain = x.Domain.Remove(0, 1);
					}

					return endpointUri.Host.EndsWith(domain);
				});

				foreach (var c in valIdCookies)
				{
					_ = request.AddCookie(c.Name, c.Value);
				}
			}

			if (Data != null)
			{
				foreach (var k in Data.Keys)
				{
					_ = request.AddParameter(k, Data[k]);
				}
			}

			var response = await Task.Run(() => (RestResponse)client.Execute(request));

			Cookies?.Add(response.Cookies);

			if (handleRedirectCodes.Contains(response.StatusCode))
			{
				response = HandleRedirect(response, baseUrl);
			}

			sent = true;

			return response;
		}



		private RestResponse HandleRedirect(RestResponse response, string baseUrl)
		{
			var redirectTo = response.Headers
				.Where(x => x.Name == "Location")
				.Select(x => new Uri(x.Value.ToString(), UriKind.RelativeOrAbsolute))
				.ToArray();

			if (redirectTo.Length != 1)
			{
				throw new InvalidRedirectException();
			}

			var endpoint = redirectTo[0].OriginalString;

			if (!redirectTo[0].IsAbsoluteUri)
			{
				endpoint = baseUrl + endpoint;
			}

			return new HttpRequest
			{
				Verb = Method.GET,
				Endpoint = endpoint,
				Cookies = Cookies
			}.SendAsync().Result;
		}
	}
}
