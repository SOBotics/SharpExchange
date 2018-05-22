using System;
using Newtonsoft.Json.Linq;

namespace SharpExchange.Chat.Events.User
{
	public class UserEntered : ChatEventDataProcessor, IChatEventHandler<int>
	{
		public override EventType Event => EventType.UserEntered;

		public event Action<int> OnEvent;

		public override void ProcessEventData(JToken data)
		{
			var userId = data.Value<int>("user_id");

			OnEvent?.Invoke(userId);
		}
	}
}
