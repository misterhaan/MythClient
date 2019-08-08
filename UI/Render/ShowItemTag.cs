namespace au.Applications.MythClient.UI.Render {
	/// <summary>
	/// Details for a contents Show item that are needed at higher levels.
	/// </summary>
	internal class ShowItemTag : IShowItemTag {
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="isSeries">Whether this show comes as a series</param>
		internal ShowItemTag(bool isSeries) {
			IsSeries = isSeries;
		}

		/// <inheritdoc />
		public bool IsSeries { get; }
	}
}
