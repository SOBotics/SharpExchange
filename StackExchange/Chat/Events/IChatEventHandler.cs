using System;
using System.Collections.Generic;
using System.Text;

namespace StackExchange.Chat.Events
{
	public interface IChatEventHandler<T>
	{
		event Action<T> OnEvent;
	}
}
