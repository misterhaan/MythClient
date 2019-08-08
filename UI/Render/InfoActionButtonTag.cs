namespace au.Applications.MythClient.UI.Render {
	/// <summary>
	/// Details for an info panel action button that are needed at higher levels.
	/// </summary>
	internal class InfoActionButtonTag : IInfoActionButtonTag {
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="actionKey">Which action this button should invoke</param>
		/// <param name="tooltip">Tooltip text for this button</param>
		internal InfoActionButtonTag(ActionKey actionKey, string tooltip) {
			ActionKey = actionKey;
			Tooltip = tooltip;
		}

		/// <inheritdoc />
		public ActionKey ActionKey { get; }

		/// <inheritdoc />
		public string Tooltip { get; }
	}
}
