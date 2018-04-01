using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using StackExchange.Net;

namespace StackExchange.Chat
{
	public class Room
	{
		public class User
		{
			public int Id { get; internal set; }

			public string Name { get; internal set; }

			public override string ToString() => Name;
		}

		private const string modChar = "♦";
		private const string roomProfilePath = "https://{0}/rooms/info/{1}";

		public int Id { get; private set; }

		public string Name { get; private set; }

		public bool IsGallery { get; private set; }

		public string Description { get; private set; }

		public string[] Tags { get; private set; }

		public DateTime FirstMessage { get; private set; }

		public MessageCountStats MessageStats { get; private set; }

		public User[] Owners { get; private set; }

		public User[] CurrentUsers { get; private set; }



		public Room(string host, int roomId)
		{
			var url = string.Format(roomProfilePath, host, roomId);
			var html = HttpRequest.GetWithStatus(url, out var status);

			if (status != System.Net.HttpStatusCode.OK)
			{
				throw new Exception($"Unable to find user {roomId} on {host}.");
			}

			var dom = new HtmlParser().Parse(html);

			Id = roomId;
			Name = GetName(dom, out var isGallery);
			IsGallery = isGallery;
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

			var m = obj as User;

			if (ReferenceEquals(m, null)) return false;

			return m.Id == Id;
		}

		public override int GetHashCode() => Id;

		public override string ToString()
		{
			if (Name == null)
			{
				throw new NullReferenceException();
			}

			return Name;
		}



		private string GetName(IHtmlDocument dom, out bool isGallery)
		{
			var name = dom
				.QuerySelector(".roomcard-xxl h1")
				?.TextContent
				?.Trim();

			if (name.StartsWith(modChar))
			{
				name = name.Remove(0, 1).Trim();
			}

			var galleryEl = dom.QuerySelector(".sprite-sec-gallery");

			isGallery = galleryEl != null;

			return name;
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

		private User[] GetUsers(IHtmlCollection<IElement> users)
		{
			if (users == null)
			{
				return null;
			}

			var ids = new User[users.Length];

			for (var i = 0; i < ids.Length; i++)
			{
				var idStr = users[i].Id.Split('-').Last();
				var name = users[i]
					.QuerySelector(".username")
					?.TextContent;

				if (name.EndsWith(modChar))
				{
					name = name.Substring(0, name.Length - 1).Trim();
				}

				ids[i] = new User
				{
					Id = int.Parse(idStr),
					Name = name
				};
			}

			return ids;
		}
	}
}
