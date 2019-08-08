using System.Drawing;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;

namespace au.Applications.MythClient.UI.Render {
	/// <summary>
	/// Capable of rendering the contents panel at the all recordings level.
	/// </summary>
	internal class RecordingsContentsRenderer : ContentsRendererBase<IShow> {
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="control">Render target control</param>
		internal RecordingsContentsRenderer(ScrollableControl control)
			: base(control) { }

		/// <inheritdoc />
		protected override Control BuildItemControl(IShow item, bool isSelected) {
			PictureBox pb = new PictureBox {
				ErrorImage = Images.Static1080p,
				InitialImage = Images.Static1080p,
				Width = 212,
				Height = 301,
				SizeMode = PictureBoxSizeMode.StretchImage,
				Padding = new Padding(_padding),
				Margin = new Padding(_margin),
				BackColor = isSelected
					? SystemColors.Highlight
					: Control.BackColor,
				Tag = new ShowItemTag(item.IsSeries),
				WaitOnLoad = false
			};
			if(!string.IsNullOrEmpty(item.CoverArtUrl))
				pb.LoadAsync(item.CoverArtUrl);
			else
				pb.Image = Images.Static1080p;
			return pb;
		}
	}
}
