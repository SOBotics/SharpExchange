using System;
using Newtonsoft.Json.Linq;

namespace StackExchange.Chat.Events.User
{
	public class UserLeft : IChatEventDataProcessor, IChatEventHandler<int>
	{
		public EventType Event => EventType.UserLeft;

		public event Action<int> OnEvent;

		public void ProcessEventData(JToken data)
		{
			var userId = data.Value<int>("user_id");

			OnEvent?.Invoke(userId);
		}
	}
}
