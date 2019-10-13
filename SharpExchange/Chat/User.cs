using System;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using SharpExchange.Net;

namespace SharpExchange.Chat
{
	public class User
	{
		public class Room
		{
			public int RoomId { get; internal set; }

			public string RoomName { get; internal set; }

			public int UserId { get; internal set; }

			public int MessagesOwned { get; set; }

			public override string ToString() => RoomName;
		}

		private const string modChar = "♦";
		private const string userProfilePath = "https://{0}/users/{1}";

		public string Host { get; private set; }

		public int Id { get; private set; }

		public string Username { get; private set; }

		public bool IsModerator { get; private set; }

		public DateTime AccountCreatedOn { get; private set; }

		public string About { get; private set; }

		public int Reputation { get; private set; }

		public MessageCountStats MessageStats { get; private set; }

		public int AllTimeMessages { get; private set; }

		public int AllTimeRooms { get; private set; }

		public Room[] Owns { get; private set; }

		public Room[] CurrentlyIn { get; private set; }



		public User(string host, int userId, IAuthenticationProvider auth = null)
		{
			host.ThrowIfNullOrEmpty(nameof(host));

			host = host.GetChatHost();

			var url = string.Format(userProfilePath, host, userId);
			GetWithStatusResult result = null;

			if (auth == null)
			{
				result = HttpRequest.GetWithStatusAsync(url).Result;
			}
			else
			{
				result = HttpRequest.GetWithStatusAsync(url, auth[host]).Result;
			}

			if (result.Status != System.Net.HttpStatusCode.OK)
			{
				throw new Exception($"Unable to find user {userId} on {host}.");
			}

			var dom = new HtmlParser().ParseDocument(result.Body);

			Host = host;
			Id = userId;
			Username = GetUsername(dom, out var isMod);
			IsModerator = isMod;
			AccountCreatedOn = GetAccountCreated(dom);
			About = GetBio(dom);
			Reputation = GetReputation(dom);
			MessageStats = GetMessageStats(dom);
			AllTimeMessages = GetAllTimeMessages(dom);
			AllTimeRooms = GetAllTimeRooms(dom);
			Owns = GetOwnedRooms(dom);
			CurrentlyIn = GetCurrentlyInRooms(dom);
		}



		public static bool operator ==(User a, User b)
		{
			var aNull = ReferenceEquals(a, null);
			var bNull = ReferenceEquals(b, null);

			if (aNull && bNull)
			{
				return true;
			}

			if (!aNull)
			{
				return a.Equals(b);
			}

			return b.Equals(a);
		}

		public static bool operator !=(User a, User b) => !(a == b);

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(obj, null)) return false;

			var u = obj as User;

			if (ReferenceEquals(u, null)) return false;

