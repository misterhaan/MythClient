using System.Drawing;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;

namespace au.Applications.MythClient.UI.Render {
	/// <summary>
	/// Capable of rendering the contents panel at the show level.
	/// </summary>
	internal class ShowContentsRenderer : CoverArtContentsRendererBase<ISeason> {
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="control">Render target control</param>
		internal ShowContentsRenderer(ScrollableControl control)
			: base(control) { }

		/// <inheritdoc />
		protected override Control BuildItemControl(ISeason item, bool isSelected) {
			PictureBox pb = new PictureBox {
				ErrorImage = Images.Static1080p,
				InitialImage = Images.Static1080p,
				Width = _width,
				Height = _height,
				SizeMode = PictureBoxSizeMode.StretchImage,
				Padding = new Padding(_padding),
				Margin = new Padding(_margin),
				BackColor = isSelected
					? SystemColors.Highlight
					: Control.BackColor,
				Tag = string.Format(Titles.Season, item.ShowTitle, item.Number),
				WaitOnLoad = false
			};
			if(!string.IsNullOrEmpty(item.CoverArtUrl))
				pb.LoadAsync(item.CoverArtUrl);
			else {
				pb.Image = Images.Static1080p;
				// normally cover art says what the show is, so we need to write on the static background when we don't have cover art
				pb.Paint += PaintGenericCoverArt;
			}
			return pb;
		}

		/// <inheritdoc />
		protected override string GetTitleFromPictureBox(PictureBox pictureBox)
			=> pictureBox.Tag as string;
	}
}
