using System;
using Newtonsoft.Json.Linq;
using StackExchange.Auth;
using StackExchange.Chat;
using StackExchange.Chat.Events;
using StackExchange.Chat.Events.Room.Extensions;
using StackExchange.Chat.Events.User.Extensions;
using StackExchange.Chat.Events.Message.Extensions;
using StackExchange.Net.WebSockets;
using StackExchange.Chat.Events.Message;
using StackExchange.Chat.Actions;
using StackExchange.Chat.Actions.Message;

namespace Demo
{
	public class AllData : IChatEventDataProcessor, IChatEventHandler<string>
	{
		// The type of event we want to process.
		public EventType Event => EventType.All;

		public event Action<string> OnEvent;

		// Process the incoming JSON data coming from the RoomWatcher's
		// WebSocket. In this example, we just stringify the object and
		// invoke any listeners.
		public void ProcessEventData(JToken data) => OnEvent?.Invoke(data.ToString());
	}

	public class Program
	{
		// This stuff should ideally be loaded in from a configuration provider.
		private const string mainSiteHost = "stackoverflow.com";
		private const string chatHost = "chat." + mainSiteHost;
		private const string roomUrl = "https://chat.stackoverflow.com/rooms/167908";

		static void Main(string[] args)
		{
			// Fetch your account's credentials from somewhere.
			var auth = new EmailAuthenticationProvider("", "");
			
			// Get the authentication cookies.
			var cookies = auth.GetAuthCookies(mainSiteHost);

			// Create an instance of the ActionScheduler. This will
			// allow us to execute chat actions like: posting messages,
			// kicking users, moving messages, etc.
			var actionScheduler = new ActionScheduler(cookies, roomUrl);

			// Create an instance of the RoomWatcher class. Here we
			// specify (via the type parameter) what WebSocket implementation
			// we'd like to use. This class allows you to subscribe to chat events.
			var roomWatcher = new RoomWatcher<DefaultWebSocket>(cookies, roomUrl);

			// Fetch the current user we're logged in as. (So we can
			// ignore events caused by ourself later on.)
			var me = User.GetMe(cookies, chatHost);

			// Post a simple message.
			var messageId = actionScheduler.CreateMessage("Hello world.");

			// Subscribe to the MessageCreated event.
			roomWatcher.AddMessageCreatedEventHandler(m =>
			{
				if (m.AuthorId != me.Id && m.Text == "explode")
				{
					actionScheduler.CreateMessage("*boom*");
				}
			});

			// Besides being able to subscribe to the default events,
			// you can also create (and listen to) your own. Your class must
			// implement the IChatEventDataProcessor interface, you can also
			// optionally implement the IChatEventHandler or IChatEventHandler<T>.
			var customEventHanlder = new AllData();

			// Add a very basic handler.
			customEventHanlder.OnEvent += data => Console.WriteLine(data);

			// Add our custom event handler to the RoomWatcher so we
			// can begin processing the incoming event data.
			roomWatcher.AddEventHandler(customEventHanlder);

			Console.Read();
		}
	}
}
