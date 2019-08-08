using System;
using System.IO;
using au.Settings;

namespace au.Applications.MythClient.Settings {
	/// <summary>
	/// Manages persistent settings for MythClient
	/// </summary>
	public class SettingsManager : SettingsFileManager<MythSettings> {
		/// <summary>
		/// Name of the settings file in local app data.
		/// </summary>
		private const string _filename = "MythClient.settings";

		/// <summary>
		/// Default constructor.
		/// </summary>
		public SettingsManager() : base(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), _filename)) { }
	}
}
