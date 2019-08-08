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
	}
}
