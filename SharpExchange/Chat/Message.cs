using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using SharpExchange.Net;

namespace SharpExchange.Chat
{
	public class Message
	{
		public class Revision
		{
			public string Text { get; internal set; }

			public int AuthorId { get; internal set; }

			public string AuthorName { get; internal set; }

			// Good luck paring that.
			public string Timestamp { get; internal set; }
		}

		public class User
		{
			public int Id { get; internal set; }

			public string Username { get; set; }
		}

		private const string messageTextUrl = "https://{0}/message/{1}?plain=true";
		private readonly CookieManager cMan;

		public string Host { get; private set; }

		public int RoomId { get; private set; }

		public int Id { get; private set; }

		public int AuthorId { get; private set; }

		public string AuthorName { get; private set; }

		public string Text { get; private set; }

		public int Stars { get; private set; }

		public User[] PinnedBy { get; private set; }

		public MessageStates States { get; private set; }

		public ReadOnlyCollection<Revision> Revisions { get; private set; }



		public Message(string host, int messageId, IAuthenticationProvider auth = null)
		{
			if (auth != null)
			{
				cMan = auth[host];
			}

			Host = host.GetChatHost();
			Id = messageId;

			var result = GetTextWithStatusAsync(Host, Id, auth).Result;

			if (result.Status != HttpStatusCode.OK)
			{
				throw new Exception($"Unable to fetch message {Id}: {result.Status}.");
			}

			Text = result.Body;

			var endpoint = $"https://{Host}/messages/{Id}/history";
			var html = HttpRequest.GetAsync(endpoint, cMan).Result;
			var dom = new HtmlParser().ParseDocument(html);

			RoomId = GetRoomId(dom);
			Stars = GetStars(dom);
			PinnedBy = GetPinnedBy(dom);
			Revisions = GetHistory(dom);
			States = GetStates(dom); // Must be last, manipulates dom
			AuthorId = Revisions[0].AuthorId;
			AuthorName = Revisions[0].AuthorName;
		}



		public static bool operator ==(Message a, Message b)
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

		public static bool operator !=(Message a, Message b) => !(a == b);

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(obj, null)) return false;

			var m = obj as Message;

			if (ReferenceEquals(m, null)) return false;

			return GetHashCode() == m.GetHashCode();
		}

		public override int GetHashCode() => new { Host, Id }.GetHashCode();

		public override string ToString() => Text;

		public static async Task<bool> ExistsAsync(string host, int messageId, IAuthenticationProvider auth = null)
		{
			var result = await GetTextWithStatusAsync(host, messageId, auth);

			return result.Status == HttpStatusCode.OK;
		}

		public static async Task<string> GetTextAsync(string host, int messageId, IAuthenticationProvider auth = null)
		{
			var result = await GetTextWithStatusAsync(host, messageId, auth);

			return result.Status == HttpStatusCode.OK
				? result.Body
				: null;
		}

		public static Task<Message> GetAsync(string host, int messageId, IAuthenticationProvider auth = null)
		{
			return Task.Run(() => new Message(host, messageId, auth));
		}



		private static Task<GetWithStatusResult> GetTextWithStatusAsync(string host, int messageId, IAuthenticationProvider auth = null)
		{
			var url = string.Format(messageTextUrl, host.GetChatHost(), messageId);

			if (auth == null)
			{
				return HttpRequest.GetWithStatusAsync(url);
			}
			else
			{
				return HttpRequest.GetWithStatusAsync(url, auth[host]);
			}
		}

		private int GetRoomId(IHtmlDocument dom)
		{
			var link = dom.QuerySelector(".message a")?.Attributes["href"].Value;

			var idStr = link.Split('?')[0].Split('/')[2];

			return int.Parse(idStr);
		}

		private int GetStars(IHtmlDocument dom)
		{
			var hasStars = dom.QuerySelector(".stars") != null;

			if (!hasStars)
			{
				return 0;
			}

			var starCount = dom
				.QuerySelector(".times")
				?.TextContent;

			if (string.IsNullOrEmpty(starCount))
			{
				starCount = "1";
			}

			return int.Parse(starCount);
		}

		private User[] GetPinnedBy(IHtmlDocument dom)
		{
			var pinnerLinks = dom.QuerySelectorAll("#content p a");

			if ((pinnerLinks?.Length ?? 0) == 0)
			{
				return new User[0];
			}

			var users = new User[pinnerLinks.Length];

			for (var i = 0; i < pinnerLinks.Length; i++)
			{
				var idStr = pinnerLinks[i]
					.Attributes["href"]
					?.Value
					.Split('/')[2];
				var id = int.TryParse(idStr, out var x) ? x : int.MinValue;
				var name = pinnerLinks[i].TextContent;

				users[i] = new User
				{
					Id = id,
					Username = name
				};
			}

			return users;
		}

		private MessageStates GetStates(IHtmlDocument dom)
		{
			var isEdited = Revisions.Count > 1;
			var isStarred = Stars > 0;
			var isPinned = PinnedBy.Length > 0;
			var isDeleted = dom
				.QuerySelectorAll(".monologue b")
				.Any(x => x.TextContent == "deleted");

			var content = dom.QuerySelector("#content");
			var childCount = content.Children.Length;

			for (var i = 0; i < childCount; i++)
			{
				_ = content.RemoveChild(content.Children[0]);
			}

			var isPuregd = content.TextContent.Trim() == "(older data no longer available)";

			var states = isDeleted
				? MessageStates.Deleted
				: MessageStates.PubliclyVisible;

			if (isEdited) states = MessageStates.Edited;
			if (isStarred) states |= MessageStates.Starred;
			if (isPinned) states |= MessageStates.Pinned;
			if (isPuregd) states |= MessageStates.Purged;

			return states;
		}

		private ReadOnlyCollection<Revision> GetHistory(IHtmlDocument dom)
		{
			var monos = dom.QuerySelectorAll("#content h2:nth-of-type(2) ~ div");
			var revs = new List<Revision>();

			foreach (var mono in monos)
			{
				var messageText = mono.QuerySelector(".message-source")?.TextContent;

				if (string.IsNullOrEmpty(messageText))
				{
					continue;
				}

				var authorA = mono.QuerySelector("a");
				var authorName = authorA.TextContent;
				var authorIdStr = authorA.Attributes["href"].Value.Split('/')[2];
				var authorId = int.Parse(authorIdStr);
				var timestamp = mono.QuerySelector(".timestamp").TextContent;

				var r = new Revision
				{
					AuthorId = authorId,
					AuthorName = authorName,
					Text = messageText,
					Timestamp = timestamp
				};

				if (revs.Count == 0)
				{
					revs.Add(r);
				}
				else
				{
					revs.Insert(0, r);
				}
			}

			return new ReadOnlyCollection<Revision>(revs);
		}
	}
}
