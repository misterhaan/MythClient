namespace au.Applications.MythClient.UI {
	/// <summary>
	/// Actions that can be triggered by buttons
	/// </summary>
	internal enum ActionKey {
		/// <summary>
		/// Export all episodes of a show
		/// </summary>
		ExportShow,

		/// <summary>
		/// Delete all episodes of a show
		/// </summary>
		DeleteShow,

		/// <summary>
		/// Play the oldest episode of a show with the default application
		/// </summary>
		PlayShowOldest,

		/// <summary>
		/// Play the oldest episode of a show after prompting for an application
		/// </summary>
		PlayShowOldestWith,

		/// <summary>
		/// Delete the oldest episode of a show
		/// </summary>
		DeleteShowOldest,

		/// <summary>
		/// Export all episodes of a season
		/// </summary>
		ExportSeason,

		/// <summary>
		/// Play the oldest episode of a season with the default application
		/// </summary>
		PlaySeasonOldest,

		/// <summary>
		/// Play the oldest episode of a season after prompting for an application
		/// </summary>
		PlaySeasonOldestWith,

		/// <summary>
		/// Delete the oldest episode of a season
		/// </summary>
		DeleteSeasonOldest,

		/// <summary>
		/// Play an episode with the default application
		/// </summary>
		PlayEpisode,

		/// <summary>
		/// Play an episode after prompting for an application
		/// </summary>
		PlayEpisodeWith,

		/// <summary>
		/// Export an episode
		/// </summary>
		ExportEpisode,

		/// <summary>
		/// Delete an episode
		/// </summary>
		DeleteEpisode,

		/// <summary>
		/// Export a movie or TV special
		/// </summary>
		ExportMovie
	}
}
