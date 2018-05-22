namespace SharpExchange.Chat
{
	public class MessageCountStats
	{
		public int AllTime { get; internal set; }

		public int DayCurrent { get; internal set; }

		public int DayAverage { get; internal set; }

		public int WeekCurrent { get; internal set; }

		public int WeekAverage { get; internal set; }
	}
}
