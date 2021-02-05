using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;

namespace au.Applications.MythClient.UI.Actions {
	/// <summary>
	/// Handles deletion of recordings.
	/// </summary>
	internal class RecordingsDeleter : IRecordingsDeleter {
		/// <summary>
		/// Window to own dialogs
		/// </summary>
		private readonly IWin32Window _owner;

		/// <summary>
		/// Object for working with recordings
		/// </summary>
		private readonly IRecordings _recordings;

		/// <summary>
		/// Object for rendering recordings
		/// </summary>
		private readonly RecordingsNavigator _navigator;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="owner">Window to own dialogs</param>
		/// <param name="recordings">Object for working with recordings</param>
		/// <param name="navigator">Object for rendering recordings</param>
		internal RecordingsDeleter(IWin32Window owner, IRecordings recordings, RecordingsNavigator navigator) {
			_owner = owner;
			_recordings = recordings;
			_navigator = navigator;
		}

		/// <inheritdoc />
		public async Task DeleteAsync(IEpisode episode) {
			DialogResult rerecord = AskToRerecord(false);
			switch(rerecord) {
				case DialogResult.Yes:
				case DialogResult.No:
					await _recordings.DeleteAsync(episode, rerecord == DialogResult.Yes).ConfigureAwait(true);
					await _navigator.RefreshAsync().ConfigureAwait(false);
					break;
			}
		}

		/// <inheritdoc />
		public async Task DeleteAsync(IShow show) {
			DialogResult rerecord = AskToRerecord(true);
			switch(rerecord) {
				case DialogResult.Yes:
				case DialogResult.No:
					List<Task<bool>> deletions = new List<Task<bool>>();
					foreach(ISeason season in show.Seasons)
						foreach(IEpisode episode in season.Episodes)
							deletions.Add(_recordings.DeleteAsync(episode, rerecord == DialogResult.Yes));
					await Task.WhenAll(deletions).ConfigureAwait(true);
					await _navigator.RefreshAsync().ConfigureAwait(false);
					break;
			}
		}

		/// <summary>
		/// Show the dialog asking whether to record again.
		/// </summary>
		/// <param name="plural">True if more than one recording is being deleted</param>
		/// <returns>User's choice:  Yes to delete and rerecord, No to delete without rerecording, or Cancel to not delete</returns>
		protected virtual DialogResult AskToRerecord(bool plural) {
			TaskDialogCommandLinkButton justDelete = new TaskDialogCommandLinkButton(
				ActionText.DeleteOptionTitle,
				plural ? ActionText.DeleteOptionDescriptionPlural : ActionText.DeleteOptionDescription
			);
			TaskDialogCommandLinkButton deleteAndRerecord = new TaskDialogCommandLinkButton(
				ActionText.DeleteRerecordOptionTitle,
				plural ? ActionText.DeleteRerecordOptionDescriptionPlural : ActionText.DeleteRerecordOptionDescription
			);
			TaskDialogButton response = TaskDialog.ShowDialog(_owner, new TaskDialogPage {
				Caption = plural
					? ActionText.DeleteAllTitle
					: ActionText.DeleteTitle,
				Heading = plural
					? ActionText.DeleteAllInstructions
					: ActionText.DeleteInstructions,
				Text = plural
					? ActionText.DeleteAllNote
					: ActionText.DeleteNote,
				Buttons = {
					justDelete,
					deleteAndRerecord,
					TaskDialogButton.Cancel
				},
				DefaultButton = justDelete
			});

			return response == justDelete
				? DialogResult.No
				: response == deleteAndRerecord
					? DialogResult.Yes
					: response == TaskDialogButton.Cancel
						? DialogResult.Cancel
						: throw new Exception("Unexpected response from TaskDialog");
		}
	}
}
