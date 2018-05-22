namespace SharpExchange.Chat.Events.Message
{
	public class MovedMessage
	{
		public int MovedBy { get; internal set; }

		public int MessageId { get; internal set; }
	}
}
