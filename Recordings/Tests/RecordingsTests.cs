using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using au.Applications.MythClient.Recordings.Types;
using au.Applications.MythClient.Settings.Types;
using au.IO.Web.API.MythTV.Types;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.Applications.MythClient.Recordings.Tests {
	[TestClass]
	public class RecordingsTests {
		[TestMethod]
		public async Task LoadAsync_CallsAggregatorFunctions() {
			Recordings recordings = GetRecordings();
			Recordings._createAggregator = GetAggregator;
			IDvrApi dvrApi = A.Fake<IDvrApi>();
			ProgramList programList = GetProgramList();
			A.CallTo(() => dvrApi.GetRecordedList()).Returns(programList);
			IContentApi contentApi = A.Fake<IContentApi>();
			A.CallTo(() => contentApi.GetPreviewImageUrl(A<uint>.Ignored)).Returns(new Uri("http://example.com/fakepreview.jpg"));
			A.CallTo(() => TestRecordings.ApiFactory.BuildDvrApi(A<bool>.Ignored, A<string>.Ignored, A<ushort>.Ignored)).Returns(dvrApi);
			A.CallTo(() => TestRecordings.ApiFactory.BuildContentApi(A<bool>.Ignored, A<string>.Ignored, A<ushort>.Ignored)).Returns(contentApi);

			await recordings.LoadAsync().ConfigureAwait(false);

			A.CallTo(() => Aggregator.Add(A<Program>.Ignored)).MustHaveHappened(programList.Programs.Count, Times.Exactly);
			A.CallTo(() => Aggregator.Finalize()).MustHaveHappened(1, Times.Exactly);
		}

		[TestMethod]
		public void FindShow_NullShow_ReturnsNull() {
			Recordings recordings = GetRecordings();

			IShow foundShow = recordings.FindShow(null);

			Assert.IsNull(foundShow, $"{nameof(recordings.FindShow)}() should return null when looking for a null show.");
		}

		[TestMethod]
		public void FindShow_ContainsReference_ReturnsSame() {
			Recordings recordings = GetRecordings();
			IShow searchShow = recordings.Shows[2];

			IShow foundShow = recordings.FindShow(searchShow);

			Assert.AreEqual(searchShow, foundShow, $"{nameof(recordings.FindShow)}() should return the searched show when its reference is in the {nameof(recordings.Shows)} property.");
		}

		[TestMethod]
		public void FindShow_ContainsTitle_ReturnsContained() {
			Recordings recordings = GetRecordings();
			IShow searchShow = A.Fake<IShow>();
			A.CallTo(() => searchShow.Title).Returns(recordings.Shows[1].Title);

			IShow foundShow = recordings.FindShow(searchShow);

			Assert.AreNotEqual(searchShow, foundShow, $"{nameof(recordings.FindShow)}() should not return a show that isn't referenced in the {nameof(recordings.Shows)} property.");
			Assert.AreEqual(recordings.Shows[1], foundShow, $"{nameof(recordings.FindShow)}() should return a referenced show thats title matches the searched show.");
		}

		[TestMethod]
		public void FindShow_DoesNotContainTitle_ReturnsNext() {
			Recordings recordings = GetRecordings();
			IShow searchShow = A.Fake<IShow>();
			A.CallTo(() => searchShow.Title).Returns("Cat Antics");

			IShow foundShow = recordings.FindShow(searchShow);

			Assert.AreEqual(recordings.Shows[1], foundShow, $"{nameof(recordings.FindShow)}() should return the first referenced show that follows the searched show.");
		}

		[TestMethod]
		public void FindShow_DoesNotContainTitleSortByRecorded_ReturnsNext() {
			Recordings recordings = GetRecordingsSortByRecorded();
			IShow searchShow = A.Fake<IShow>();
			A.CallTo(() => searchShow.Title).Returns("Cat Antics");
			A.CallTo(() => searchShow.OldestEpisode.FirstAired).Returns(new DateTime(2001, 6, 15));

			IShow foundShow = recordings.FindShow(searchShow);

			Assert.AreEqual(recordings.Shows[1], foundShow, $"{nameof(recordings.FindShow)}() should return the first referenced show that follows the searched show.");
		}

		[TestMethod]
		public void FindShow_DoesNotContainTitle_ReturnsLast() {
			Recordings recordings = GetRecordings();
			IShow searchShow = A.Fake<IShow>();
			A.CallTo(() => searchShow.Title).Returns("Zoro Returns");

			IShow foundShow = recordings.FindShow(searchShow);

			Assert.AreEqual(recordings.Shows.Last(), foundShow, $"{nameof(recordings.FindShow)}() should return the last referenced show when the searched show would have been last.");
		}

		[DataTestMethod]
		[DataRow(true)]
		[DataRow(false)]
		public async Task DeleteAsync_CallsAPI(bool rerecord) {
			TestRecordings recordings = GetRecordings();
			IDvrApi dvrApi = A.Fake<IDvrApi>();
			recordings.SetDvrApi(dvrApi);
			IEpisode episode = A.Fake<IEpisode>();
			A.CallTo(() => episode.ID).Returns((uint)42);

			await recordings.DeleteAsync(episode, rerecord).ConfigureAwait(false);

			A.CallTo(() => dvrApi.DeleteRecording(episode.ID, rerecord)).MustHaveHappenedOnceExactly();
		}

		private static TestRecordings GetRecordings()
			=> new TestRecordings(RecordingSortOption.Title, GetShows());

		private static Recordings GetRecordingsSortByRecorded()
			=> new TestRecordings(RecordingSortOption.OldestRecorded, GetShows());

		private static IShow[] GetShows()
			=> new IShow[] {
				GetMovie(),
				GetMultiEpisodeShow(),
				GetMultiSeasonShow()
			};

		private static IShow GetMovie() {
			Show movie = new Show("The Best Movie", "category", "movie");
			ISeason season = A.Fake<ISeason>();
			movie.Seasons = new ISeason[] { season };
			IEpisode episode = A.Fake<IEpisode>();
			A.CallTo(() => episode.FirstAired).Returns(new DateTime(2001, 2, 3));
			A.CallTo(() => season.Episodes).Returns(new IEpisode[] { episode });
			A.CallTo(() => season.OldestEpisode).Returns(episode);
			A.CallTo(() => season.NewestEpisode).Returns(episode);
			return movie;
		}

		private static IShow GetMultiEpisodeShow() {
			const string title = "Crime Pays";
			Show show = new Show(title, "category", "series");
			ISeason season = A.Fake<ISeason>();
			show.Seasons = new ISeason[] { season };
			A.CallTo(() => season.ShowTitle).Returns(title);
			A.CallTo(() => season.Number).Returns(3);
			IEpisode episode1 = A.Fake<IEpisode>();
			A.CallTo(() => episode1.SubTitle).Returns("Don't Run from the Po-lice!");
			A.CallTo(() => episode1.FirstAired).Returns(new DateTime(2002, 4, 8));
			IEpisode episode2 = A.Fake<IEpisode>();
			A.CallTo(() => episode2.SubTitle).Returns("A Florida Man");
			A.CallTo(() => episode2.FirstAired).Returns(new DateTime(2002, 4, 16));
			A.CallTo(() => season.Episodes).Returns(new IEpisode[] { episode1, episode2 });
			A.CallTo(() => season.OldestEpisode).Returns(episode1);
			return show;
		}

		private static IShow GetMultiSeasonShow() {
			const string title = "Whiny Babies";
			Show show = new Show(title, "category", "series");
			ISeason season1 = A.Fake<ISeason>();
			A.CallTo(() => season1.ShowTitle).Returns(title);
			A.CallTo(() => season1.Number).Returns(1);
			ISeason season2 = A.Fake<ISeason>();
			A.CallTo(() => season2.ShowTitle).Returns(title);
			A.CallTo(() => season2.Number).Returns(2);
			show.Seasons = new ISeason[] { season1, season2 };
			IEpisode s1episode = A.Fake<IEpisode>();
			A.CallTo(() => s1episode.FirstAired).Returns(new DateTime(2003, 6, 9));
			A.CallTo(() => season1.Episodes).Returns(new IEpisode[] { s1episode });
			A.CallTo(() => season1.OldestEpisode).Returns(s1episode);
			IEpisode s2episode = A.Fake<IEpisode>();
			A.CallTo(() => s2episode.FirstAired).Returns(new DateTime(2004, 8, 16));
			A.CallTo(() => season2.Episodes).Returns(new IEpisode[] { s2episode });
			A.CallTo(() => season2.OldestEpisode).Returns(s2episode);
			return show;
		}

		private ProgramList GetProgramList()
			=> new ProgramList {
				Programs = new List<Program> {
					new Program {
						Title="CSI",
						SerializedAirdate=new DateTime(2001,2,3).ToString(),
						Season=3,
						Episode=7,
						SubTitle="Duct Tape Murder"
					}
				}
			};

		private RecordingsAggregator GetAggregator(IMythSettings settings, IContentApi contentApi)
			=> Aggregator;

		private static readonly RecordingsAggregator Aggregator = A.Fake<RecordingsAggregator>(options => options.WithArgumentsForConstructor(() => new RecordingsAggregator(null, null)));

		private class TestRecordings : Recordings {
			public TestRecordings(RecordingSortOption sortOption, IShow[] shows) : base(GetSettings(sortOption), ApiFactory) {
				Shows = shows;
			}

			public static IApiFactory ApiFactory = A.Fake<IApiFactory>();

			public void SetDvrApi(IDvrApi dvrApi) => _dvrApi = dvrApi;

			private static IMythSettings GetSettings(RecordingSortOption sortOption) {
				IMythSettings settings = A.Fake<IMythSettings>();
				A.CallTo(() => settings.RecordingSortOption).Returns(sortOption);
				return settings;
			}
		}
	}
}
