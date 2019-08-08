using System.Drawing;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.Applications.MythClient.UI.Render.Tests {
	[TestClass]
	public class ShowContentsRendererTests : RendererTestsBase {
		[TestMethod]
		public void Render_IsSelected_Highlighted() {
			ScrollableControl target = GetRenderTargetControl();
			ContentsRenderer renderer = GetContentsRenderer(target);
			ISeason season = GetSeason();

			renderer.Render(new ISeason[] { season }, season);

			Assert.AreEqual(SystemColors.Highlight, target.Controls[0].BackColor, "Selected season should have highlighted background.");
		}

		[TestMethod]
		public void Render_NotSelected_NotHighlighted() {
			ScrollableControl target = GetRenderTargetControl();
			ContentsRenderer renderer = GetContentsRenderer(target);
			ISeason season = GetSeason();

			renderer.Render(new ISeason[] { season }, null);

			Assert.AreNotEqual(SystemColors.Highlight, target.Controls[0].BackColor, "Non-selected season should not have highlighted background.");
		}

		[TestMethod]
		public void Render_HasCoverArtUrl_UsesUrl() {
			ScrollableControl target = GetRenderTargetControl();
			ContentsRenderer renderer = GetContentsRenderer(target);
			ISeason season = GetSeason(PreviewImageUrl);

			renderer.Render(new ISeason[] { season }, null);
			PictureBox pictureBox = (PictureBox)target.Controls[0];

			Assert.AreEqual(PreviewImageUrl, pictureBox.ImageLocation, "Cover art should load from URL location when a cover art URL is provided.");
		}

		[DataTestMethod]
		[DataRow(null)]
		[DataRow("")]
		public void Render_NoCoverArtUrl_ImageHasNoLocation(string coverArtUrl) {
			ScrollableControl target = GetRenderTargetControl();
			ContentsRenderer renderer = GetContentsRenderer(target);
			ISeason season = GetSeason(coverArtUrl);

			renderer.Render(new ISeason[] { season }, null);
			PictureBox pictureBox = (PictureBox)target.Controls[0];

			Assert.IsNull(pictureBox.ImageLocation, "Cover art should not load from o URL when a cover art URL is not provided.");
		}

		private static ISeason GetSeason(string coverArtUrl = null) {
			ISeason season = A.Fake<ISeason>();
			A.CallTo(() => season.CoverArtUrl).Returns(coverArtUrl);
			return season;
		}
	}
}
