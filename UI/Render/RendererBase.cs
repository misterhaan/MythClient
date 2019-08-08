using System.Windows.Forms;

namespace au.Applications.MythClient.UI.Render {
	/// <summary>
	/// Common logic for renderer classes.
	/// </summary>
	internal abstract class RendererBase {
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="control">Render target control</param>
		protected RendererBase(ScrollableControl control) {
			Control = control;
		}

		/// <summary>
		/// Render target control
		/// </summary>
		protected ScrollableControl Control { get; }
	}
}
