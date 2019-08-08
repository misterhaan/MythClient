using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.Applications.MythClient.UI.Render.Tests {
	[TestClass]
	public class SeasonInfoRendererTests : RendererTestsBase {
		[TestMethod]
		public void Render_Succeeds() {
			ScrollableControl target = GetRenderTargetControl();
			InfoRenderer renderer = GetInfoRenderer(target);
			ISeason season = GetSeason();

			renderer.Render(season);
		}

		private static ISeason GetSeason() {
			ISeason season = A.Fake<ISeason>();
			return season;
		}
	}
}
