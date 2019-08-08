using System;
using au.Applications.MythClient.Recordings.Types;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.Applications.MythClient.Recordings.Tests {
	[TestClass]
	public class SeasonTests {
		[TestMethod]
		public void OldestEpisode_OneEpisode_ReturnsEpisode() {
			Season season = GetSeason();
			IEpisode episode = GetEpisode(2001, 2, 3);
			season.Episodes = new IEpisode[] { episode };

			Assert.AreEqual(episode, season.OldestEpisode, $"{nameof(season.OldestEpisode)} should be the only {nameof(Episode)} when there is only one.");
		}

		[TestMethod]
		public void OldestEpisode_TwoEpisodes_ReturnsOldest() {
			Season season = GetSeason();
			IEpisode oldestEpisode = GetEpisode(2001, 2, 3);
			IEpisode newestEpisode = GetEpisode(2002, 3, 4);
			season.Episodes = new IEpisode[] { newestEpisode, oldestEpisode };

			Assert.AreEqual(oldestEpisode, season.OldestEpisode, $"{nameof(season.OldestEpisode)} should be the {nameof(Episode)} with the oldest {nameof(Episode.FirstAired)} date.");
		}

		[TestMethod]
		public void NewestEpisode_OneEpisode_ReturnsEpisode() {
			Season season = GetSeason();
			IEpisode episode = GetEpisode(2001, 2, 3);
			season.Episodes = new IEpisode[] { episode };

			Assert.AreEqual(episode, season.NewestEpisode, $"{nameof(season.NewestEpisode)} should be the only {nameof(Episode)} when there is only one.");
		}

		[TestMethod]
		public void NewestEpisode_TwoEpisodes_ReturnsNewest() {
			Season season = GetSeason();
			IEpisode oldestEpisode = GetEpisode(2001, 2, 3);
			IEpisode newestEpisode = GetEpisode(2002, 3, 4);
			season.Episodes = new IEpisode[] { newestEpisode, oldestEpisode };

			Assert.AreEqual(newestEpisode, season.NewestEpisode, $"{nameof(season.NewestEpisode)} should be the {nameof(Episode)} with the newest {nameof(Episode.FirstAired)} date.");
		}

		[TestMethod]
		public void Duration_OneEpisode_ReturnsEpisodeDuration() {
			Season season = GetSeason();
			IEpisode episode = GetEpisode(60);
			season.Episodes = new IEpisode[] { episode };

			Assert.AreEqual(episode.Duration, season.Duration, $"{nameof(season.Duration)} of a {nameof(Season)} with one {nameof(Episode)} should be the same as the {nameof(Episode)}'s {nameof(episode.Duration)}.");
		}

		[TestMethod]
		public void Duration_TwoEpisodes_ReturnsSumOfEpisodeDurations() {
			Season season = GetSeason();
			IEpisode episode1 = GetEpisode(60);
			IEpisode episode2 = GetEpisode(120);
			season.Episodes = new IEpisode[] { episode1, episode2 };

			Assert.AreEqual(episode1.Duration + episode2.Duration, season.Duration, $"{nameof(season.Duration)} of a {nameof(Season)} with multiple {nameof(Episode)}s should be the sum as the {nameof(Episode)}s' {nameof(episode1.Duration)}s.");
		}

		private static Season GetSeason() => new Season("test", 7);

		private static IEpisode GetEpisode(int year, int month, int day) {
			IEpisode episode = GetEpisode();
			A.CallTo(() => episode.FirstAired).Returns(new DateTime(year, month, day));
			return episode;
		}

		private static IEpisode GetEpisode(int minutesDuration) {
			IEpisode episode = GetEpisode();
			A.CallTo(() => episode.Duration).Returns(TimeSpan.FromMinutes(minutesDuration));
			return episode;
		}

		private static IEpisode GetEpisode()
			=> A.Fake<IEpisode>();
	}
}
