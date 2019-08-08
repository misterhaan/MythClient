namespace au.Applications.MythClient.Settings.Types {
	/// <summary>
	/// Settings pertaining to the MythTV server.
	/// </summary>
	public interface IServerSettings {
		/// <summary>
		/// MythTV server hostname.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// MythTV server port number.
		/// </summary>
		ushort Port { get; set; }

		/// <summary>
		/// File share path to the MythTV server's recordings directory.
		/// </summary>
		string RawFilesDirectory { get; set; }
	}
}
