using au.Settings.Types;

namespace au.Applications.MythClient.Settings.Types {
	/// <summary>
	/// All settings for MythClient.
	/// </summary>
	public interface IMythSettings {
		/// <summary>
		/// How to sort recordings.
		/// </summary>
		RecordingSortOption RecordingSortOption { get; set; }

		/// <summary>
		/// The last directory used to export recordings.
		/// </summary>
		string LastExportDirectory { get; set; }

		/// <summary>
		/// Settings pertaining to the MythTV server.
		/// </summary>
		IServerSettings Server { get; }

		/// <summary>
		/// Settings for opening the main form.
		/// </summary>
		IFormGeometrySettings MainForm { get; }

		/// <summary>
		/// True if settings have values required to run the application.
		/// </summary>
		bool HasRequiredSettings { get; }
	}
}
