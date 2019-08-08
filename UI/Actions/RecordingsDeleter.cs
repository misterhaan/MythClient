using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;
using au.UI.TaskDialog;

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

		/// <summary>
		/// Delete a single recording.
		/// </summary>
		/// <param name="episode">Episode to delete</param>
		public async Task DeleteAsync(IEpisode episode) {
			DialogResult rerecord = AskToRerecord(false);
			switch(rerecord) {
				case DialogResult.Yes:
				case DialogResult.No:
					await _recordings.DeleteAsync(episode, rerecord == DialogResult.Yes);
					await _recordings.LoadAsync();
					_navigator.Render();
					break;
			}
		}

		/// <summary>
		/// Delete all recordings of a show.
		/// </summary>
		/// <param name="show">Show to delete</param>
		public async Task DeleteAsync(IShow show) {
			DialogResult rerecord = AskToRerecord(true);
			switch(rerecord) {
				case DialogResult.Yes:
				case DialogResult.No:
					List<Task<bool>> deletions = new List<Task<bool>>();
					foreach(ISeason season in show.Seasons)
						foreach(IEpisode episode in season.Episodes)
							deletions.Add(_recordings.DeleteAsync(episode, rerecord == DialogResult.Yes));
					await Task.WhenAll(deletions);
					await _recordings.LoadAsync();
					_navigator.Render();
					break;
			}
		}

		/// <summary>
		/// Show the dialog asking whether to record again.
		/// </summary>
		/// <param name="plural">True if more than one recording is being deleted</param>
		/// <returns>User's choice:  Yes to delete and rerecord, No to delete without rerecording, or Cancel to not delete</returns>
		protected virtual DialogResult AskToRerecord(bool plural) {
			TaskDialog dialog = new TaskDialog {
				WindowTitle = plural
					? ActionText.DeleteAllTitle
					: ActionText.DeleteTitle,
				MainInstruction = plural
					? ActionText.DeleteAllInstructions
					: ActionText.DeleteInstructions,
				Content = plural
					? ActionText.DeleteAllNote
					: ActionText.DeleteNote,
				CommonButtons = TaskDialogCommonButtons.Cancel,
				Buttons = new TaskDialogButton[] {
					new TaskDialogButton((int)DialogResult.No, plural ? ActionText.DeleteAllOption : ActionText.DeleteOption),
					new TaskDialogButton((int)DialogResult.Yes, plural ? ActionText.DeleteAllRerecordOption : ActionText.DeleteRerecordOption)
				},
				DefaultButton = (int)DialogResult.No,
				PositionRelativeToWindow = true,
				UseCommandLinks = true
			};
			return (DialogResult)dialog.Show(_owner);
		}
	}
}
