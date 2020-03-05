namespace au.Applications.MythClient.UI.Render {
	/// <summary>
	/// Details for a contents Show item that are needed at higher levels.
	/// </summary>
	internal class ShowItemTag : IShowItemTag {
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="title">Title of this show</param>
		/// <param name="isSeries">Whether this show comes as a series</param>
		internal ShowItemTag(string title, bool isSeries) {
			Title = title;
			IsSeries = isSeries;
		}

		/// <inheritdoc />
		public string Title { get; }

		/// <inheritdoc />
		public bool IsSeries { get; }
	}
}
