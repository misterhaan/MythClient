using System.Collections.Generic;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;

namespace au.Applications.MythClient.UI.Render {
	/// <summary>
	/// Capable of rendering the contents panel for all possible levels.
	/// </summary>
	internal interface IContentsRenderer : IContentsRenderer<IShow>, IContentsRenderer<ISeason>, IContentsRenderer<IEpisode> {
		/// <summary>
		/// Update the view to indicate that the specified control is now selected.
		/// </summary>
		/// <param name="selected">Control that should render as selected</param>
		void UpdateSelected(Control selected);

		/// <summary>
		/// Update the view to indicate that the specified control is now selected.
		/// </summary>
		/// <param name="index">Index of the control that should render as selected</param>
		void UpdateSelected(int index);

		/// <summary>
		/// Find and return the control that is currently selected.
		/// </summary>
		/// <returns>Currently selected control</returns>
		Control GetSelected();

		/// <summary>
		/// Find and return the index of the control that is currently selected.
		/// </summary>
		/// <returns>Index of the currently selected control</returns>
		int GetSelectedIndex();

		/// <summary>
		/// Count how many controls fit on a row.
		/// </summary>
		/// <returns>Number of controls fit on a row, or -1 if there is only one row</returns>
		int GetControlsPerRow();
	}


	/// <summary>
	/// Capable of rendering the contents panel for the specified item type.
	/// </summary>
	/// <typeparam name="TItem">Type of items to show in the contents.</typeparam>
	internal interface IContentsRenderer<TItem> {
		/// <summary>
		/// Render the contents panel with the specified items.
		/// </summary>
		/// <param name="items">All items to show in the contents</param>
		/// <param name="selectedItem">The item that should show as selected</param>
		void Render(IReadOnlyCollection<TItem> items, TItem selectedItem);
	}
}
