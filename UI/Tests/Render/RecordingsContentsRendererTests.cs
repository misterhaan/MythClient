using System.Drawing;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.Applications.MythClient.UI.Render.Tests {
	[TestClass]
	public class RecordingsContentsRendererTests : RendererTestsBase {
		[TestMethod]
		public void Render_IsSelected_Highlighted() {
			ScrollableControl target = GetRenderTargetControl();
			ContentsRenderer renderer = GetContentsRenderer(target);
			IShow show = GetShow();

			renderer.Render(new IShow[] { show }, show);

			Assert.AreEqual(SystemColors.Highlight, target.Controls[0].BackColor, "Selected show should have highlighted background.");
		}

		[TestMethod]
		public void Render_NotSelected_NotHighlighted() {
			ScrollableControl target = GetRenderTargetControl();
			ContentsRenderer renderer = GetContentsRenderer(target);
			IShow show = GetShow();

			renderer.Render(new IShow[] { show }, null);

			Assert.AreNotEqual(SystemColors.Highlight, target.Controls[0].BackColor, "Non-selected show should not have highlighted background.");
		}

		[TestMethod]
		public void Render_HasCoverArtUrl_UsesUrl() {
			ScrollableControl target = GetRenderTargetControl();
			ContentsRenderer renderer = GetContentsRenderer(target);
			IShow show = GetShow(PreviewImageUrl);

			renderer.Render(new IShow[] { show }, null);
			PictureBox pictureBox = (PictureBox)target.Controls[0];

			Assert.AreEqual(PreviewImageUrl, pictureBox.ImageLocation, "Cover art should load from URL location when a cover art URL is provided.");
		}

		[DataTestMethod]
		[DataRow(null)]
		[DataRow("")]
		public void Render_NoCoverArtUrl_ImageHasNoLocation(string coverArtUrl) {
			ScrollableControl target = GetRenderTargetControl();
			ContentsRenderer renderer = GetContentsRenderer(target);
			IShow show = GetShow(coverArtUrl);

			renderer.Render(new IShow[] { show }, null);
			PictureBox pictureBox = (PictureBox)target.Controls[0];

			Assert.IsNull(pictureBox.ImageLocation, "Cover art should not load from a URL when a cover art URL is not provided.");
		}

		private static IShow GetShow(string coverArtUrl = null) {
			IShow show = A.Fake<IShow>();
			A.CallTo(() => show.CoverArtUrl).Returns(coverArtUrl);
			return show;
		}
	}
}
