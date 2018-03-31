using System;
using System.Collections.Generic;
using System.Text;

namespace StackExchange.Chat.Events
{
	public interface IChatEventDataProcessor
	{
		EventType Event { get; }

		void ProcessEventData(string json);
	}
}
