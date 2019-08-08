using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;

namespace au.Applications.MythClient.UI.Render {
	/// <summary>
	/// Common logic for info panel renderers.
	/// </summary>
	/// <typeparam name="TItem">Item type that can be rendered</typeparam>
	internal abstract class InfoRendererBase<TItem> : RendererBase, IInfoRenderer<TItem> {
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="control">Info panel control</param>
		protected InfoRendererBase(ScrollableControl control)
			: base(control) { }

		/// <inheritdoc />
		public void Render(TItem item) {
			Control.Controls.Clear();
			Control.Controls.AddRange(GetControlsToRender(item).ToArray());
		}

		/// <summary>
		/// Get all the controls that should be rendered (in order) to show the item in the info panel.
		/// </summary>
		/// <param name="item">Item being shown in the info panel</param>
		/// <returns>Controls to render in the info panel</returns>
		protected abstract IEnumerable<Control> GetControlsToRender(TItem item);

		/// <summary>
		/// Get controls that should be rendered (in order) to show the next episode in the info panel.
		/// </summary>
		/// <param name="nextEpisode">Episode to render</param>
		/// <param name="inSeason">True if the season context is already obvious</param>
		/// <returns>Controls to render in the info panel</returns>
		protected IEnumerable<Control> GetNextUpControls(IEpisode nextEpisode, bool inSeason) {
			yield return Subtitle(Titles.NextUp, nextEpisode.SubTitle ?? nextEpisode.FirstAired.ToShortDateString());
			yield return Thumbnail(nextEpisode.PreviewImageUrl);
			foreach(Control infoControl in GetEpisodeInfoControls(nextEpisode, inSeason))
				yield return infoControl;

			yield return Action(Icons.Material_Play18, ActionText.PlayCaption,
				inSeason ? ActionText.PlaySeasonTooltip : ActionText.PlayShowTooltip,
				inSeason ? ActionKey.PlaySeasonOldest : ActionKey.PlayShowOldest
			);
			yield return Action(Icons.Material_PlayWith18, ActionText.PlayWithCaption,
				inSeason ? ActionText.PlayWithSeasonTooltip : ActionText.PlayWithShowTooltip,
				inSeason ? ActionKey.PlaySeasonOldestWith : ActionKey.PlayShowOldestWith
			);
			yield return Action(Icons.Material_Delete18, ActionText.DeleteCaption,
				inSeason ? ActionText.DeleteSeasonOldestTooltip : ActionText.DeleteShowOldestTooltip,
				inSeason ? ActionKey.DeleteSeasonOldest : ActionKey.DeleteShowOldest
			);
		}

		/// <summary>
		/// Get the controls (in order) to show information about an episode.
		/// </summary>
		/// <param name="episode">Episode to render</param>
		/// <param name="inSeason">True if the season context is already obvious</param>
		/// <returns>Controls to render in the info panel</returns>
		protected IEnumerable<Control> GetEpisodeInfoControls(IEpisode episode, bool inSeason) {
			if(episode.InProgress) {
				Label stillRecording = Info(InfoText.StillRecording, GetDurationString(episode.DoneRecording - DateTime.Now));
				stillRecording.ForeColor = Color.Red;
				yield return stillRecording;
			}
			if(episode.SeasonNumber > 0)
				yield return inSeason
					? Info(InfoText.EpisodeNumber, episode.Number)
					: Info(InfoText.SeasonNumberEpisodeNumber, episode.SeasonNumber, episode.Number);
			if(episode.Recorded.ToShortDateString() != episode.FirstAired.ToShortDateString())
				yield return Info(InfoText.FirstAired, episode.FirstAired);
			yield return Info(InfoText.Recorded, episode.Recorded);
			yield return Duration(episode.Duration);
		}

		/// <summary>
		/// Create a control to show a title.  This is expected to be the first control.
		/// </summary>
		/// <param name="title">Title text to display</param>
		/// <returns>Control showing the title</returns>
		protected Label Title(string title) {
			Label label = Info(title);
			label.BackColor = SystemColors.Highlight;
			label.Font = new Font(Control.Font.FontFamily, 14F);
			label.ForeColor = SystemColors.HighlightText;
			label.Margin = new Padding(0, 0, 0, 3);
			label.Padding = new Padding(3);
			return label;
		}

		/// <summary>
		/// Create a control to show a title.  This is expected to be the first control.
		/// </summary>
		/// <param name="format">Format string for text to display</param>
		/// <param name="args">Arguments for the format string</param>
		/// <returns>Control showing the title</returns>
		protected Label Title(string format, params object[] args)
			=> Title(string.Format(format, args));

		/// <summary>
		/// Create a control to show a subtitle.
		/// </summary>
		/// <param name="subtitle">Subtitle text to display</param>
		/// <returns>Control showing the subtitle</returns>
		protected Label Subtitle(string subtitle) {
			Label label = Info(subtitle);
			label.Font = new Font(Control.Font.FontFamily, 11F);
			label.ForeColor = SystemColors.Highlight;
			label.Margin = new Padding(0, 12, 0, 0);
			return label;
		}

