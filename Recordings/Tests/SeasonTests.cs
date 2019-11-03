using System;
using System.Linq;
using au.Applications.MythClient.Recordings.Types;
using au.IO.Web.API.MythTV.Types;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.Applications.MythClient.Recordings.Tests {
	[TestClass]
	public class SeasonTests {
		[TestMethod]
		public void OldestEpisode_OneEpisode_ReturnsEpisode() {
			Season season = GetSeason();
			IEpisode episode = GetEpisodeWithFirstAired(2001, 2, 3);
			season.Episodes = new IEpisode[] { episode };

			Assert.AreEqual(episode, season.OldestEpisode, $"{nameof(season.OldestEpisode)} should be the only {nameof(Episode)} when there is only one.");
		}

		[TestMethod]
		public void OldestEpisode_TwoEpisodes_ReturnsOldest() {
			Season season = GetSeason();
			IEpisode oldestEpisode = GetEpisodeWithFirstAired(2001, 2, 3);
			IEpisode newestEpisode = GetEpisodeWithFirstAired(2002, 3, 4);
			season.Episodes = new IEpisode[] { newestEpisode, oldestEpisode };

			Assert.AreEqual(oldestEpisode, season.OldestEpisode, $"{nameof(season.OldestEpisode)} should be the {nameof(Episode)} with the oldest {nameof(Episode.FirstAired)} date.");
		}

		[TestMethod]
		public void NewestEpisode_OneEpisode_ReturnsEpisode() {
			Season season = GetSeason();
			IEpisode episode = GetEpisodeWithFirstAired(2001, 2, 3);
			season.Episodes = new IEpisode[] { episode };

			Assert.AreEqual(episode, season.NewestEpisode, $"{nameof(season.NewestEpisode)} should be the only {nameof(Episode)} when there is only one.");
		}

		[TestMethod]
		public void NewestEpisode_TwoEpisodes_ReturnsNewest() {
			Season season = GetSeason();
			IEpisode oldestEpisode = GetEpisodeWithFirstAired(2001, 2, 3);
			IEpisode newestEpisode = GetEpisodeWithFirstAired(2002, 3, 4);
			season.Episodes = new IEpisode[] { newestEpisode, oldestEpisode };

			Assert.AreEqual(newestEpisode, season.NewestEpisode, $"{nameof(season.NewestEpisode)} should be the {nameof(Episode)} with the newest {nameof(Episode.FirstAired)} date.");
		}

		[TestMethod]
		public void Duration_OneEpisode_ReturnsEpisodeDuration() {
			Season season = GetSeason();
			IEpisode episode = GetEpisodeWithDuration(60);
			season.Episodes = new IEpisode[] { episode };

			Assert.AreEqual(episode.Duration, season.Duration, $"{nameof(season.Duration)} of a {nameof(Season)} with one {nameof(Episode)} should be the same as the {nameof(Episode)}'s {nameof(episode.Duration)}.");
		}

		[TestMethod]
		public void Duration_TwoEpisodes_ReturnsSumOfEpisodeDurations() {
			Season season = GetSeason();
			IEpisode episode1 = GetEpisodeWithDuration(60);
			IEpisode episode2 = GetEpisodeWithDuration(120);
			season.Episodes = new IEpisode[] { episode1, episode2 };

			Assert.AreEqual(episode1.Duration + episode2.Duration, season.Duration, $"{nameof(season.Duration)} of a {nameof(Season)} with multiple {nameof(Episode)}s should be the sum as the {nameof(Episode)}s' {nameof(episode1.Duration)}s.");
		}

		[TestMethod]
		public void Matches_NullOther_ReturnsFalse() {
			Season season = GetSeason();

			bool matches = season.Matches(null);

			Assert.IsFalse(matches, $"{nameof(season.Matches)}() should return false against null.");
		}

		[TestMethod]
		public void Matches_Self_ReturnsTrue() {
			Season season = GetSeason();

			bool matches = season.Matches(season);

			Assert.IsTrue(matches, $"{nameof(season.Matches)}() should return true against itself.");
		}

		[TestMethod]
		public void Matches_SameNumber_ReturnsTrue() {
			Season season = GetSeason();
			ISeason other = A.Fake<ISeason>();
			A.CallTo(() => other.Number).Returns(season.Number);

			bool matches = season.Matches(other);

			Assert.IsTrue(matches, $"{nameof(season.Matches)}() should return true against a season with the same number.");
		}

		[TestMethod]
		public void Matches_DifferentNumber_ReturnsFalse() {
			Season season = GetSeason();
			ISeason other = A.Fake<ISeason>();
			A.CallTo(() => other.Number).Returns(42);

			bool matches = season.Matches(other);

			Assert.IsFalse(matches, $"{nameof(season.Matches)}() should return false against a season with a different number.");
		}

		[TestMethod]
		public void FindEpisode_NullSearch_ReturnsNull() {
			Season season = GetSeasonWithEpisodes();

			IEpisode foundEpisode = season.FindEpisode(null);

			Assert.IsNull(foundEpisode, $"{nameof(season.FindEpisode)}() should return null when looking for a null episode.");
		}

		[TestMethod]
		public void FindEpisode_ContainsReference_ReturnsSame() {
			Season season = GetSeasonWithEpisodes();
			IEpisode searchEpisode = season.Episodes[1];

			IEpisode foundEpisode = season.FindEpisode(searchEpisode);

			Assert.AreEqual(searchEpisode, foundEpisode, $"{nameof(season.FindEpisode)}() should return the searched episode when its reference is in the {nameof(season.Episodes)} property.");
		}

		[TestMethod]
		public void FindEpisode_ContainsNumber_ReturnsContained() {
			Season season = GetSeasonWithEpisodes();
			IEpisode copy = season.Episodes[0];
			IEpisode searchEpisode = GetComparableEpisode(copy.SubTitle, copy.Number, copy.FirstAired);

			IEpisode foundEpisode = season.FindEpisode(searchEpisode);

			Assert.AreNotEqual(searchEpisode, foundEpisode, $"{nameof(season.FindEpisode)}() should not return an episode that isn't referenced in the {nameof(season.Episodes)} property.");
			Assert.AreEqual(season.Episodes[0], foundEpisode, $"{nameof(season.FindEpisode)}() should return a referenced episode thats number matches the searched episode.");
		}

		[TestMethod]
		public void FindEpisode_DoesNotContainNumber_ReturnsNext() {
			Season season = GetSeasonWithEpisodes();
			IEpisode searchEpisode = GetComparableEpisode("third", 3, new DateTime(2016, 7, 28));

			IEpisode foundEpisode = season.FindEpisode(searchEpisode);

			Assert.AreEqual(season.Episodes[1], foundEpisode, $"{nameof(season.FindEpisode)}() should return the first referenced episode that follows the searched episode.");
		}

		[TestMethod]
		public void FindEpisode_DoesNotContainNumber_ReturnsLast() {
			Season season = GetSeasonWithEpisodes();
			IEpisode searchEpisode = GetComparableEpisode("seventh", 7, new DateTime(2016, 9, 11));

			IEpisode foundEpisode = season.FindEpisode(searchEpisode);

			Assert.AreEqual(season.Episodes.Last(), foundEpisode, $"{nameof(season.FindEpisode)}() should return the last referenced episode when the searched episode would have been last.");
		}

		private static Season GetSeason() => new Season("test", 7);

		private static Season GetSeasonWithEpisodes()
			=> new Season("test", 7) {
				Episodes = new IEpisode[] {
					GetComparableEpisode("second", 2, new DateTime(2016,8,4)),
					GetComparableEpisode("fourth",4,new DateTime(2016,8,18)),
					GetComparableEpisode("fifth",5,new DateTime(2016,8,25))
				}
			};

		private static IEpisode GetEpisodeWithFirstAired(int year, int month, int day) {
			IEpisode episode = GetEpisode();
			A.CallTo(() => episode.FirstAired).Returns(new DateTime(year, month, day));
			return episode;
		}

		private static IEpisode GetEpisodeWithDuration(int minutesDuration) {
			IEpisode episode = GetEpisode();
			A.CallTo(() => episode.Duration).Returns(TimeSpan.FromMinutes(minutesDuration));
			return episode;
		}

		private static IEpisode GetEpisode()
			=> A.Fake<IEpisode>();

		private static IEpisode GetComparableEpisode(string title, int number, DateTime aired)
			=> new Episode("test", 7, 0, "filename", title, number, aired, null, null, RecStatusType.Recorded, "image");
	}
}
