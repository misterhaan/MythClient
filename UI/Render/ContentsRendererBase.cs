using System.Collections.Generic;
using System.Windows.Forms;

namespace au.Applications.MythClient.UI.Render {
	/// <summary>
	/// Common logic for contents renderers.
	/// </summary>
	/// <typeparam name="TItem"></typeparam>
	internal abstract class ContentsRendererBase<TItem> : RendererBase, IContentsRenderer<TItem> {
		/// <summary>
		/// Margin applied to each content item
		/// </summary>
		protected const int _margin = 5;

		/// <summary>
		/// Padding applied to each content item
		/// </summary>
		protected const int _padding = 7;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="control">Render target control</param>
		internal ContentsRendererBase(ScrollableControl control)
			: base(control) { }

		/// <inheritdoc />
		public void Render(IReadOnlyCollection<TItem> items, TItem selectedItem) {
			Control.Controls.Clear();
			foreach(TItem item in items) {
				bool selected = (object)item == (object)selectedItem;  // casting to object because it doesn't recognize the two TItem types as the same
				Control itemControl = BuildItemControl(item, selected);
				Control.Controls.Add(itemControl);
				if(selected)
					Control.ScrollControlIntoView(itemControl);
			}
			// add an extra control to fix the scrolling problem
			Control.Controls.Add(new Control {
				Width = Control.Width - Control.Padding.Left - Control.Padding.Right - 1,
				Height = Control.Padding.Bottom + _margin + 1
			});
		}

		/// <summary>
		/// Build a control to represent the item
		/// </summary>
		/// <param name="item">Item control should represent</param>
		/// <param name="isSelected">Whether the control should appear selected</param>
		/// <returns>Control representing the item</returns>
		protected abstract Control BuildItemControl(TItem item, bool isSelected);
	}
}
