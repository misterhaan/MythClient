using System;
using System.Collections.Generic;
using System.Linq;
using au.Applications.MythClient.Recordings.Types;

namespace au.Applications.MythClient.Recordings {
	/// <summary>
	/// A season of a show.
	/// </summary>
	internal class Season : ISeason {
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="showTitle">Title of the show this episode belongs to</param>
		/// <param name="number">Season number, or 0 for shows without proper seasons</param>
		internal Season(string showTitle, int number) {
			ShowTitle = showTitle;
			Number = number;
		}

		/// <inheritdoc />
		public string ShowTitle { get; }

		/// <inheritdoc />
		public int Number { get; }

		/// <inheritdoc />
		public string CoverArtUrl { get; internal set; }

		/// <inheritdoc />
		public IReadOnlyList<IEpisode> Episodes { get; internal set; } = new List<IEpisode>();

		/// <inheritdoc />
		public IEpisode OldestEpisode => Episodes.OrderBy(EpisodeAired).First();

		/// <inheritdoc />
		public IEpisode NewestEpisode => Episodes.OrderByDescending(EpisodeAired).First();

		/// <inheritdoc />
		public TimeSpan Duration
			=> Episodes.Aggregate(TimeSpan.Zero, (sum, episode) => sum + episode.Duration);

		/// <inheritdoc />
		public bool Matches(ISeason other)
			=> Number == other?.Number;

		/// <inheritdoc />
		public IEpisode FindEpisode(IEpisode example) {
			if(example == null || Episodes.Contains(example))
				return example;

			foreach(IEpisode episode in Episodes)
				if(episode.CompareTo(example) >= 0)
					return episode;

			return Episodes.LastOrDefault();
		}

		/// <summary>
		/// Get the first-aired date of the specified episode.
		/// </summary>
		/// <param name="episode">Episode to get the first-aired date from</param>
		/// <returns>First-aired date</returns>
		private static DateTime EpisodeAired(IEpisode episode)
			=> episode.FirstAired;
	}
}
