using System;
using System.Linq;
using au.Applications.MythClient.Recordings.Types;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.Applications.MythClient.Recordings.Tests {
	[TestClass]
	public class ShowTests {
		[TestMethod]
		public void NumEpisodes_OneSeason_ReturnsEpisodeCount() {
			Show show = GetShow();
			show.Seasons = new ISeason[] { GetSeasonWithEpisodeCount(3) };

			Assert.AreEqual(3, show.NumEpisodes, $"{nameof(show.NumEpisodes)} should equal the {nameof(Episode)} count of the {nameof(Season)} when there is only one {nameof(Season)}.");
		}

		[TestMethod]
		public void NumEpisodes_TwoSeasons_ReturnsSumOfEpisodeCounts() {
			Show show = GetShow();
			ISeason season1 = GetSeasonWithEpisodeCount(3);
			ISeason season2 = GetSeasonWithEpisodeCount(7);
			show.Seasons = new ISeason[] { season1, season2 };

			Assert.AreEqual(3 + 7, show.NumEpisodes, $"{nameof(show.NumEpisodes)} should equal the sum of the {nameof(Episode)} count of all {nameof(Season)}s.");
		}

		[TestMethod]
		public void OldestEpisode_OneSeason_ReturnsSeasonOldest() {
			Show show = GetShow();
			IEpisode episode = GetEpisode(2001, 2, 3);
			show.Seasons = new ISeason[] { GetSeasonWithOldestEpisode(episode) };

			Assert.AreEqual(episode, show.OldestEpisode, $"{nameof(show.OldestEpisode)} should return the {nameof(Season)}'s {nameof(Season.OldestEpisode)} when there is only one {nameof(Season)}.");
		}

		[TestMethod]
		public void OldestEpisode_TwoSeasons_ReturnsOldestOfOldest() {
			Show show = GetShow();
			IEpisode oldestEpisode = GetEpisode(2001, 2, 3);
			IEpisode newestEpisode = GetEpisode(2002, 3, 4);
			show.Seasons = new ISeason[] { GetSeasonWithOldestEpisode(oldestEpisode), GetSeasonWithOldestEpisode(newestEpisode) };

			Assert.AreEqual(oldestEpisode, show.OldestEpisode, $"{nameof(show.OldestEpisode)} should return the {nameof(Season.OldestEpisode)} of the {nameof(Season)} with the oldest {nameof(Season.OldestEpisode)}.");
		}

		[TestMethod]
		public void NewestEpisode_OneSeason_ReturnsSeasonNewest() {
			Show show = GetShow();
			IEpisode episode = GetEpisode(2001, 2, 3);
			show.Seasons = new ISeason[] { GetSeasonWithNewestEpisode(episode) };

			Assert.AreEqual(episode, show.NewestEpisode, $"{nameof(show.NewestEpisode)} should return the {nameof(Season)}'s {nameof(Season.NewestEpisode)} when there is only one {nameof(Season)}.");
		}

		[TestMethod]
		public void NewestEpisode_TwoSeasons_ReturnsNewestOfNewest() {
			Show show = GetShow();
			IEpisode oldestEpisode = GetEpisode(2001, 2, 3);
			IEpisode newestEpisode = GetEpisode(2002, 3, 4);
			show.Seasons = new ISeason[] { GetSeasonWithNewestEpisode(oldestEpisode), GetSeasonWithNewestEpisode(newestEpisode) };

			Assert.AreEqual(newestEpisode, show.NewestEpisode, $"{nameof(show.NewestEpisode)} should return the {nameof(Season.NewestEpisode)} of the {nameof(Season)} with the newest {nameof(Season.NewestEpisode)}.");
		}

		[TestMethod]
		public void Duration_OneSeason_ReturnsSeasonDuration() {
			Show show = GetShow();
			ISeason season = GetSeasonWithDuration(60);
			show.Seasons = new ISeason[] { season };

			Assert.AreEqual(season.Duration, show.Duration, $"{nameof(show.Duration)} should return the {nameof(Season)}'s {nameof(Season.Duration)} when there is only one {nameof(Season)}.");
		}

		[TestMethod]
		public void Duration_TwoSeasons_ReturnsSumOfDurations() {
			Show show = GetShow();
			ISeason season1 = GetSeasonWithDuration(60);
			ISeason season2 = GetSeasonWithDuration(30);
			show.Seasons = new ISeason[] { season1, season2 };

			Assert.AreEqual(season1.Duration + season2.Duration, show.Duration, $"{nameof(show.Duration)} should return the sum of all {nameof(Season)}s' {nameof(Season.Duration)}s.");
		}

		[DataTestMethod]
		[DataRow(null)]
		[DataRow("")]
		public void CoverArtUrl_NoSeasonWithCoverArtUrl_ReturnsNull(string url) {
			Show show = GetShow();
			ISeason season = GetSeasonWithCoverArt(url);
			show.Seasons = new ISeason[] { season };

			Assert.IsNull(show.CoverArtUrl, $"{nameof(show.CoverArtUrl)} should be null if no {nameof(Season)}s have a {nameof(Season.CoverArtUrl)}.");
		}

		[TestMethod]
		public void CoverArtUrl_OneSeasonWithCoverArtUrl_ReturnsCoverArtUrl() {
			Show show = GetShow();
			ISeason season = GetSeasonWithCoverArt("fake url");
			show.Seasons = new ISeason[] { season };

			Assert.AreEqual(season.CoverArtUrl, show.CoverArtUrl, $"{nameof(show.CoverArtUrl)} should return the {nameof(Season.CoverArtUrl)} of the first {nameof(Season)} that has one.");
		}

		[TestMethod]
		public void CoverArtUrl_SecondSeasonHasCoverArtUrl_ReturnsCoverArtUrl() {
			Show show = GetShow();
			ISeason noCoverSeason = GetSeasonWithCoverArt(null);
			ISeason coverSeason = GetSeasonWithCoverArt("fake url");
			show.Seasons = new ISeason[] { noCoverSeason, coverSeason };

			Assert.AreEqual(coverSeason.CoverArtUrl, show.CoverArtUrl, $"{nameof(show.CoverArtUrl)} should return the {nameof(Season.CoverArtUrl)} of the first {nameof(Season)} that has one.");
		}

		[TestMethod]
		public void Matches_NullOther_ReturnsFalse() {
			Show show = GetShow();

			bool matches = show.Matches(null);

			Assert.IsFalse(matches, $"{nameof(show.Matches)}() should return false against null.");
		}

		[TestMethod]
		public void Matches_Self_ReturnsTrue() {
			Show show = GetShow();

			bool matches = show.Matches(show);

			Assert.IsTrue(matches, $"{nameof(show.Matches)}() should return true against itself.");
		}

		[TestMethod]
		public void Matches_SameTitle_ReturnsTrue() {
			Show show = GetShow();
			IShow other = A.Fake<IShow>();
			A.CallTo(() => other.Title).Returns(show.Title);

			bool matches = show.Matches(other);

			Assert.IsTrue(matches, $"{nameof(show.Matches)}() should return true against a show with the same title.");
		}

		[TestMethod]
		public void Matches_SameTitleDifferencCasing_ReturnsTrue() {
			Show show = GetShow();
			IShow other = A.Fake<IShow>();
			A.CallTo(() => other.Title).Returns(show.Title.ToUpper());

			bool matches = show.Matches(other);

			Assert.IsTrue(matches, $"{nameof(show.Matches)}() should return true against a show with the same title with different capitalization.");
		}

		[TestMethod]
		public void Matches_DifferentTitle_ReturnsFalse() {
			Show show = GetShow();
			IShow other = A.Fake<IShow>();
			A.CallTo(() => other.Title).Returns("Something Else");

			bool matches = show.Matches(other);

			Assert.IsFalse(matches, $"{nameof(show.Matches)}() should return false against a show with a different title.");
		}

		[TestMethod]
		public void FindSeason_NullSearch_ReturnsNull() {
			Show show = GetShowWithSeasons(3, 4, 5);

			ISeason foundSeason = show.FindSeason(null);

			Assert.IsNull(foundSeason, $"{nameof(show.FindSeason)}() should return null when looking for a null season.");
		}

		[TestMethod]
		public void FindSeason_ContainsReference_ReturnsSame() {
			Show show = GetShowWithSeasons(3, 4, 5);
			ISeason searchSeason = show.Seasons[1];

			ISeason foundSeason = show.FindSeason(searchSeason);

			Assert.AreEqual(searchSeason, foundSeason, $"{nameof(show.FindSeason)}() should return the searched season when its reference is in the {nameof(show.Seasons)} property.");
		}

		[TestMethod]
		public void FindSeason_ContainsNumber_ReturnsContained() {
			Show show = GetShowWithSeasons(3, 4, 5);
			ISeason searchSeason = GetSeasonWithNumber(show.Seasons[0].Number);

			ISeason foundSeason = show.FindSeason(searchSeason);

			Assert.AreNotEqual(searchSeason, foundSeason, $"{nameof(show.FindSeason)}() should not return a season that isn't referenced in the {nameof(show.Seasons)} property.");
			Assert.AreEqual(show.Seasons[0], foundSeason, $"{nameof(show.FindSeason)}() should return a referenced season thats number matches the searched season.");
		}

		[TestMethod]
		public void FindSeason_DoesNotContainNumber_ReturnsNext() {
			Show show = GetShowWithSeasons(2, 4, 5);
			ISeason searchSeason = GetSeasonWithNumber(3);

			ISeason foundSeason = show.FindSeason(searchSeason);

			Assert.AreEqual(show.Seasons[1], foundSeason, $"{nameof(show.FindSeason)}() should return the first referenced season that follows the searched season.");
		}

		[TestMethod]
		public void FindSeason_DoesNotContainNumber_ReturnsLast() {
			Show show = GetShowWithSeasons(3, 4, 5);
			ISeason searchSeason = GetSeasonWithNumber(7);

			ISeason foundSeason = show.FindSeason(searchSeason);

			Assert.AreEqual(show.Seasons.Last(), foundSeason, $"{nameof(show.FindSeason)}() should return the last referenced season when the searched season would have been last.");
		}

		private static Show GetShow() => new Show("test", "testing", "series");

		private static Show GetShowWithSeasons(params int[] numbers) {
			Show show = GetShow();
			show.Seasons = numbers.Select(number => GetSeasonWithNumber(number)).ToList();
			return show;
		}

		private static ISeason GetSeasonWithEpisodeCount(int numEpisodes) {
			ISeason season = GetSeason();
			A.CallTo(() => season.Episodes.Count).Returns(numEpisodes);
			return season;
		}

		private static ISeason GetSeasonWithOldestEpisode(IEpisode oldestEpisode) {
			ISeason season = GetSeason();
			A.CallTo(() => season.OldestEpisode).Returns(oldestEpisode);
			return season;
		}

		private static ISeason GetSeasonWithNewestEpisode(IEpisode newestEpisode) {
			ISeason season = GetSeason();
			A.CallTo(() => season.NewestEpisode).Returns(newestEpisode);
			return season;
		}

		private static ISeason GetSeasonWithDuration(int minutes) {
			ISeason season = GetSeason();
			A.CallTo(() => season.Duration).Returns(TimeSpan.FromMinutes(minutes));
			return season;
		}

		private static ISeason GetSeasonWithCoverArt(string url) {
			ISeason season = GetSeason();
			A.CallTo(() => season.CoverArtUrl).Returns(url);
			return season;
		}

		private static ISeason GetSeasonWithNumber(int number)
			=> new Season("showTitle", number);

		private static ISeason GetSeason() => A.Fake<ISeason>();

		private static IEpisode GetEpisode(int year, int month, int day) {
			IEpisode episode = GetEpisode();
			A.CallTo(() => episode.FirstAired).Returns(new DateTime(year, month, day));
			return episode;
		}

		private static IEpisode GetEpisode() => A.Fake<IEpisode>();
	}
}
