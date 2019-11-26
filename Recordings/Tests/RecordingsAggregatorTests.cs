using System;
using System.Collections.Generic;
using System.Linq;
using au.Applications.MythClient.Recordings.Types;
using au.Applications.MythClient.Settings.Types;
using au.IO.Web.API.MythTV.Types;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.Applications.MythClient.Recordings.Tests {
	[TestClass]
	public class RecordingsAggregatorTests {
		[TestMethod]
		public void OneShow_Deleted_EmptyShows() {
			RecordingsAggregator agg = GetAggregator(RecordingSortOption.Title);
			Program deleted = GetProgram("title", 1, "deleted test", 1, DateTime.Now);
			deleted.Recording.RecGroup = "Deleted";
			agg.Add(deleted);

			IReadOnlyList<IShow> shows = agg.Finalize();

			Assert.IsFalse(shows.Any(), "Deleted programs should be ignored.");
		}

		[TestMethod]
		public void TwoPrograms_DifferentTitle_TwoShows() {
			RecordingsAggregator agg = GetAggregator(RecordingSortOption.Title);
			agg.Add(GetProgram("title 1", 1, "episode 1", 1, DateTime.Now));
			agg.Add(GetProgram("title 2", 1, "episode 1", 1, DateTime.Now));

			IReadOnlyCollection<IShow> shows = agg.Finalize();

			Assert.AreEqual(2, shows.Count, "Programs with different titles should aggregate to different shows.");
		}

		[TestMethod]
		public void ThreePrograms_SameTitle_OneShow() {
			RecordingsAggregator agg = GetAggregator(RecordingSortOption.Title);
			agg.Add(GetProgram("title", 1, "episode 1", 1, DateTime.Now));
			agg.Add(GetProgram("title", 1, "episode 2", 2, DateTime.Now));
			agg.Add(GetProgram("Title", 1, "episode 3", 3, DateTime.Now));

			IReadOnlyCollection<IShow> shows = agg.Finalize();

			Assert.AreEqual(1, shows.Count, "Programs with same title should aggregate to the same show.");
		}

		[TestMethod]
		public void TwoPrograms_SameTitleDifferentSeason_OneShowTwoSeasons() {
			RecordingsAggregator agg = GetAggregator(RecordingSortOption.Title);
			agg.Add(GetProgram("title", 1, "s1e1", 1, DateTime.Now));
			agg.Add(GetProgram("title", 2, "s2e1", 1, DateTime.Now));

			IReadOnlyCollection<IShow> shows = agg.Finalize();

			Assert.AreEqual(2, shows.Single().Seasons.Count);
		}

		[TestMethod]
		public void TwoPrograms_SameTitle_SameSeason_OneShowOneSeason() {
			RecordingsAggregator agg = GetAggregator(RecordingSortOption.Title);
			agg.Add(GetProgram("title", 1, "s1e1", 1, DateTime.Now));
			agg.Add(GetProgram("title", 1, "s1e2", 2, DateTime.Now));

			IReadOnlyCollection<IShow> shows = agg.Finalize();

			Assert.AreEqual(1, shows.Single().Seasons.Count);
		}

		[TestMethod]
		public void TwoPrograms_SameTitleAndSeason_FirstCoverArt() {
			const string firstUrl = "first?URL";
			Program first = GetProgram("title", 1, "first episode", 1, DateTime.Now);
			first.Artwork.ArtworkInfos.Add(new ArtworkInfo { Type = "coverart", URL = firstUrl });
			Program second = GetProgram("title", 1, "second episode", 2, DateTime.Now);
			second.Artwork.ArtworkInfos.Add(new ArtworkInfo { Type = "coverart", URL = "second?URL" });
			RecordingsAggregator agg = GetAggregator(RecordingSortOption.Title);
			agg.Add(first);
			agg.Add(second);

			IReadOnlyList<IShow> shows = agg.Finalize();
			ISeason season = shows.Single().Seasons.Single();

			Assert.IsTrue(season.CoverArtUrl.EndsWith(firstUrl), $"{nameof(season.CoverArtUrl)} should be set to the first program that has cover art.");
		}

		[TestMethod]
		public void TwoShows_TitleSortSetting_SortByTitle() {
			RecordingsAggregator agg = GetAggregator(RecordingSortOption.Title);
			string title1 = "the first show";
			string title2 = "second show";
			agg.Add(GetProgram(title1, 1, "the first episode", 1, DateTime.Now));
			agg.Add(GetProgram(title2, 1, "second episode", 2, DateTime.Now));

			IReadOnlyCollection<IShow> shows = agg.Finalize();

			Assert.AreEqual(title1, shows.First().Title, "First title did not sort first.");
		}

		[TestMethod]
		public void TwoShows_OldestSortSetting_SortByFirstAired() {
			RecordingsAggregator agg = GetAggregator(RecordingSortOption.OldestRecorded);
			DateTime airdate1 = new DateTime(2001, 2, 3);
			DateTime airdate2 = new DateTime(2002, 4, 8);
			agg.Add(GetProgram("early show", 1, "episode", 1, airdate1));
			agg.Add(GetProgram("after show", 1, "episode", 1, airdate2));

			IReadOnlyCollection<IShow> shows = agg.Finalize();

			Assert.AreEqual(airdate1, shows.First().OldestEpisode.FirstAired, "Earlier air date did not sort first.");
		}

		private static RecordingsAggregator GetAggregator(RecordingSortOption sortOption)
			=> new RecordingsAggregator(GetSettings(sortOption), A.Fake<IContentApi>());

		private static IMythSettings GetSettings(RecordingSortOption sortOption) {
			IMythSettings settings = A.Fake<IMythSettings>();
			A.CallTo(() => settings.RecordingSortOption).Returns(sortOption);
			A.CallTo(() => settings.Server.Name).Returns("localhost");
			return settings;
		}

		private static Program GetProgram(string title, int season, string subtitle, int episode, DateTime firstAired) {
			return new Program {
				Title = title,
				Season = season,
				SubTitle = subtitle,
				Episode = episode,
				SerializedAirdate = firstAired.ToString(),
				Recording = GetRecording()
			};
		}

		private static RecordingInfo GetRecording() {
			return new RecordingInfo {
				RecGroup = "test"
			};
		}
	}
}
