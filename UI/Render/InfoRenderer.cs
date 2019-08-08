using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;

namespace au.Applications.MythClient.UI.Render {
	/// <summary>
	/// Capable of rendering the info panel for all possible levels.
	/// </summary>
	internal class InfoRenderer : IInfoRenderer {
		/// <summary>
		/// Capable of rendering the info panel for a movie or TV special
		/// </summary>
		private readonly IInfoRenderer<IShow> _movie;

		/// <summary>
		/// Capable of rendering the info panel for a series show
		/// </summary>
		private readonly IInfoRenderer<IShow> _show;

		/// <summary>
		/// Capable of rendering the info panel for a season
		/// </summary>
		private readonly IInfoRenderer<ISeason> _season;

		/// <summary>
		/// Capable of rendering the info panel for an episode
		/// </summary>
		private readonly IInfoRenderer<IEpisode> _episode;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="control">Info panel control</param>
		internal InfoRenderer(ScrollableControl control) {
			_movie = new MovieInfoRenderer(control);
			_show = new ShowInfoRenderer(control);
			_season = new SeasonInfoRenderer(control);
			_episode = new EpisodeInfoRenderer(control);
		}

		/// <inheritdoc />
		public void Render(IShow item) {
			if(item.IsSeries)
				_show.Render(item);
			else
				_movie.Render(item);
		}

		/// <inheritdoc />
		public void Render(ISeason item)
			=> _season.Render(item);

		/// <inheritdoc />
		public void Render(IEpisode item)
			=> _episode.Render(item);
	}
}
