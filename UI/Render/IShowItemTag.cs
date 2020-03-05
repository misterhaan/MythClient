namespace au.Applications.MythClient.UI.Render {
	/// <summary>
	/// Details for a contents Show item that are needed at higher levels.
	/// </summary>
	internal interface IShowItemTag {
		/// <summary>
		/// Title of this show
		/// </summary>
		string Title { get; }

		/// <summary>
		/// Whether this show comes as a series
		/// </summary>
		bool IsSeries { get; }
	}
}
