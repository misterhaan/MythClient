using System.Windows.Forms;

namespace au.Applications.MythClient.UI.Render.Tests {
	public abstract class RendererTestsBase {
		protected const string PreviewImageUrl = "https://www.track7.org/images/track7.png";

		protected static ScrollableControl GetRenderTargetControl()
			=> new FlowLayoutPanel();

		internal static ContentsRenderer GetContentsRenderer(ScrollableControl target)
			=> new ContentsRenderer(target);

		internal static InfoRenderer GetInfoRenderer(ScrollableControl target)
			=> new InfoRenderer(target);
	}
}
