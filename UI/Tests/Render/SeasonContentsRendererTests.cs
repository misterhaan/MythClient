using System.Drawing;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;
using au.UI.CaptionedPictureBox;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.Applications.MythClient.UI.Render.Tests {
	[TestClass]
	public class SeasonContentsRendererTests : RendererTestsBase {
		[TestMethod]
		public void Render_IsSelected_Highlighted() {
			ScrollableControl target = GetRenderTargetControl();
			ContentsRenderer renderer = GetContentsRenderer(target);
			IEpisode episode = GetEpisode();

			renderer.Render(new IEpisode[] { episode }, episode);
			CaptionedPictureBox pictureBox = (CaptionedPictureBox)target.Controls[0];

			Assert.AreEqual(SystemColors.Highlight, pictureBox.BackColor, "Selected episode should have highlighted background.");
			Assert.AreEqual(SystemColors.HighlightText, pictureBox.ForeColor, "Selected episode should have highlighted text color.");
		}

		[TestMethod]
		public void Render_NotSelected_NotHighlighted() {
			ScrollableControl target = GetRenderTargetControl();
			ContentsRenderer renderer = GetContentsRenderer(target);
			IEpisode episode = GetEpisode();

			renderer.Render(new IEpisode[] { episode }, null);
			CaptionedPictureBox pictureBox = (CaptionedPictureBox)target.Controls[0];

			Assert.AreNotEqual(SystemColors.Highlight, pictureBox.BackColor, "Non-selected episode should not have highlighted background.");
			Assert.AreNotEqual(SystemColors.HighlightText, pictureBox.ForeColor, "Non-selected episode should not have highlighted text color.");
		}

		[TestMethod]
		public void Render_HasPreviewImageUrl_UsesUrl() {
			ScrollableControl target = GetRenderTargetControl();
			ContentsRenderer renderer = GetContentsRenderer(target);
			IEpisode episode = GetEpisode(PreviewImageUrl);

			renderer.Render(new IEpisode[] { episode }, null);
			CaptionedPictureBox pictureBox = (CaptionedPictureBox)target.Controls[0];

			Assert.AreEqual(PreviewImageUrl, pictureBox.ImageLocation, "Preview image should load from URL location when a preview image URL is provided.");
		}

		[DataTestMethod]
		[DataRow(null)]
		[DataRow("")]
		public void Render_NoPreviewImageUrl_ImageHasNoLocation(string previewImageUrl) {
			ScrollableControl target = GetRenderTargetControl();
			ContentsRenderer renderer = GetContentsRenderer(target);
			IEpisode episode = GetEpisode(previewImageUrl);

			renderer.Render(new IEpisode[] { episode }, null);
			CaptionedPictureBox pictureBox = (CaptionedPictureBox)target.Controls[0];

			Assert.IsNull(pictureBox.ImageLocation, "Preview image should not load from a URL when a preview image URL is not provided.");
		}

		private static IEpisode GetEpisode(string previewImageUrl = null) {
			IEpisode episode = A.Fake<IEpisode>();
			A.CallTo(() => episode.PreviewImageUrl).Returns(previewImageUrl);
			return episode;
		}
	}
}
