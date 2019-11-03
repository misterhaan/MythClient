using System;
using System.Collections.Generic;

namespace au.Applications.MythClient.Recordings.Types {
	/// <summary>
	/// A TV show that has some recordings.
	/// </summary>
	public interface IShow {
		/// <summary>
		/// Title of the show
		/// </summary>
		string Title { get; }

		/// <summary>
		/// Category of the show
		/// </summary>
		string Category { get; }

		/// <summary>
		/// Whether this show comes as a series
		/// </summary>
		bool IsSeries { get; }

		/// <summary>
		/// Seasons of the show that have recordings
		/// </summary>
		IReadOnlyList<ISeason> Seasons { get; }

		/// <summary>
		/// Number of recorded episodes of this show
		/// </summary>
		int NumEpisodes { get; }

		/// <summary>
		/// The oldest recorded episode of this show
		/// </summary>
		IEpisode OldestEpisode { get; }

		/// <summary>
		/// The newest recorded episode of this show
		/// </summary>
		IEpisode NewestEpisode { get; }

		/// <summary>
		/// Total duration of all recorded episodes of this show
		/// </summary>
		TimeSpan Duration { get; }

		/// <summary>
		/// Cover art for the oldest season of this show that has cover art
		/// </summary>
		string CoverArtUrl { get; }

		/// <summary>
		/// Compare to another show object and determine equivalence.
		/// </summary>
		/// <param name="other">Other show to compare</param>
		/// <returns>True if the shows are equivalent</returns>
		bool Matches(IShow other);

		/// <summary>
		/// Attempt to find the specified season, or the next season if the actual season no longer exists.
		/// </summary>
		/// <param name="sample">Sample season to find</param>
		/// <returns>Best match for the sample season, or null if sample was null or there are no seasons</returns>
		ISeason FindSeason(ISeason sample);
	}
}
