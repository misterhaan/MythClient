using au.Applications.MythClient.Recordings.Types;

namespace au.Applications.MythClient.UI.Actions {
	/// <summary>
	/// Handles export of recordings.
	/// </summary>
	internal interface IRecordingsExporter {
		/// <summary>
		/// Export a single episode.
		/// </summary>
		/// <param name="episode">Episode to export</param>
		void Export(IEpisode episode);

		/// <summary>
		/// Export all episodes of a season.
		/// </summary>
		/// <param name="season">Season to export</param>
		void Export(ISeason season);

		/// <summary>
		/// Export all episodes of a show.
		/// </summary>
		/// <param name="show">Show to export</param>
		void Export(IShow show);
	}
}