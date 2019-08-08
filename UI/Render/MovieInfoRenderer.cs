using System.Collections.Generic;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;

namespace au.Applications.MythClient.UI.Render {
	/// <summary>
	/// Capable of rendering a movie or TV special to the info panel.
	/// </summary>
	internal class MovieInfoRenderer : InfoRendererBase<IShow> {
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="control">Info render target</param>
		internal MovieInfoRenderer(ScrollableControl control)
			: base(control) { }

		/// <inheritdoc />
		protected override IEnumerable<Control> GetControlsToRender(IShow item) {
			IEpisode episode = item.NewestEpisode;
			yield return Title(InfoText.MovieTitle, item.Title, episode.FirstAired);
			if(!string.IsNullOrEmpty(item.Category))
				yield return Info(item.Category);
			yield return Duration(episode.Duration);
			yield return Thumbnail(episode.PreviewImageUrl);
			yield return Info(InfoText.Recorded, episode.Recorded);

			yield return Action(Icons.Material_Play18, ActionText.PlayCaption, string.Format(ActionText.PlayEpisodeTooltip, item.Title), ActionKey.PlayShowOldest);
			yield return Action(Icons.Material_PlayWith18, ActionText.PlayWithCaption, string.Format(ActionText.PlayWithEpisodeTooltip, item.Title), ActionKey.PlayShowOldestWith);
			yield return Action(Icons.Material_Export18, ActionText.ExportCaption, string.Format(ActionText.ExportEpisodeTooltip, item.Title), ActionKey.ExportMovie);
			yield return Action(Icons.Material_Delete18, ActionText.DeleteCaption, string.Format(ActionText.DeleteEpisodeTooltip, item.Title), ActionKey.DeleteShowOldest);
		}
	}
}
