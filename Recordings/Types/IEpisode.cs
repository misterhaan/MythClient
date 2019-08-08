using System;

namespace au.Applications.MythClient.Recordings.Types {
	/// <summary>
	/// A single episode of a show.
	/// </summary>
	public interface IEpisode : IComparable<IEpisode>, IComparable {
		/// <summary>
		/// ID of this recording used to identify it on the MythTV server.
		/// </summary>
		uint ID { get; }

		/// <summary>
		/// Name of this episode's video file, which can be found in the raw recordings directory.
		/// </summary>
		string Filename { get; }

		/// <summary>
		/// Title of the show this episode belongs to.
		/// </summary>
		string ShowTitle { get; }

		/// <summary>
		/// Number of the season this episode belongs to.
		/// </summary>
		int SeasonNumber { get; }

		/// <summary>
		/// Title of this episode (doesn't include show title).
		/// </summary>
		string SubTitle { get; }

		/// <summary>
		/// Episode number for this episode, or 0 if none.
		/// </summary>
		int Number { get; }

		/// <summary>
		/// Date this episode first aired.  Doesn't include time, and will not
		/// match the date from Recorded if it's a rerun.
		/// </summary>
		DateTime FirstAired { get; }

		/// <summary>
		/// Date and time this episode began recording.
		/// </summary>
		DateTime Recorded { get; }

		/// <summary>
		/// Date and time this episode finished (or is scheduled to finish) recording.
		/// For episodes currently recording, this is a future date.
		/// </summary>
		DateTime DoneRecording { get; }

		/// <summary>
		/// Duration of this recording, according to the schedule.  Recordings
		/// might start a few minutes early or end a few minutes late, making the
		/// actual video file duration longer.  Loss of digital TV signal can
		/// result in shorter actual video file duration.
		/// </summary>
		TimeSpan Duration { get; }

		/// <summary>
		/// Whether the episode is currently being recorded.
		/// </summary>
		bool InProgress { get; }

		/// <summary>
		/// URL to the preview image of the episode.
		/// </summary>
		string PreviewImageUrl { get; }
	}
}
