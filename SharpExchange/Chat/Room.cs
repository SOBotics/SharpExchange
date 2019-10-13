using System;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using SharpExchange.Net;

namespace SharpExchange.Chat
{
	public class Room
	{
		public class User
		{
			public int UserId { get; internal set; }

			public int RoomId { get; internal set; }

			public string Username { get; internal set; }

			public int MessagesOwned { get; internal set; }

			public override string ToString() => Username;
		}

		private const string modChar = "♦";
		private const string roomProfilePath = "https://{0}/rooms/info/{1}";

		public string Host { get; private set; }

		public int Id { get; private set; }

		public string Name { get; private set; }

		public RoomStates States { get; private set; }

		public string Description { get; private set; }

		public string[] Tags { get; private set; }

		public DateTime FirstMessage { get; private set; }

		public MessageCountStats MessageStats { get; private set; }

		public User[] Owners { get; private set; }

		public User[] CurrentUsers { get; private set; }



		public Room(string host, int roomId, IAuthenticationProvider auth = null)
		{
			host.ThrowIfNullOrEmpty(nameof(host));

			host = host.GetChatHost();

			var url = string.Format(roomProfilePath, host, roomId);
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
				throw new Exception($"Unable to find room {roomId} on {host}.");
			}

			var dom = new HtmlParser().ParseDocument(result.Body);

			Host = host;
			Id = roomId;
			Name = GetName(dom);
			States = GetStates(dom);
			Description = GetDescription(dom);
			Tags = GetTags(dom);
			FirstMessage = GetFirstMessage(dom);
			MessageStats = GetMessageStats(dom);
			Owners = GetOwners(dom);
			CurrentUsers = GetCurrentUsers(dom);
		}



		public static bool operator ==(Room a, Room b)
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

		public static bool operator !=(Room a, Room b) => !(a == b);

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(obj, null)) return false;

			var r = obj as Room;

			if (ReferenceEquals(r, null)) return false;

			return GetHashCode() == r.GetHashCode();
		}

		public override int GetHashCode() => new { Host, Id }.GetHashCode();

		public override string ToString() => Name;

		public static Task<Room> GetAsync(string host, int roomId, IAuthenticationProvider auth = null)
		{
			return Task.Run(() => new Room(host, roomId, auth));
		}



		private string GetName(IHtmlDocument dom)
		{
			var name = dom
				.QuerySelector(".roomcard-xxl h1")
				?.TextContent
				?.Trim();

			if (name.StartsWith(modChar))
			{
				name = name.Remove(0, 1).Trim();
			}

			return name;
		}

		private RoomStates GetStates(IHtmlDocument dom)
		{
			var isPrivate = dom.QuerySelector(".sprite-sec-private") != null;
			var isGallery = dom.QuerySelector(".sprite-sec-gallery") != null;
			var isFrozen = dom.QuerySelector(".frozen") != null;

			var states = RoomStates.None;

			if (isPrivate)
			{
				states = RoomStates.Private;
			}
			else if (isGallery)
			{
				states = RoomStates.Gallery;
			}
			else
			{
				states = RoomStates.Normal;
			}

			if (isFrozen)
			{
				states |= RoomStates.Frozen;
			}

			return states;
		}

		private string GetDescription(IHtmlDocument dom)
		{
			return dom.QuerySelector(".roomcard-xxl p")?.TextContent;
		}

		private string[] GetTags(IHtmlDocument dom)
		{
			var tagElements = dom.QuerySelectorAll(".tag");
			var tags = new string[tagElements.Length];

			for (var i = 0; i < tags.Length; i++)
			{
				tags[i] = tagElements[i].TextContent;
			}

			return tags;
		}

		private DateTime GetFirstMessage(IHtmlDocument dom)
		{
			var dateStr = dom
				.QuerySelector(".room-stats tr:first-child > td:last-child")
				?.TextContent;

			if (string.IsNullOrEmpty(dateStr))
			{
				return DateTime.MinValue;
			}

			return DateTime.Parse(dateStr);
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
				.QuerySelector(".room-message-count-xxl")
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

		private User[] GetOwners(IHtmlDocument dom)
		{
			var rooms = dom.QuerySelectorAll("#room-ownercards > div");

			return GetUsers(rooms);
		}

		private User[] GetCurrentUsers(IHtmlDocument dom)
		{
			var rooms = dom.QuerySelectorAll("#room-usercards-container > div");

			return GetUsers(rooms);
		}

		private User[] GetUsers(IHtmlCollection<IElement> userElements)
		{
			if (userElements == null)
			{
				return null;
			}

			var users = new User[userElements.Length];

			for (var i = 0; i < users.Length; i++)
			{
				var idStr = userElements[i].Id.Split('-').Last();
				var name = userElements[i]
					.QuerySelector(".username")
					?.TextContent;

				if (name.EndsWith(modChar))
				{
					name = name.Substring(0, name.Length - 1).Trim();
				}

				var msgCountStr = userElements[i]
					.QuerySelector(".user-message-count")
					?.Attributes["title"]
					?.Value
					.Split()[0];

				_ = int.TryParse(msgCountStr ?? "0", out var msgCount);

				users[i] = new User
				{
					UserId = int.Parse(idStr),
					RoomId = Id,
					Username = name,
					MessagesOwned = msgCount
				};
			}

			return users;
		}
	}
}
