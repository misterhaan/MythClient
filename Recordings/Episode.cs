using System;
using au.Applications.MythClient.Recordings.Types;
using au.IO.Web.API.MythTV.Types;

namespace au.Applications.MythClient.Recordings {
	/// <summary>
	/// A single episode of a show.
	/// </summary>
	internal class Episode : IEpisode {
		/// <summary>
		/// Status of the recording
		/// </summary>
		private readonly RecStatusType _status;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="showTitle">Title of the show this episode belongs to</param>
		/// <param name="seasonNumber">Number of the season this episode belongs to</param>
		/// <param name="id">ID of this recording used to identify it on the MythTV server</param>
		/// <param name="filename">Name of this episode's video file, which can be found in the raw recordings directory</param>
		/// <param name="title">Title of this episode (doesn't include show title)</param>
		/// <param name="number">Episode number for this episode, or 0 if none</param>
		/// <param name="aired">Date this episode first aired</param>
		/// <param name="recorded">Date and time this episode began recording</param>
		/// <param name="doneRecording">Date and time this episode finished (or is scheduled to finish) recording</param>
		/// <param name="status">Status of the recording</param>
		/// <param name="previewImageUrl">URL to the preview image for the recording</param>
		internal Episode(string showTitle, int seasonNumber, uint id, string filename, string title, int number, DateTime? aired, DateTime? recorded, DateTime? doneRecording, RecStatusType status, string previewImageUrl) {
			ShowTitle = showTitle;
			SeasonNumber = seasonNumber;
			ID = id;
			Filename = filename;
			SubTitle = title;
			Number = number;

			if(recorded.HasValue)
				Recorded = recorded.Value;
			if(doneRecording.HasValue)
				DoneRecording = doneRecording.Value;
			FirstAired = aired ?? Recorded;

			_status = status;
			PreviewImageUrl = previewImageUrl;
		}

		public uint ID { get; }

		/// <inheritdoc />
		public string Filename { get; }


		/// <inheritdoc />
		public string ShowTitle { get; }

		/// <inheritdoc />
		public int SeasonNumber { get; }

		/// <inheritdoc />
		public string SubTitle { get; }

		/// <inheritdoc />
		public int Number { get; }

		/// <inheritdoc />
		public DateTime FirstAired { get; }

		/// <inheritdoc />
		public DateTime Recorded { get; }

		/// <inheritdoc />
		public DateTime DoneRecording { get; }

		/// <inheritdoc />
		public TimeSpan Duration => DoneRecording - Recorded;

		/// <inheritdoc />
		public bool InProgress => _status == RecStatusType.Recording;

		/// <inheritdoc />
		public string PreviewImageUrl { get; }

		/// <summary>
		/// Compares this instance to a specified Episode and returns an indication of their relative values.
		/// </summary>
		/// <param name="other">Episode to compare</param>
		/// <returns>Comparison value used for sorting</returns>
		public int CompareTo(IEpisode other) {
			int numberCompare = Number.CompareTo(other.Number);
			return numberCompare == 0
				? FirstAired.CompareTo(other.FirstAired)
				: numberCompare;
		}

		/// <summary>
		/// Compares this instance to a specified Episode and returns an indication of their relative values.
		/// </summary>
		/// <param name="obj">Episode to compare</param>
		/// <returns>Comparison value used for sorting</returns>
		public int CompareTo(object obj)
			=> throw new ArgumentException($"{nameof(Episode)} objects can only be compared to other {nameof(IEpisode)} objects.", nameof(obj));
	}
}
