using Newtonsoft.Json.Linq;

namespace StackExchange.Chat.Events
{
	public abstract class ChatEventDataProcessor
	{
		public int RoomId { get; internal set; }

		public abstract EventType Event { get; }

		public abstract void ProcessEventData(JToken data);
	}
}
