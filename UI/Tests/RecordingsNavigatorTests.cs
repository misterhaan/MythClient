using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;
using au.Applications.MythClient.Settings.Types;
using au.Applications.MythClient.UI.Actions;
using au.Applications.MythClient.UI.Render;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.Applications.MythClient.UI.Tests {
	[TestClass]
	public class RecordingsNavigatorTests {
		//const int _showIndexMovie = 0;
		const int _showIndexMultiEpisode = 1;
		const int _showIndexMultiSeason = 2;

		[TestMethod]
		public void Depth_DefaultRecordings() {
			RecordingsNavigator navigator = GetNavigator();

			Assert.AreEqual(BrowsingDepth.Recordings, navigator.Depth, $"{nameof(navigator.Depth)} should default to {nameof(BrowsingDepth.Recordings)}.");
		}

		#region LocationName
		[TestMethod]
		public void LocationName_DefaultRecordings() {
			RecordingsNavigator navigator = GetNavigator();

			Assert.AreEqual(Titles.Recordings, navigator.LocationName, $"{nameof(navigator.LocationName)} should default to recordings name.");
		}

		[TestMethod]
		public void LocationName_ShowDepth_ShowTitle() {
			TestingRecordingsNavigator navigator = GetNavigatorAtShowDepth();

			Assert.AreEqual(navigator.Show.Title, navigator.LocationName, $"{nameof(navigator.LocationName)} should be the current show title at the show depth.");
		}

		[TestMethod]
		public void LocationName_SeasonDepth_ShowTitleAndSeasonNumber() {
			TestingRecordingsNavigator navigator = GetNavigatorAtSeasonDepth();

			Assert.AreEqual(string.Format(Titles.Season, navigator.Season.ShowTitle, navigator.Season.Number), navigator.LocationName, $"{nameof(navigator.LocationName)} should be the current show title and season number at the season depth.");
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException), nameof(RecordingsNavigator.BackDescription) + " should throw an " + nameof(InvalidOperationException) + " when the depth is unknown.")]
		public void LocationName_UnknownDepth_ThrowsException() {
			TestingRecordingsNavigator navigator = GetNavigator();
			navigator.SetDepth((BrowsingDepth)(-42));

			_ = navigator.LocationName;
		}
		#endregion LocationName

		#region BackDescription
		[TestMethod]
		public void BackDescription_DefaultEmpty() {
			RecordingsNavigator navigator = GetNavigator();

			Assert.AreEqual("", navigator.BackDescription, $"{nameof(navigator.BackDescription)} should default to nothing.");
		}

		[TestMethod]
		public void BackDescription_ShowDepth_BackToRecordings() {
			TestingRecordingsNavigator navigator = GetNavigatorAtShowDepth();

			Assert.AreEqual(string.Format(Titles.BackTo, Titles.Recordings), navigator.BackDescription, $"{nameof(navigator.BackDescription)} should say back to shows at the show depth.");
		}

		[TestMethod]
		public void BackDescription_SeasonDepthOneSeason_BackToRecordings() {
			TestingRecordingsNavigator navigator = GetNavigatorAtSeasonDepth(_showIndexMultiEpisode);

			Assert.AreEqual(string.Format(Titles.BackTo, Titles.Recordings), navigator.BackDescription, $"{nameof(navigator.BackDescription)} should say back to shows at the season depth for a show with only one season.");
		}

		[TestMethod]
		public void BackDescription_SeasonDepthTwoSeasons_BackToShow() {
			TestingRecordingsNavigator navigator = GetNavigatorAtSeasonDepth(_showIndexMultiSeason);

			Assert.AreEqual(string.Format(Titles.BackTo, navigator.Show.Title), navigator.BackDescription, $"{nameof(navigator.BackDescription)} should say back to the show title at the season depth.");
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException), nameof(RecordingsNavigator.BackDescription) + " should throw an " + nameof(InvalidOperationException) + " when the depth is unknown.")]
		public void BackDescription_UnknownDepth_ThrowsException() {
			TestingRecordingsNavigator navigator = GetNavigator();
			navigator.SetDepth((BrowsingDepth)(-42));

			_ = navigator.BackDescription;
		}
		#endregion BackDescription

		#region RefreshAsync
		[TestMethod]
		public async Task RefreshAsync_RecordingsDepth_DoesNotCallFindSeason() {
			TestingRecordingsNavigator navigator = GetNavigator();
			IShow foundShow = navigator.Recordings.Shows[1];
			navigator.Show = A.Fake<IShow>();
			A.CallTo(() => navigator.Show.Title).Returns(foundShow.Title);
			A.CallTo(() => navigator.Recordings.FindShow(A<IShow>.Ignored)).Returns(foundShow);

			await navigator.RefreshAsync().ConfigureAwait(false);

			Assert.AreEqual(foundShow, navigator.Show, $"{nameof(navigator.RefreshAsync)}() should change selected show to one that's in recordings.");
			A.CallTo(() => foundShow.FindSeason(A<ISeason>.Ignored)).MustNotHaveHappened();
		}

		[TestMethod]
		public async Task RefreshAsync_ShowDepth_NoMatchingShow_RecordingsDepth() {
			TestingRecordingsNavigator navigator = GetNavigatorAtShowDepth();
			navigator.Show = A.Fake<IShow>();
			A.CallTo(() => navigator.Show.Matches(A<IShow>.Ignored)).Returns(false);

			await navigator.RefreshAsync().ConfigureAwait(false);

			Assert.AreEqual(BrowsingDepth.Recordings, navigator.Depth, $"{nameof(navigator.RefreshAsync)}() should change set depth to recordings if the previously-selected show doesn't exist.");
		}

		[TestMethod]
		public async Task RefreshAsync_ShowDepth_DoesNotCallFindEpisode() {
			TestingRecordingsNavigator navigator = GetNavigatorAtShowDepth();
			A.CallTo(() => navigator.Recordings.FindShow(A<IShow>.Ignored)).Returns(navigator.Show);
			A.CallTo(() => navigator.Show.Matches(A<IShow>.Ignored)).Returns(true);
			ISeason foundSeason = navigator.Show.Seasons[0];
			navigator.Season = A.Fake<ISeason>();
			A.CallTo(() => navigator.Season.Matches(foundSeason)).Returns(true);
			A.CallTo(() => navigator.Show.FindSeason(A<ISeason>.Ignored)).Returns(foundSeason);

			await navigator.RefreshAsync().ConfigureAwait(false);

			A.CallTo(() => navigator.Season.FindEpisode(A<IEpisode>.Ignored)).MustNotHaveHappened();
		}

		[TestMethod]
		public async Task RefreshAsync_SeasonDepth_NoMatchingSeason_ShowDepth() {
			TestingRecordingsNavigator navigator = GetNavigatorAtSeasonDepth();
			A.CallTo(() => navigator.Recordings.FindShow(A<IShow>.Ignored)).Returns(navigator.Show);
			A.CallTo(() => navigator.Show.Matches(A<IShow>.Ignored)).Returns(true);
			A.CallTo(() => navigator.Show.FindSeason(A<ISeason>.Ignored)).Returns(A.Fake<ISeason>());
			A.CallTo(() => navigator.Season.Matches(A<ISeason>.Ignored)).Returns(false);

			await navigator.RefreshAsync().ConfigureAwait(false);

			Assert.AreEqual(BrowsingDepth.Show, navigator.Depth, $"{nameof(navigator.RefreshAsync)}() should change set depth to show if the previously-selected season doesn't exist.");
		}
		#endregion RefreshAsync

		#region Render
		[TestMethod]
		public void Render_RecordingsDepth_RendersShows() {
			TestingRecordingsNavigator navigator = GetNavigator();

			navigator.Render();

			A.CallTo(() => navigator.Contents.Render(A<IReadOnlyCollection<IShow>>.Ignored, A<IShow>.Ignored)).MustHaveHappenedOnceExactly();
			A.CallTo(() => navigator.Info.Render(A<IShow>.Ignored)).MustHaveHappenedOnceExactly();
		}

		[TestMethod]
		public void Render_ShowDepth_RendersSeasons() {
			TestingRecordingsNavigator navigator = GetNavigatorAtShowDepth();

			navigator.Render();

			A.CallTo(() => navigator.Contents.Render(A<IReadOnlyCollection<ISeason>>.Ignored, A<ISeason>.Ignored)).MustHaveHappenedOnceExactly();
			A.CallTo(() => navigator.Info.Render(A<ISeason>.Ignored)).MustHaveHappenedOnceExactly();
		}

		[TestMethod]
		public void Render_SeasonDepth_RendersEpisodes() {
			TestingRecordingsNavigator navigator = GetNavigatorAtSeasonDepth();

			navigator.Render();

			A.CallTo(() => navigator.Contents.Render(A<IReadOnlyCollection<IEpisode>>.Ignored, A<IEpisode>.Ignored)).MustHaveHappenedOnceExactly();
			A.CallTo(() => navigator.Info.Render(A<IEpisode>.Ignored)).MustHaveHappenedOnceExactly();
		}
		#endregion Render

		#region DeleteAsync
		[TestMethod]
		public async Task DeleteAsync_RecordingsDepth_DeletesSelectedShowOldestEpisode() {
			TestingRecordingsNavigator navigator = GetNavigator();
			navigator.Show = navigator.Recordings.Shows[_showIndexMultiEpisode];

			await navigator.DeleteAsync().ConfigureAwait(false);

			A.CallTo(() => navigator.Deleter.DeleteAsync(navigator.Show.OldestEpisode)).MustHaveHappenedOnceExactly();
		}

		[TestMethod]
		public async Task DeleteAsync_ShowDepth_DeletesSelectedSeasonOldestEpisode() {
			TestingRecordingsNavigator navigator = GetNavigatorAtShowDepth(_showIndexMultiSeason);
			navigator.Season = navigator.Show.Seasons[1];

			await navigator.DeleteAsync().ConfigureAwait(false);

			A.CallTo(() => navigator.Deleter.DeleteAsync(navigator.Season.OldestEpisode)).MustHaveHappenedOnceExactly();
		}

		[TestMethod]
		public async Task DeleteAsync_SeasonDepth_DeletesSelectedEpisode() {
			TestingRecordingsNavigator navigator = GetNavigatorAtSeasonDepth(_showIndexMultiEpisode);
			navigator.Episode = navigator.Season.Episodes[1];

			await navigator.DeleteAsync().ConfigureAwait(false);

			A.CallTo(() => navigator.Deleter.DeleteAsync(navigator.Episode)).MustHaveHappenedOnceExactly();
		}
		#endregion DeleteAsync

		private static TestingRecordingsNavigator GetNavigator()
			=> new TestingRecordingsNavigator();

		private static TestingRecordingsNavigator GetNavigatorAtShowDepth(int showIndex = _showIndexMultiSeason) {
			TestingRecordingsNavigator navigator = GetNavigator();
			navigator.SetDepth(BrowsingDepth.Show);
			navigator.Show = navigator.Recordings.Shows[showIndex];
			return navigator;
		}

		private static TestingRecordingsNavigator GetNavigatorAtSeasonDepth(int showIndex = _showIndexMultiEpisode, int seasonIndex = 0) {
			TestingRecordingsNavigator navigator = GetNavigator();
			navigator.SetDepth(BrowsingDepth.Season);
			navigator.Show = navigator.Recordings.Shows[showIndex];
			navigator.Season = navigator.Show.Seasons[seasonIndex];
			return navigator;
		}

		private class TestingRecordingsNavigator : RecordingsNavigator {
			public TestingRecordingsNavigator()
				: base(A.Fake<IWin32Window>(),
					A.Fake<IMythSettings>(),
					GetRecordings(),
					A.Fake<IContentsRenderer>(),
					A.Fake<IInfoRenderer>(),
					GetActorFactory()) { }

			public void SetDepth(BrowsingDepth depth) => Depth = depth;

			public IRecordings Recordings => _recordings;
			public IShow Show { get => _show; set => _show = value; }
			public ISeason Season { get => _season; set => _season = value; }
			public IEpisode Episode { get => _episode; set => _episode = value; }

			public IContentsRenderer Contents => _contents;
			public IInfoRenderer Info => _info;

			public IRecordingsDeleter Deleter => _deleter;

			private static IActorFactory GetActorFactory() {
				IActorFactory actorFactory = A.Fake<IActorFactory>();
				A.CallTo(() => actorFactory.BuildRecordingsDeleter(A<IWin32Window>.Ignored, A<IRecordings>.Ignored, A<RecordingsNavigator>.Ignored)).Returns(A.Fake<IRecordingsDeleter>());
				A.CallTo(() => actorFactory.BuildRecordingsExporter(A<IWin32Window>.Ignored, A<IMythSettings>.Ignored)).Returns(A.Fake<IRecordingsExporter>());
				return actorFactory;
			}

			#region Fake Recordings
			private static IRecordings GetRecordings() {
				IRecordings recordings = A.Fake<IRecordings>();
				A.CallTo(() => recordings.Shows).Returns(new IShow[]{
					GetMovie(),
					GetMultiEpisodeShow(),
					GetMultiSeasonShow()
				});
				return recordings;
			}

			private static IShow GetMovie() {
				IShow movie = A.Fake<IShow>();
				A.CallTo(() => movie.Title).Returns("The Best Movie");
				A.CallTo(() => movie.IsSeries).Returns(false);
				ISeason season = A.Fake<ISeason>();
				A.CallTo(() => movie.Seasons).Returns(new ISeason[] { season });
				IEpisode episode = A.Fake<IEpisode>();
				A.CallTo(() => movie.OldestEpisode).Returns(episode);
				A.CallTo(() => movie.NewestEpisode).Returns(episode);
				A.CallTo(() => season.Episodes).Returns(new IEpisode[] { episode });
				A.CallTo(() => season.OldestEpisode).Returns(episode);
				A.CallTo(() => season.NewestEpisode).Returns(episode);
				return movie;
			}

			private static IShow GetMultiEpisodeShow() {
				const string title = "Crime Pays";
				IShow show = A.Fake<IShow>();
				A.CallTo(() => show.Title).Returns(title);
				A.CallTo(() => show.IsSeries).Returns(true);
				ISeason season = A.Fake<ISeason>();
				A.CallTo(() => show.Seasons).Returns(new ISeason[] { season });
				A.CallTo(() => season.ShowTitle).Returns(title);
				A.CallTo(() => season.Number).Returns(3);
				IEpisode episode1 = A.Fake<IEpisode>();
				A.CallTo(() => episode1.SubTitle).Returns("Don't Run from the Po-lice!");
				A.CallTo(() => show.OldestEpisode).Returns(episode1);
				IEpisode episode2 = A.Fake<IEpisode>();
				A.CallTo(() => episode2.SubTitle).Returns("A Florida Man");
				A.CallTo(() => season.Episodes).Returns(new IEpisode[] { episode1, episode2 });
				return show;
			}

			private static IShow GetMultiSeasonShow() {
				const string title = "Whiny Babies";
				IShow show = A.Fake<IShow>();
				A.CallTo(() => show.Title).Returns(title);
				A.CallTo(() => show.IsSeries).Returns(true);
				ISeason season1 = A.Fake<ISeason>();
				A.CallTo(() => season1.ShowTitle).Returns(title);
				A.CallTo(() => season1.Number).Returns(1);
				ISeason season2 = A.Fake<ISeason>();
				A.CallTo(() => season2.ShowTitle).Returns(title);
				A.CallTo(() => season2.Number).Returns(2);
				A.CallTo(() => show.Seasons).Returns(new ISeason[] { season1, season2 });
				IEpisode s1episode = A.Fake<IEpisode>();
				A.CallTo(() => season1.Episodes).Returns(new IEpisode[] { s1episode });
				A.CallTo(() => season1.OldestEpisode).Returns(s1episode);
				IEpisode s2episode = A.Fake<IEpisode>();
				A.CallTo(() => season2.Episodes).Returns(new IEpisode[] { s2episode });
				A.CallTo(() => season2.OldestEpisode).Returns(s2episode);
				return show;
			}
			#endregion Fake Recordings
		}
	}
}
