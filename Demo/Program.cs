using System;
using Newtonsoft.Json.Linq;
using StackExchange.Auth;
using StackExchange.Chat;
using StackExchange.Chat.Events;
using StackExchange.Chat.Events.User.Extensions;
using StackExchange.Net.WebSockets;
using StackExchange.Chat.Actions;

namespace Demo
{
	public class AllData : ChatEventDataProcessor, IChatEventHandler<string>
	{
		// The type of event we want to process.
		public override EventType Event => EventType.All;

		public event Action<string> OnEvent;

		// Process the incoming JSON data coming from the RoomWatcher's
		// WebSocket. In this example, we just stringify the object and
		// invoke any listeners.
		public override void ProcessEventData(JToken data) => OnEvent?.Invoke(data.ToString());
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

			// Create an instance of the ActionScheduler. This will
			// allow us to execute chat actions like: posting messages,
			// kicking users, moving messages, etc.
			var actionScheduler = new ActionScheduler(auth, roomUrl);

			// Create an instance of the RoomWatcher class. Here we
			// specify (via the type parameter) what WebSocket implementation
			// we'd like to use. This class allows you to subscribe to chat events.
			var roomWatcher = new RoomWatcher<DefaultWebSocket>(auth, roomUrl);

			// Fetch the current user we're logged in as. (So we can
			// ignore events caused by ourself later on.)
			var me = User.GetMe(auth, chatHost);

			// Post a simple message.
			var messageId = actionScheduler.CreateMessage("Hello world.");

			// Subscribe to the UserMentioned event.
			roomWatcher.AddUserMentionedEventHandler(m =>
			{
				actionScheduler.CreateReply("hello!", m);
			});

			// Besides being able to subscribe to the default events,
			// you can also create (and listen to) your own. Your class must
			// implement the ChatEventDataProcessor class, you can also
			// optionally implement the IChatEventHandler or IChatEventHandler<T>.
			var customEventHanlder = new AllData();

			// Add a very basic handler.
			customEventHanlder.OnEvent += data => Console.WriteLine(data);

			// Add our custom event handler so we can
			// begin processing the incoming event data.
			roomWatcher.EventRouter.AddProcessor(customEventHanlder);

			Console.Read();
		}
	}
}