			return GetHashCode() == u.GetHashCode();
		}

		public override int GetHashCode() => new { Host, Id }.GetHashCode();

		public override string ToString() => Username;

		public static async Task<User> GetMeAsync(IAuthenticationProvider auth, string host)
		{
			auth.ThrowIfNull(nameof(auth));
			host.ThrowIfNullOrEmpty(nameof(host));

			host = host.GetChatHost();

			var url = $"https://{host}/faq";
			var html = await HttpRequest.GetAsync(url, auth[host]);
			var dom = await new HtmlParser().ParseDocumentAsync(html);
			var idStr = dom
				.QuerySelector(".topbar-menu-links a")
				?.Attributes["href"]
				?.Value
				.Split('/')[2];

			if (string.IsNullOrEmpty(idStr))
			{
				return null;
			}

			if (!int.TryParse(idStr, out var id))
			{
				return null;
			}

			return new User(host, id);
		}

		public static Task<User> GetAsync(string host, int userId, IAuthenticationProvider auth = null)
		{
			return Task.Run(() => new User(host, userId, auth));
		}



		private string GetUsername(IHtmlDocument dom, out bool isMod)
		{
			var name = dom
				.QuerySelector(".user-status")
				?.TextContent
				?.Trim();

			if (name?.EndsWith(modChar) ?? false)
			{
				isMod = true;
				name = name.Substring(0, name.Length - 1).Trim();
			}
			else
			{
				isMod = false;
			}

			return name;
		}

		private DateTime GetAccountCreated(IHtmlDocument dom)
		{
			var dateStr = dom
				.QuerySelector(".user-stats tr:first-child > td:last-child")
				?.TextContent;

			return DateTime.Parse(dateStr);
		}

		private string GetBio(IHtmlDocument dom)
		{
			var bio = dom
				.QuerySelector(".user-stats tr:nth-of-type(4) > td:last-child")
				?.TextContent;

			return bio;
		}

		private int GetReputation(IHtmlDocument dom)
		{
			var repStr = dom
				.QuerySelector(".reputation-score")
				?.Attributes["title"]
				?.Value;

			return int.Parse(repStr);
		}

		private MessageCountStats GetMessageStats(IHtmlDocument dom)
		{
			var allStats = dom.QuerySelectorAll(".stats-count");
			var dayCurrentStr = "0";
			var dayAvgStr = "0";
			var weekCurrentStr = "0";
			var weekAvgStr = "0";

			for (var i = 0; i < allStats.Length; i++)
			{
				switch (i)
				{
					case 0:
					{
						dayCurrentStr = allStats[i].TextContent;
						break;
					}
					case 2:
					{
						dayAvgStr = allStats[i].TextContent;
						break;
					}
					case 3:
					{
						weekCurrentStr = allStats[i].TextContent;
						break;
					}
					case 5:
					{
						weekAvgStr = allStats[i].TextContent;
						break;
					}
				}
			}

			var allTimeMsgStr = dom
				.QuerySelector(".user-message-count-xxl")
				?.TextContent;

			return new MessageCountStats
			{
				AllTime = allTimeMsgStr.ParseFriendlyNumber(),
				DayCurrent = dayCurrentStr.ParseFriendlyNumber(),
				DayAverage = dayAvgStr.ParseFriendlyNumber(),
				WeekCurrent = weekCurrentStr.ParseFriendlyNumber(),
				WeekAverage = weekAvgStr.ParseFriendlyNumber(),
			};
		}

		private int GetAllTimeMessages(IHtmlDocument dom)
		{
			var countStr = dom
				.QuerySelector(".user-message-count-xxl")
				?.TextContent;

			return int.TryParse(countStr, out var count)
				? count
				: 0;
		}

		private int GetAllTimeRooms(IHtmlDocument dom)
		{
			var countStr = dom
				.QuerySelector(".user-room-count-xxl")
				?.TextContent;

			return int.TryParse(countStr, out var count)
				? count
				: 0;
		}

		private Room[] GetOwnedRooms(IHtmlDocument dom)
		{
			var rooms = dom.QuerySelectorAll("#user-owningcards > div");

			return GetRooms(rooms);
		}

		private Room[] GetCurrentlyInRooms(IHtmlDocument dom)
		{
			var rooms = dom.QuerySelectorAll("#user-roomcards-container > div");

			return GetRooms(rooms);
		}

		private Room[] GetRooms(IHtmlCollection<IElement> roomElements)
		{
			if (roomElements == null)
			{
				return null;
			}

			var rooms = new Room[roomElements.Length];

			for (var i = 0; i < rooms.Length; i++)
			{
				var idStr = roomElements[i].Id.Remove(0, 5);
				var name = roomElements[i]
					.QuerySelector(".room-name")
					?.Attributes["title"]
					?.Value;

				var msgCountStr = roomElements[i]
					.QuerySelector(".room-message-count")
					?.Attributes["title"]
					?.Value
					.Split()[0];

				_ = int.TryParse(msgCountStr ?? "0", out var msgCount);

				rooms[i] = new Room
				{
					RoomId = int.Parse(idStr),
					UserId = Id,
					RoomName = name,
					MessagesOwned = msgCount
				};
			}

			return rooms;
		}
	}
}
