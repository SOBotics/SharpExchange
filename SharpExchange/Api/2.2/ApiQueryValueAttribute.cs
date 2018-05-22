using System;

namespace SharpExchange.Api.V22
{
	internal class ApiQueryValueAttribute : Attribute
	{
		public string Value { get; private set; }

		public ApiQueryValueAttribute(string v) => Value = v;
	}
}
