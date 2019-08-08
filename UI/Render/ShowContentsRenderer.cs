using System.Drawing;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;

namespace au.Applications.MythClient.UI.Render {
	internal class ShowContentsRenderer : ContentsRendererBase<ISeason> {
		internal ShowContentsRenderer(ScrollableControl control)
			: base(control) { }

		protected override Control BuildItemControl(ISeason item, bool isSelected) {
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
