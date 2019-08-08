using System;
using System.Collections.Generic;
using System.Linq;
using au.Applications.MythClient.Recordings.Types;

namespace au.Applications.MythClient.Recordings {
	/// <summary>
	/// A TV show that has some recordings.
	/// </summary>
	public class Show : IShow {
		private const string _seriesCatType = "series";

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="title">Title of the show</param>
		/// <param name="category">Category of the show</param>
		/// <param name="type">Type of the show</param>
		internal Show(string title, string category, string type) {
			Title = title;
			Category = category;
			IsSeries = _seriesCatType.Equals(type, StringComparison.InvariantCultureIgnoreCase);
		}

		/// <inheritdoc />
		public string Title { get; }

		/// <inheritdoc />
		public string Category { get; }

		/// <inheritdoc />
		public bool IsSeries { get; }

		/// <inheritdoc />
		public IReadOnlyList<ISeason> Seasons { get; internal set; } = new List<ISeason>();

		/// <inheritdoc />
		public int NumEpisodes => Seasons.Sum(SeasonEpisodeCount);

		/// <inheritdoc />
		public IEpisode OldestEpisode
			=> Seasons.OrderBy(SeasonOldestAiredDate).First().OldestEpisode;

		/// <inheritdoc />
		public IEpisode NewestEpisode
			=> Seasons.OrderByDescending(SeasonNewestAiredDate).First().NewestEpisode;

		/// <inheritdoc />
		public TimeSpan Duration
			=> Seasons.Aggregate(TimeSpan.Zero, (sum, season) => sum + season.Duration);

		/// <inheritdoc />
		public string CoverArtUrl
			=> Seasons.FirstOrDefault(SeasonHasCoverArtUrl)?.CoverArtUrl;

		/// <summary>
		/// Gets the count of episodes in a season.
		/// </summary>
		/// <param name="season">Season to count episodes</param>
		/// <returns>Episode count</returns>
		private static int SeasonEpisodeCount(ISeason season)
			=> season.Episodes.Count;

		/// <summary>
		/// Gets the first-aired date of the oldest episode in a season.
		/// </summary>
		/// <param name="season">Season to find oldest aired date</param>
		/// <returns>Oldest first-aired date</returns>
		private static DateTime SeasonOldestAiredDate(ISeason season)
			=> season.OldestEpisode.FirstAired;

		/// <summary>
		/// Gets the first-aired date of the newest episode in a season.
		/// </summary>
		/// <param name="season">Season to find newest aired date</param>
		/// <returns>Newest first-aired date</returns>
		private static DateTime SeasonNewestAiredDate(ISeason season)
			=> season.NewestEpisode.FirstAired;

		/// <summary>
		/// Whether a season has a cover art URL.
		/// </summary>
		/// <param name="season">Season that might have cover art</param>
		/// <returns>True if the season has a cover art URL</returns>
		private static bool SeasonHasCoverArtUrl(ISeason season)
			=> !string.IsNullOrEmpty(season.CoverArtUrl);
	}
}
