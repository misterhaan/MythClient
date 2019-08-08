namespace au.Applications.MythClient.UI {
	/// <summary>
	/// Recordings hierarchy depths
	/// </summary>
	internal enum BrowsingDepth {
		/// <summary>
		/// Highest level with all recordings summarized
		/// </summary>
		Recordings,

		/// <summary>
		/// Within a specific show
		/// </summary>
		Show,

		/// <summary>
		/// Within a specific season of a specific show
		/// </summary>
		Season
	}
}
