using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.Applications.MythClient.UI.Render.Tests {
	[TestClass]
	public class ShowInfoRendererTests : RendererTestsBase {
		[TestMethod]
		public void Render_HasCategory_AddsCategoryControl() {
			const string category = "Action";
			ScrollableControl target = GetRenderTargetControl();
			InfoRenderer renderer = GetInfoRenderer(target);
			IShow show = GetShow(category);

			renderer.Render(show);

			Assert.AreEqual(category, target.Controls[1].Text, "Show information should have a control showing the category if a category is present.");
		}

		[TestMethod]
		public void Render_HasOneEpisode_AddsOneEpisodeControl() {
			ScrollableControl target = GetRenderTargetControl();
			InfoRenderer renderer = GetInfoRenderer(target);
			IShow show = GetShow(null, 1);

			renderer.Render(show);

			Assert.AreEqual("1 episode", target.Controls[1].Text, "Show information should have a control saying there's one episode when there's one episode.");
		}

		[DataTestMethod]
		[DataRow(3)]
		[DataRow(7)]
		public void Render_HasMultipleEpisodes_AddsEpisodeCountControl(int numEpisodes) {
			ScrollableControl target = GetRenderTargetControl();
			InfoRenderer renderer = GetInfoRenderer(target);
			IShow show = GetShow(null, numEpisodes);

			renderer.Render(show);

			Assert.AreEqual($"{numEpisodes} episodes", target.Controls[1].Text, "Show information should have a control saying how many episodes there are when there's more than one episode.");
		}

		[DataTestMethod]
		[DataRow(3)]
		[DataRow(7)]
		public void Render_HasMultipleSeasons_AddsSeasonCountControl(int numSeasons) {
			ScrollableControl target = GetRenderTargetControl();
			InfoRenderer renderer = GetInfoRenderer(target);
			IShow show = GetShow(null, 0, numSeasons);

			renderer.Render(show);

			Assert.AreEqual($"{numSeasons} seasons", target.Controls[2].Text, "Show information should have a control saying how many seasons there are when there's more than one season.");
		}

		[TestMethod]
		public void Render_HasOneSeasonAndEpisodeNumbers_AddsOneSeasonControl() {
			ScrollableControl target = GetRenderTargetControl();
			InfoRenderer renderer = GetInfoRenderer(target);
			IShow show = GetShow(null, 0, 1, 1);

			renderer.Render(show);

			Assert.AreEqual("1 season", target.Controls[2].Text, "Show information should have a control saying there's one season when there's one season and episodes have numbers.");
		}

		private static IShow GetShow(string category, int numEpisodes = 0, int numSeasons = 0, int oldestEpisodeNum = 0) {
			IShow show = A.Fake<IShow>();
			A.CallTo(() => show.IsSeries).Returns(true);
			A.CallTo(() => show.Category).Returns(category);
			A.CallTo(() => show.NumEpisodes).Returns(numEpisodes);
			A.CallTo(() => show.Seasons).Returns(new ISeason[numSeasons]);
			A.CallTo(() => show.OldestEpisode.Number).Returns(oldestEpisodeNum);
			return show;
		}
	}
}