		/// <summary>
		/// Create a control to show a subtitle.
		/// </summary>
		/// <param name="format">Format string for text to display</param>
		/// <param name="args">Arguments for the format string</param>
		/// <returns>Control showing the subtitle</returns>
		protected Label Subtitle(string format, params object[] args)
			=> Subtitle(string.Format(format, args));

		/// <summary>
		/// Create a control to show a thumbnail image.
		/// </summary>
		/// <param name="imageUrl">URL to the image</param>
		/// <returns>Control showing the image</returns>
		protected PictureBox Thumbnail(string imageUrl) {
			const int thumbWidth = 200;
			const int thumbHeight = 117;
			PictureBox thumb = new PictureBox {
				ErrorImage = Images.Static1080p,
				Height = thumbHeight,
				InitialImage = Images.Static1080p,
				Margin = new Padding((Control.Width - thumbWidth) / 2, 3, 0, 0),
				SizeMode = PictureBoxSizeMode.StretchImage,
				Width = thumbWidth,
			};
			if(!string.IsNullOrEmpty(imageUrl))
				thumb.LoadAsync(imageUrl);
			else
				thumb.Image = Images.Static1080p;
			return thumb;
		}

		/// <summary>
		/// Create a control to show a piece of information.
		/// </summary>
		/// <param name="info">Text to display</param>
		/// <returns>Control showing the piece of information</returns>
		protected Label Info(string info)
			=> new Label {
				AutoSize = true,
				Margin = new Padding(0, 3, 0, 0),
				MaximumSize = new Size(Control.Width, 0),
				MinimumSize = new Size(Control.Width, 0),
				Padding = new Padding(4, 0, 4, 0),
				Text = info,
				UseMnemonic = false,
			};

		/// <summary>
		/// Create a control to show a piece of information.
		/// </summary>
		/// <param name="format">Format string for text to display</param>
		/// <param name="args">Arguments for the format string</param>
		/// <returns>Control showing the piece of information</returns>
		protected Label Info(string format, params object[] args)
			=> Info(string.Format(format, args));

		/// <summary>
		/// Create a control to show a duration.
		/// </summary>
		/// <param name="duration">Duration to show in the control</param>
		/// <returns>Control showing the duration</returns>
		protected Label Duration(TimeSpan duration)
			=> Info(GetDurationString(duration));

		/// <summary>
		/// Turn a duration into a readable hours and / or minutes description.
		/// </summary>
		/// <param name="duration">Duration to format</param>
		/// <returns>Duration in readable format</returns>
		private static string GetDurationString(TimeSpan duration)
			=> duration.TotalHours < 2
				? string.Format(InfoText.DurationMinutes, duration.TotalMinutes)
				: duration.Minutes > 0
					? string.Format(InfoText.DurationHoursAndMinutes, Math.Floor(duration.TotalHours), duration.Minutes)
					: string.Format(InfoText.DurationHours, duration.TotalHours);

		/// <summary>
		/// Create a control to perform an action.
		/// </summary>
		/// <param name="icon">Icon to show with the action</param>
		/// <param name="caption">Caption of the action</param>
		/// <param name="tooltip">Tooltip for the action</param>
		/// <param name="actionKey">Key indicating which action to perform when this control is activated</param>
		/// <returns>Control to perform an action</returns>
		protected Button Action(Bitmap icon, string caption, string tooltip, ActionKey actionKey) {
			const int width = 124;
			int sideMargin = (Control.Width - width) / 2;
			Button action = new Button {
				FlatStyle = FlatStyle.Flat,
				Image = icon,
				ImageAlign = ContentAlignment.MiddleLeft,
				Margin = new Padding(sideMargin, 6, 0, 0),
				Padding = new Padding(2, 2, 2, 3),
				Size = new Size(width, 32),
				TabStop = false,
				Tag = new InfoActionButtonTag(actionKey, tooltip),
				Text = caption,
				TextAlign = ContentAlignment.MiddleLeft,
				TextImageRelation = TextImageRelation.ImageBeforeText,
				UseMnemonic = false,
			};
			action.FlatAppearance.BorderColor = Control.BackColor;
			action.FlatAppearance.MouseDownBackColor = SystemColors.GradientActiveCaption;
			action.FlatAppearance.MouseOverBackColor = SystemColors.GradientActiveCaption;
			action.MouseEnter += ActionButton_MouseEnter;
			action.MouseLeave += ActionButton_MouseLeave;
			return action;
		}

		/// <summary>
		/// Update the border color when the mouse moves over an action button.
		/// </summary>
		/// <param name="sender">Button control the mouse has moved over</param>
		/// <param name="e">Not used</param>
		private static void ActionButton_MouseEnter(object sender, EventArgs e)
			=> ((Button)sender).FlatAppearance.BorderColor = SystemColors.Highlight;

		/// <summary>
		/// Update the border color when the mouse moves off an action button.
		/// </summary>
		/// <param name="sender">Button control the mouse has moved off</param>
		/// <param name="e">Not used</param>
		private void ActionButton_MouseLeave(object sender, EventArgs e)
			=> ((Button)sender).FlatAppearance.BorderColor = Control.BackColor;
	}
}
