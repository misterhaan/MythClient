using au.Applications.MythClient.Settings.Types;

namespace au.Applications.MythClient.Settings {
	/// <summary>
	/// Settings pertaining to the MythTV server.
	/// </summary>
	public class ServerSettings : IServerSettings {
		/// <summary>
		/// Default port for connecting to the MythTV server.
		/// </summary>
		public const int DefaultPort = 6544;

		/// <inheritdoc />
		public string Name { get; set; }

		/// <inheritdoc />
		public ushort Port { get; set; } = DefaultPort;

		/// <inheritdoc />
		public string RawFilesDirectory { get; set; }
	}
}
