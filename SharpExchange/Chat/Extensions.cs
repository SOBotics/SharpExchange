namespace SharpExchange.Chat
{
	public static class Extensions
	{
		public static string RemoveReplyPrefix(this string messageText)
		{
			if (string.IsNullOrEmpty(messageText))
			{
				return null;
			}

			var endOfPrefixIndex = messageText.IndexOf(' ');

			if (messageText.StartsWith(":") && endOfPrefixIndex != -1)
			{
				return messageText.Substring(endOfPrefixIndex + 1);
			}

			return messageText;
		}
	}
}
