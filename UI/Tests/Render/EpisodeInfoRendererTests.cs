using System;
using System.Drawing;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.Applications.MythClient.UI.Render.Tests {
	[TestClass]
	public class EpisodeInfoRendererTests : RendererTestsBase {
		private static readonly DateTime _firstAired = new DateTime(2000, 4, 8);

		[TestMethod]
		public void Render_EpisodeHasName_TitleIncludesName() {
			ScrollableControl target = GetRenderTargetControl();
			InfoRenderer renderer = GetInfoRenderer(target);
			IEpisode episode = GetEpisode(nameof(Render_EpisodeHasName_TitleIncludesName));

			renderer.Render(episode);

			Assert.IsTrue(target.Controls[0].Text.EndsWith(nameof(Render_EpisodeHasName_TitleIncludesName)), "The title control should include the episode subtitle when specified.");
		}

		[DataTestMethod]
		[DataRow("")]
		[DataRow(null)]
		public void Render_BlankEpisodeName_TitleIncludesDate(string subtitle) {
			ScrollableControl target = GetRenderTargetControl();
			InfoRenderer renderer = GetInfoRenderer(target);
			IEpisode episode = GetEpisode(subtitle);

			renderer.Render(episode);

			Assert.IsTrue(target.Controls[0].Text.EndsWith(_firstAired.ToShortDateString()), "The title control should include the first aired date when there's no episode subtitle.");
		}

		[TestMethod]
		public void Render_DoesNotIncludeThumbnail() {
			ScrollableControl target = GetRenderTargetControl();
			InfoRenderer renderer = GetInfoRenderer(target);
			IEpisode episode = GetEpisode(nameof(Render_DoesNotIncludeThumbnail));

			renderer.Render(episode);

			bool hasPictureBoxControl = false;
			foreach(Control control in target.Controls)
				if(control is PictureBox) {
					hasPictureBoxControl = true;
					break;
				}

			Assert.IsFalse(hasPictureBoxControl, "Episode info panels should not include a thumbnail.");
		}

		[TestMethod]
		public void Render_InProgress_IncludesRedControl() {
			ScrollableControl target = GetRenderTargetControl();
			InfoRenderer renderer = GetInfoRenderer(target);
			IEpisode episode = GetEpisode(nameof(Render_InProgress_IncludesRedControl), true);

			renderer.Render(episode);

			bool hasRedControl = false;
			foreach(Control control in target.Controls)
				if(control.ForeColor == Color.Red) {
					hasRedControl = true;
					break;
				}

			Assert.IsTrue(hasRedControl, "Episode info panels should include red text when a recording is in progress.");
		}

		[TestMethod]
		public void Render_HasSeasonNumber_IncludesEpisodeNumber() {
			ScrollableControl target = GetRenderTargetControl();
			InfoRenderer renderer = GetInfoRenderer(target);
			IEpisode episode = GetEpisode(nameof(Render_HasSeasonNumber_IncludesEpisodeNumber), false, 3, 11);

			renderer.Render(episode);

			bool includesEpisodeNumber = false;
			string episodeNumber = string.Format(InfoText.SeasonNumberEpisodeNumber, 3, 11);
			foreach(Control control in target.Controls)
				if(control.Text == episodeNumber) {
					includesEpisodeNumber = true;
					break;
				}

			Assert.IsTrue(includesEpisodeNumber, "Episode info panels should include the episode number when the episode is part of a nonzero season.");
		}

		private static IEpisode GetEpisode(string subtitle, bool inProgress = false, int seasonNumber = 0, int episodeNumber = 0) {
			IEpisode episode = A.Fake<IEpisode>();
			A.CallTo(() => episode.ShowTitle).Returns(nameof(EpisodeInfoRendererTests));
			A.CallTo(() => episode.SubTitle).Returns(subtitle);
			A.CallTo(() => episode.FirstAired).Returns(_firstAired);
			A.CallTo(() => episode.InProgress).Returns(inProgress);
			A.CallTo(() => episode.SeasonNumber).Returns(seasonNumber);
			A.CallTo(() => episode.Number).Returns(episodeNumber);
			return episode;
		}
	}
}
