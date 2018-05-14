using System;

namespace StackExchange.Api.v22
{
	internal class ApiQueryValueAttribute : Attribute
	{
		public string Value { get; private set; }

		public ApiQueryValueAttribute(string v) => Value = v;
	}
}
