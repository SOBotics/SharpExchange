using System;
using Newtonsoft.Json.Linq;

namespace SharpExchange.Chat.Events.User
{
	public class UserLeft : ChatEventDataProcessor, IChatEventHandler<int>
	{
		public override EventType Event => EventType.UserLeft;

		public event Action<int> OnEvent;

		public override void ProcessEventData(JToken data)
		{
			var userId = data.Value<int>("user_id");

			OnEvent?.Invoke(userId);
		}
	}
}
