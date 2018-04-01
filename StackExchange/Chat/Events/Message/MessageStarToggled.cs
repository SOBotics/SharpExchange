using System;
using Newtonsoft.Json.Linq;

namespace StackExchange.Chat.Events.Message
{
	public class MessageStars
	{
		public int Id { get; internal set; }
		public int Stars { get; internal set; }
		public bool IsPinned { get; internal set; }
		public bool StarredBySelf { get; internal set; }
		public bool PinnedBySelf { get; internal set; }
	}

	public class MessageStarToggled : IChatEventDataProcessor, IChatEventHandler<MessageStars>
	{
		public EventType Event => EventType.MessageStarToggled;

		public event Action<MessageStars> OnEvent;

		public void ProcessEventData(JToken data)
		{
			var msgId = data.Value<int>("message_id");
			var stars = data.Value<int?>("message_stars");
			var pins = data.Value<int?>("message_owner_stars");
			var starredBySelf = data.Value<bool?>("message_starred");
			var pinnedBySelf = data.Value<bool?>("message_owner_starred");

			OnEvent?.Invoke(new MessageStars
			{
				Id = msgId,
				Stars = stars ?? 0,
				IsPinned = pins != null,
				StarredBySelf = starredBySelf != null,
				PinnedBySelf = pinnedBySelf != null
			});
		}
	}
}
