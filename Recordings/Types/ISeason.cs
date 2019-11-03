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

		/// <summary>
		/// Compare to another season object and determine equivalence.
		/// </summary>
		/// <param name="other">Other season to compare</param>
		/// <returns>True if the seasons are equivalent</returns>
		bool Matches(ISeason other);

		/// <summary>
		/// Attempt to find the specified episode, or the next episode if the actual episode no longer exists.
		/// </summary>
		/// <param name="sample">Sample episode to find</param>
		/// <returns>Best match for the sample episode, or null if sample was null or there are no episodes</returns>
		IEpisode FindEpisode(IEpisode sample);
	}
}
