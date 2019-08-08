using System;
using System.Collections.Generic;
using au.Applications.MythClient.Recordings.Types;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.Applications.MythClient.Recordings.Tests {
	[TestClass]
	public class ShowComparerTests {
		[TestMethod]
		public void Title_SortsCorrectly() {
			IShow show1 = GetShow("the awesomest show");
			IShow show2 = GetShow("An Enchanted Evening");
			IShow show3 = GetShow("fantastic");
			IShow show4 = GetShow("A Little More");
			List<IShow> shows = new List<IShow> { show3, show1, show4, show2 };

			shows.Sort(ShowComparer.Title);

			Assert.AreEqual(show1, shows[0], "Prefix 'the' was not ignored.");
			Assert.AreEqual(show2, shows[1], "Prefix 'An' was not ignored.");
			Assert.AreEqual(show3, shows[2], "Some prefixes were not ignored.");
			Assert.AreEqual(show4, shows[3], "Prefix 'A' was not ignored.");
		}

		[TestMethod]
		public void OldestRecorded_SortsCorrectly() {
			IShow show1 = GetShow(2001, 2, 3);
			IShow show2 = GetShow(2002, 3, 4);
			IShow show3 = GetShow(2002, 4, 8);
			IShow show4 = GetShow(2002, 4, 16);
			List<IShow> shows = new List<IShow> { show2, show4, show1, show3 };

			shows.Sort(ShowComparer.OldestRecorded);

			Assert.AreEqual(show1, shows[0], "Earliest year didn't sort to first.");
			Assert.AreEqual(show2, shows[1], "Earlier month didn't sort earlier.");
			Assert.AreEqual(show3, shows[2], "Earlier day didn't sort earlier.");
			Assert.AreEqual(show4, shows[3], "Last date didn't sort last.");
		}

		private static IShow GetShow(string title) {
			IShow show = GetShow();
			A.CallTo(() => show.Title).Returns(title);
			return show;
		}

		private static IShow GetShow(int year, int month, int day) {
			IShow show = GetShow();
			IEpisode episode = A.Fake<IEpisode>();
			A.CallTo(() => episode.FirstAired).Returns(new DateTime(year, month, day));
			A.CallTo(() => show.OldestEpisode).Returns(episode);
			return show;
		}

		private static IShow GetShow() => A.Fake<IShow>();
	}
}
