using System;
using System.Collections.Generic;

namespace au.Applications.MythClient.Recordings.Types {
	/// <summary>
	/// A season of a show.
	/// </summary>
	public interface ISeason {
		/// <summary>
		/// Title of the show this episode belongs to
		/// </summary>
		string ShowTitle { get; }

		/// <summary>
		/// Season number, or 0 for shows without proper seasons
		/// </summary>
		int Number { get; }

		/// <summary>
		/// URL to cover art for this season
		/// </summary>
		string CoverArtUrl { get; }

		/// <summary>
		/// Recorded episodes from this season
		/// </summary>
		IReadOnlyList<IEpisode> Episodes { get; }

		/// <summary>
		/// Oldest recorded episode in this season
		/// </summary>
		IEpisode OldestEpisode { get; }

		/// <summary>
		/// Newest recorded episode in this season
		/// </summary>
		IEpisode NewestEpisode { get; }

		/// <summary>
		/// Total duration of all recorded episodes in this season
		/// </summary>
		TimeSpan Duration { get; }
	}
}
