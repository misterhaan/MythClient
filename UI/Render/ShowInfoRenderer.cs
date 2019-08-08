using System.Collections.Generic;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;

namespace au.Applications.MythClient.UI.Render {
	/// <summary>
	/// Capable of rendering a series show to the info panel.
	/// </summary>
	internal class ShowInfoRenderer : InfoRendererBase<IShow> {
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="control">Info render target</param>
		internal ShowInfoRenderer(ScrollableControl control)
			: base(control) { }

		/// <inheritdoc />
		protected override IEnumerable<Control> GetControlsToRender(IShow item) {
			yield return Title(item.Title);
			if(!string.IsNullOrEmpty(item.Category))
				yield return Info(item.Category);
			yield return item.NumEpisodes == 1
				? Info(InfoText.OneEpisode)
				: Info(InfoText.NumEpisodes, item.NumEpisodes);
			if(item.Seasons.Count > 1)
				yield return Info(InfoText.NumSeasons, item.Seasons.Count);
			else if(item.OldestEpisode.Number != 0)
				yield return Info(InfoText.OneSeason);
			yield return Info(InfoText.RecordedRange, item.OldestEpisode.Recorded, item.NewestEpisode.Recorded);
			yield return Duration(item.Duration);

			yield return Action(Icons.Material_Export18, ActionText.ExportCaption, ActionText.ExportShowTooltip, ActionKey.ExportShow);
			yield return Action(Icons.Material_Delete18, ActionText.DeleteAllCaption, ActionText.DeleteShowTooltip, ActionKey.DeleteShow);

			foreach(Control control in GetNextUpControls(item.OldestEpisode, false))
				yield return control;
		}
	}
}
