using Newtonsoft.Json.Linq;

namespace StackExchange.Chat.Events
{
	public interface IChatEventDataProcessor
	{
		EventType Event { get; }

		void ProcessEventData(JToken data);
	}
}
