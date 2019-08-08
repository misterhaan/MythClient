namespace au.Applications.MythClient.UI.Render {
	/// <summary>
	/// Details for an info panel action button that are needed at higher levels.
	/// </summary>
	internal interface IInfoActionButtonTag {
		/// <summary>
		/// Which action this button should invoke
		/// </summary>
		ActionKey ActionKey { get; }

		/// <summary>
		/// Tooltip text for this button
		/// </summary>
		string Tooltip { get; }
	}
}