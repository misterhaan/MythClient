using System.IO;
using au.Applications.MythClient.Recordings.Types;
using au.Applications.MythClient.Settings.Types;

namespace au.Applications.MythClient.UI {
	/// <summary>
	/// Base class for classes needing to access recording files.
	/// </summary>
	internal abstract class NeedsRecordingFilesBase {
		protected NeedsRecordingFilesBase(IMythSettings settings) {
			_settings = settings;
		}

		/// <summary>
		/// Application settings
		/// </summary>
		protected readonly IMythSettings _settings;

		/// <summary>
		/// Gets the recording file for an episode.
		/// </summary>
		/// <param name="episode">Episode whose recording file is needed</param>
		/// <returns>File for the episode</returns>
		/// <exception cref="FileNotFoundException">When the file doesn't exist</exception>
		protected FileInfo GetRecordingFile(IEpisode episode) {
			FileInfo file = new FileInfo(Path.Combine(_settings.Server.RawFilesDirectory, episode.Filename));
			if(!file.Exists)
				throw new FileNotFoundException(ExceptionMessages.FileNotFound, file.FullName);
			return file;
		}

		/// <summary>
		/// Gets the full filename for an episode.
		/// </summary>
		/// <param name="episode">Episode whose full filename is needed</param>
		/// <returns>Full filename for the episode</returns>
		/// <exception cref="FileNotFoundException">When the file doesn't exist</exception>
		protected string GetRecordingFileName(IEpisode episode)
			=> GetRecordingFile(episode).FullName;
	}
}
