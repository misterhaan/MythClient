using System.Collections.Generic;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;

namespace au.Applications.MythClient.UI.Render {
	/// <summary>
	/// Capable of rendering the info panel for a season
	/// </summary>
	internal class SeasonInfoRenderer : InfoRendererBase<ISeason> {
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="control">Info render target</param>
		internal SeasonInfoRenderer(ScrollableControl control)
			: base(control) { }

		/// <inheritdoc />
		protected override IEnumerable<Control> GetControlsToRender(ISeason item) {
			// if season info is missing from some recordings, this will show "... Season 0"
			yield return Title(InfoText.SeasonTitle, item.ShowTitle, item.Number);
			yield return Info(InfoText.NumEpisodes, item.Episodes.Count);
			yield return Info(InfoText.RecordedRange, item.OldestEpisode.Recorded, item.NewestEpisode.Recorded);
			yield return Duration(item.Duration);

			yield return Action(Icons.Material_Export18, ActionText.ExportCaption, ActionText.ExportSeasonTooltip, ActionKey.ExportSeason);

			foreach(Control control in GetNextUpControls(item.OldestEpisode, true))
				yield return control;
		}
	}
}
