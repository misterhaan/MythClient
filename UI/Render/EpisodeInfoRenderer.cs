using System.Collections.Generic;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;

namespace au.Applications.MythClient.UI.Render {
	/// <summary>
	/// Capable of rendering the info panel for an episode
	/// </summary>
	internal class EpisodeInfoRenderer : InfoRendererBase<IEpisode> {
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="control">Info render target</param>
		internal EpisodeInfoRenderer(ScrollableControl control)
			: base(control) { }

		/// <inheritdoc />
		protected override IEnumerable<Control> GetControlsToRender(IEpisode item) {
			string title = string.IsNullOrEmpty(item.SubTitle)
				? string.Format(InfoText.EpisodeNamelessTitle, item.ShowTitle, item.FirstAired)
				: string.Format(InfoText.EpisodeTitle, item.ShowTitle, item.SubTitle);

			yield return Title(title);
			foreach(Control infoControl in GetEpisodeInfoControls(item, false))
				yield return infoControl;

			yield return Action(Icons.Material_Play18, ActionText.PlayCaption, string.Format(ActionText.PlayEpisodeTooltip, title), ActionKey.PlayEpisode);
			yield return Action(Icons.Material_PlayWith18, ActionText.PlayWithCaption, string.Format(ActionText.PlayWithEpisodeTooltip, title), ActionKey.PlayEpisodeWith);
			yield return Action(Icons.Material_Export18, ActionText.ExportCaption, string.Format(ActionText.ExportEpisodeTooltip, title), ActionKey.ExportEpisode);
			yield return Action(Icons.Material_Delete18, ActionText.DeleteCaption, string.Format(ActionText.DeleteEpisodeTooltip, title), ActionKey.DeleteEpisode);
		}
	}
}
