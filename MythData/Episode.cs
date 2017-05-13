using System;
using System.Drawing;

namespace au.Applications.MythClient.Data {
	public class Episode : IComparable {
		private string _recordingId;  // for referencing this recording in Myth API calls

		/// <summary>
		/// Season this Episode belongs to.
		/// </summary>
		public Season Season { get { return _season; } }
		private Season _season;

		/// <summary>
		/// Thumbnail image of this episode.
		/// </summary>
		public Image Thumb {
			get {
				if(_thumb == null && !string.IsNullOrEmpty(_recordingId))
					_thumb = Season.Show.Recordings.GetEpisodeThumb(_recordingId);
				return _thumb;
			}
		}
		private Image _thumb;

		/// <summary>
		/// Name of this episode's video file, which can be found in the raw recordings directory.
		/// </summary>
		public string Filename { get { return _filename; } }
		private string _filename;

		/// <summary>
		/// Name of this episode (doesn't include show title).
		/// </summary>
		public string Name { get { return _name; } }
		private string _name;

		/// <summary>
		/// Episode number for this episode, or 0 if none.
		/// </summary>
		public int Number { get { return _number; } }
		private int _number = 0;

		/// <summary>
		/// Date this episode first aired.  Doesn't include time, and will not
		/// match the date from Recorded if it's a rerun.
		/// </summary>
		public DateTime FirstAired { get { return _aired; } }
		private DateTime _aired;

		/// <summary>
		/// Date and time this episode was recorded.
		/// </summary>
		public DateTime Recorded { get { return _recorded; } }
		private DateTime _recorded;

		/// <summary>
		/// Date and time this episode finished (or will finish) recording.  For
		/// episodes currently recording, this is a future date.
		/// </summary>
		public DateTime DoneRecording { get { return _doneRecording; } }
		private DateTime _doneRecording;

		/// <summary>
		/// Duration of this recording, according to the schedule.  Recordings
		/// might start a few minutes early or end a few minutes late, making the
		/// actual video file duration longer.  Loss of digital TV signal can
		/// result in shorter actual video file duration.
		/// </summary>
		public TimeSpan Duration { get { return DoneRecording - Recorded; } }

		/// <summary>
		/// Whether the episode is currently being recorded.
		/// </summary>
		public bool InProgress { get { return _inProgress; } }
		private bool _inProgress = false;

		/// <summary>
		/// Whether the episode is a movie and not actually an episode from a series.
		/// </summary>
		public bool IsMovie { get { return _catType.Equals("movie", StringComparison.CurrentCultureIgnoreCase); } }
		private string _catType = "";

		/// <summary>
		/// Creates a new Episode.
		/// </summary>
		/// <param name="season">Season this episode belongs to</param>
		/// <param name="recordedId">ID of this recording used for further Myth API calls about this recording</param>
		/// <param name="filename">Name of this episode's video file (without path)</param>
		/// <param name="name">Name of this episode (without show title)</param>
		/// <param name="number">Episode number (should parse as integer)</param>
		/// <param name="aired">Date first aired</param>
		/// <param name="recorded">Date and time recorded</param>
		/// <param name="endtime">Date and time the recording ended</param>
		/// <param name="status">Status of the recording</param>
		/// <param name="catType">Type of the recording (movie or series)</param>
		public Episode(Season season, string recordedId, string filename, string name, string number, string aired, string recorded, string endtime, string status, string catType) {
			_season = season;
			_recordingId = recordedId;
			_filename = filename;
			_name = name;
			int.TryParse(number, out _number);
			bool hasAired = DateTime.TryParse(aired, out _aired);
			if(DateTime.TryParse(recorded, out _recorded)) {
				DateTime.TryParse(endtime, out _doneRecording);
				if(!hasAired)
					_aired = _recorded;
			}
			_inProgress = status.Equals("Recording", StringComparison.CurrentCultureIgnoreCase);
			_catType = catType;
		}

		/// <summary>
		/// Delete this Episode from the server.
		/// </summary>
		/// <param name="rerecord">Whether the server should rerecord this episode</param>
		/// <returns>True if the Episode was deleted successfully</returns>
		public bool Delete(bool rerecord) {
			if(Season.Show.Recordings.DeleteRecording(_recordingId, rerecord)) {
				Season.Episodes.Remove(this);
				if(Season.Episodes.Count < 1) {
					Season.Show.Seasons.Remove(Season);
					if(Season.Show.Seasons.Count < 1)
						Season.Show.Recordings.Shows.Remove(Season.Show);
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// Compares this instance to a specified Episode and returns an indication of their relative values.
		/// </summary>
		/// <param name="episode">Episode to compare</param>
		/// <returns>Comparison value used for sorting</returns>
		public int CompareTo(Episode episode) {
			// use episode number first, but if they're the same then use the date first aired.
			int num = Number.CompareTo(episode.Number);
			return num == 0 ? FirstAired.CompareTo(episode.FirstAired) : num;
		}
		/// <summary>
		/// Compares this instance to a specified Episode and returns an indication of their relative values.
		/// </summary>
		/// <param name="obj">Episode to compare</param>
		/// <returns>Comparison value used for sorting</returns>
		public int CompareTo(object obj) {
			if(obj is Episode)
				return CompareTo(obj as Episode);
			throw new NotImplementedException();
		}
	}
}
