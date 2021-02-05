using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace au.Applications.MythClient.UI.Render {
	/// <summary>
	/// Common logic for contents renderers that render cover art.
	/// </summary>
	/// <typeparam name="TItem"></typeparam>
	internal abstract class CoverArtContentsRendererBase<TItem> : ContentsRendererBase<TItem> {
		/// <summary>
		/// Cover art width
		/// </summary>
		protected const int _width = 212;

		/// <summary>
		/// Cover art height
		/// </summary>
		protected const int _height = 301;

		/// <summary>
		/// Area on top of the cover art where a title can be written
		/// </summary>
		private static RectangleF _textArea = new RectangleF(_padding * 2, _padding * 2, _width - 4 * _padding, _width - 4 * _padding);

		/// <summary>
		/// Size of the readability outline to draw behind the title
		/// </summary>
		private const int _outlineSize = 9;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="control">Render target control</param>
		internal CoverArtContentsRendererBase(ScrollableControl control)
			: base(control) { }

		/// <summary>
		/// Paint event handler for cover art that needs to have the title manually added
		/// </summary>
		/// <param name="sender">Control being painted</param>
		/// <param name="e">Provides access to the graphics object</param>
		protected void PaintGenericCoverArt(object sender, PaintEventArgs e) {
			if(sender is PictureBox pb) {
				string title = GetTitleFromPictureBox(pb);
				if(!string.IsNullOrEmpty(title)) {
					e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
					using GraphicsPath path = new GraphicsPath();
					using StringFormat format = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };
					using Brush background = new SolidBrush(Color.FromArgb(96, Color.Black));
					using Pen outline = new Pen(background, _outlineSize);
					path.AddString(title, pb.Font.FontFamily, (int)FontStyle.Regular, e.Graphics.DpiY * 16 / 72, _textArea, format);
					e.Graphics.DrawPath(outline, path);
					e.Graphics.FillPath(Brushes.White, path);
				}
			}
		}

		/// <summary>
		/// Find the title that should be displayed based on the PictureBox control.
		/// </summary>
		/// <param name="pictureBox">Control being painted</param>
		/// <returns>Title to display on the control</returns>
		protected abstract string GetTitleFromPictureBox(PictureBox pictureBox);
	}
}
