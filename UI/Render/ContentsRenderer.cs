using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;

namespace au.Applications.MythClient.UI.Render {
	/// <summary>
	/// Capable of rendering the contents panel for all possible levels.
	/// </summary>
	internal class ContentsRenderer : RendererBase, IContentsRenderer {
		/// <summary>
		/// Capable of rendering the contents panel for all recordings
		/// </summary>
		private readonly IContentsRenderer<IShow> _recordings;

		/// <summary>
		/// Capable of rendering the contents panel for a show
		/// </summary>
		private readonly IContentsRenderer<ISeason> _show;

		/// <summary>
		/// Capable of rendering the contents panel for a season
		/// </summary>
		private readonly IContentsRenderer<IEpisode> _season;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="control">Contents panel control</param>
		internal ContentsRenderer(ScrollableControl control)
			: base(control) {
			_recordings = new RecordingsContentsRenderer(control);
			_show = new ShowContentsRenderer(control);
			_season = new SeasonContentsRenderer(control);
		}

		/// <inheritdoc />
		public void Render(IReadOnlyCollection<IShow> items, IShow selectedItem)
			=> _recordings.Render(items, selectedItem);

		/// <inheritdoc />
		public void Render(IReadOnlyCollection<ISeason> items, ISeason selectedItem)
			=> _show.Render(items, selectedItem);

		/// <inheritdoc />
		public void Render(IReadOnlyCollection<IEpisode> items, IEpisode selectedItem)
			=> _season.Render(items, selectedItem);

		/// <inheritdoc />
		public void UpdateSelected(Control selected) {
			foreach(Control item in Control.Controls)
				if(item == selected) {
					item.BackColor = SystemColors.Highlight;
					item.ForeColor = SystemColors.HighlightText;
				} else {
					item.BackColor = Control.BackColor;
					item.ForeColor = Control.ForeColor;
				}
			Control.ScrollControlIntoView(selected);
		}

		/// <inheritdoc />
		public void UpdateSelected(int index)
			=> UpdateSelected(Control.Controls[index]);

		/// <inheritdoc />
		public Control GetSelected() {
			foreach(Control control in Control.Controls)
				if(control.BackColor == SystemColors.Highlight)
					return control;
#if DEBUG
			throw new InvalidOperationException("Could not find a selected control in contents!");
#else
			return null;
#endif
		}

		/// <inheritdoc />
		public int GetSelectedIndex() {
			for(int index = 0; index < Control.Controls.Count; index++)
				if(Control.Controls[index].BackColor == SystemColors.Highlight)
					return index;
#if DEBUG
			throw new InvalidOperationException("Could not find a selected control in contents!");
#else
			return -1;
#endif
		}

		/// <inheritdoc />
		public int GetControlsPerRow() {
			Control firstControl = Control.Controls[0];
			for(int count = 1; count < Control.Controls.Count; count++)
				if(firstControl.Top != Control.Controls[count].Top)
					return count;
			return -1;
		}
	}
}
