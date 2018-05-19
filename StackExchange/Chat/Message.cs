using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using StackExchange.Net;

namespace StackExchange.Chat
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

		public ReadOnlyCollection<Revision> Revisions { get; private set; }



		public Message(string host, int messageId, IAuthenticationProvider auth = null)
		{
			if (auth != null)
			{
				cMan = auth[host];
			}

			Host = host.GetChatHost();
			Id = messageId;

			Text = GetTextWithStatus(Host, Id, cMan, out var status);

			if (status != HttpStatusCode.OK)
			{
				throw new Exception($"Unable to fetch message {Id}: {status}.");
			}

			var endpoint = $"https://{Host}/messages/{Id}/history";
			var html = HttpRequest.Get(endpoint, cMan);
			var dom = new HtmlParser().Parse(html);

			RoomId = GetRoomId(dom);
			Stars = GetStars(dom);
			PinnedBy = GetPinnedBy(dom);
			Revisions = GetHistory(dom);
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

		public static bool Exists(string host, int messageId, CookieManager cookieManager = null)
		{
			GetTextWithStatus(host, messageId, cookieManager, out var status);

			return status == HttpStatusCode.OK;
		}

		public static string GetText(string host, int messageId, CookieManager cookieManager = null)
		{
			return GetTextWithStatus(host, messageId, cookieManager, out var status);
		}



		private static string GetTextWithStatus(string host, int messageId, CookieManager cookieManager, out HttpStatusCode status)
		{
			var endpoint = string.Format(messageTextUrl, host.GetChatHost(), messageId);
			var result = HttpRequest.GetWithStatus(endpoint, cookieManager);

			status = result.Status;

			return result.Status == HttpStatusCode.OK ? result.Body : null;
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
