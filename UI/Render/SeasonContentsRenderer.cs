using System.Drawing;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;
using au.UI.CaptionedPictureBox;

namespace au.Applications.MythClient.UI.Render {
	/// <summary>
	/// Capable of rendering the contents panel at the season level.
	/// </summary>
	internal class SeasonContentsRenderer : ContentsRendererBase<IEpisode> {
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="control">Render target control</param>
		internal SeasonContentsRenderer(ScrollableControl control)
			: base(control) { }

		/// <inheritdoc />
		protected override Control BuildItemControl(IEpisode item, bool isSelected) {
			CaptionedPictureBox cpb = new CaptionedPictureBox {
				ErrorImage = Images.Static1080p,
				InitialImage = Images.Static1080p,
				Text = item.SubTitle ?? item.FirstAired.ToShortDateString(),
				Width = 212,
				SizeMode = PictureBoxSizeMode.StretchImage,
				Padding = new Padding(_padding),
				Margin = new Padding(_margin),
				BackColor = isSelected
					? SystemColors.Highlight
					: Control.BackColor,
				ForeColor = isSelected
					? SystemColors.HighlightText
					: Control.ForeColor,
				WaitOnLoad = false
			};
			cpb.Height = 125 + cpb.CaptionHeight;
			if(!string.IsNullOrEmpty(item.PreviewImageUrl))
				cpb.LoadAsync(item.PreviewImageUrl);
			else
				cpb.Image = Images.Static1080p;
			return cpb;
		}
	}
}
