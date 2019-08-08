using System;
using System.IO;
using System.Xml.Serialization;
using au.Applications.MythClient.Settings.Types;
using au.Settings;
using au.Settings.Types;

namespace au.Applications.MythClient.Settings {
	/// <summary>
	/// All settings for MythClient.
	/// </summary>
	public class MythSettings : IMythSettings {
		/// <inheritdoc />
		public RecordingSortOption RecordingSortOption { get; set; } = RecordingSortOption.Title;

		/// <inheritdoc />
		public string LastExportDirectory {
			get => _lastExportDirectory;
			set {
				DirectoryInfo dir = new DirectoryInfo(value);
				while(!dir.Exists && dir.Parent != null)  // find nearest existing ancestor if possible
					dir = dir.Parent;
				if(dir.Exists)
					_lastExportDirectory = dir.FullName;
			}
		}
		private string _lastExportDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);

		/// <inheritdoc />
		IServerSettings IMythSettings.Server => Server;

		/// <summary>
		/// Serializable Server implementation.
		/// </summary>
		public ServerSettings Server { get; set; } = new ServerSettings();

		/// <inheritdoc />
		IFormGeometrySettings IMythSettings.MainForm => MainForm;

		/// <summary>
		/// Serializable MainForm implementation.
		/// </summary>
		public FormGeometrySettings MainForm { get; set; } = new FormGeometrySettings();

		/// <inheritdoc />
		[XmlIgnore]
		public bool HasRequiredSettings
			=> !string.IsNullOrEmpty(Server.Name)
				&& !string.IsNullOrEmpty(Server.RawFilesDirectory)
				&& Directory.Exists(Server.RawFilesDirectory);
	}
}
