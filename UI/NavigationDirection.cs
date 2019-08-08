namespace au.Applications.MythClient.UI {
	/// <summary>
	/// Where to navigate
	/// </summary>
	internal enum NavigationDirection {
		/// <summary>
		/// The first item
		/// </summary>
		First,

		/// <summary>
		/// The item directly above the currently-selected item
		/// </summary>
		PreviousRow,

		/// <summary>
		/// The item before the currently-selected item
		/// </summary>
		Previous,

		/// <summary>
		/// The item after the currently-selected item
		/// </summary>
		Next,

		/// <summary>
		/// The item directly below the currently-selected item
		/// </summary>
		NextRow,

		/// <summary>
		/// The last item
		/// </summary>
		Last
	}
}
