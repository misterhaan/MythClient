using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.Applications.MythClient.UI.Render.Tests {
	[TestClass]
	public class MovieInfoRendererTests : RendererTestsBase {
		[TestMethod]
		public void Render_HasCategory_AddsNineControls() {
			ScrollableControl target = GetRenderTargetControl();
			InfoRenderer renderer = GetInfoRenderer(target);
			IShow movie = GetMovie("Action");

			renderer.Render(movie);

			Assert.AreEqual(9, target.Controls.Count, "Movie information should have 9 controls when a category is present.");
		}

		[DataTestMethod]
		[DataRow(null)]
		[DataRow("")]
		public void Render_NoCategory_AddsEightControls(string category) {
			ScrollableControl target = GetRenderTargetControl();
			InfoRenderer renderer = GetInfoRenderer(target);
			IShow movie = GetMovie(category);

			renderer.Render(movie);

			Assert.AreEqual(8, target.Controls.Count, "Movie information should have 8 controls when category is not specified.");
		}

		[TestMethod]
		public void Render_HasPreviewImageUrl_ThumbnailUsesUrl() {
			const string previewImageUrl = "https://www.track7.org/images/track7.png";
			ScrollableControl target = GetRenderTargetControl();
			InfoRenderer renderer = GetInfoRenderer(target);
			IShow movie = GetMovie(null, previewImageUrl);

			renderer.Render(movie);
			PictureBox pictureBox = (PictureBox)target.Controls[2];

			Assert.AreEqual(previewImageUrl, pictureBox.ImageLocation, "Movie thumbnail should load from URL location when a preview URL is provided.");
		}

		[DataTestMethod]
		[DataRow(null)]
		[DataRow("")]
		public void Render_NoPreviewImageUrl_ThumbnailHasNoLocation(string previewImageUrl) {
			ScrollableControl target = GetRenderTargetControl();
			InfoRenderer renderer = GetInfoRenderer(target);
			IShow movie = GetMovie(null, previewImageUrl);

			renderer.Render(movie);
			PictureBox pictureBox = (PictureBox)target.Controls[2];

			Assert.IsNull(pictureBox.ImageLocation, "Movie thumbnail should not load from a URL when no preview URL is provided.");
		}

		private static IShow GetMovie(string category, string previewImageUrl = null) {
			IShow movie = A.Fake<IShow>();
			A.CallTo(() => movie.IsSeries).Returns(false);
			A.CallTo(() => movie.Category).Returns(category);
			A.CallTo(() => movie.NewestEpisode.PreviewImageUrl).Returns(previewImageUrl);

			return movie;
		}
	}
}
