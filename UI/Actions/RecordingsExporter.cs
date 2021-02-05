using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;
using au.Applications.MythClient.Settings.Types;
using au.IO.Files.FileOperation;

namespace au.Applications.MythClient.UI.Actions {
	/// <summary>
	/// Handles export of recordings.
	/// </summary>
	internal class RecordingsExporter : NeedsRecordingFilesBase, IRecordingsExporter {
		/// <summary>
		/// Window to own dialogs
		/// </summary>
		private readonly IWin32Window _owner;

		/// <summary>
		/// Dialog for choosing the directory to export multiple recordings
		/// </summary>
		private readonly FolderBrowserDialog _chooseExportDirectory;

		/// <summary>
		/// Dialog for exporting a single recording
		/// </summary>
		private readonly SaveFileDialog _chooseExportFile;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="owner">Window to own dialogs</param>
		/// <param name="settings">Application settings</param>
		internal RecordingsExporter(IWin32Window owner, IMythSettings settings)
			: base(settings) {
			_owner = owner;
			_chooseExportDirectory = CreateExportDirectoryDialog(settings.LastExportDirectory);
			_chooseExportFile = CreateExportFileDialog(settings.LastExportDirectory);
		}

		/// <summary>
		/// Create the dialog for choosing the directory to export multiple recordings.
		/// </summary>
		/// <param name="lastExportDirectory">The dialog starts at the directory it used last time</param>
		/// <returns>Dialog for choosing the directory to export multiple recordings</returns>
		private static FolderBrowserDialog CreateExportDirectoryDialog(string lastExportDirectory)
			=> new FolderBrowserDialog {
				Description = ActionText.ChooseExportFolderDescription,
				SelectedPath = lastExportDirectory,
				ShowNewFolderButton = true
			};

		/// <summary>
		/// Create the dialog for exporting a single recording.
		/// </summary>
		/// <param name="lastExportDirectory">The dialog starts at the directory it used last time</param>
		/// <returns>Dialog for exporting a single recording</returns>
		private static SaveFileDialog CreateExportFileDialog(string lastExportDirectory)
			=> new SaveFileDialog {
				InitialDirectory = lastExportDirectory,
				Title = ActionText.ExportTitle
			};

		/// <inheritdoc />
		public void Export(IShow show) {
			if(AskExportDetails(show.OldestEpisode, out string filenameFormat, out string exportPath))
				using(CopyFilesOperation copy = new CopyFilesOperation()) {
					List<string> missingFiles = new List<string>();
					foreach(ISeason season in show.Seasons)
						foreach(IEpisode episode in season.Episodes)
							try {
								QueueExport(copy, episode, filenameFormat, exportPath);
							} catch(FileNotFoundException e) {
								missingFiles.Add(e.FileName);
							}
					if(missingFiles.Any())
						MessageBox.Show(_owner, string.Format(ExceptionMessages.ExportAllFilesNotFound, missingFiles.Count), ActionText.ExportAllTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
		}

		/// <inheritdoc />
		public void Export(ISeason season) {
			if(AskExportDetails(season.OldestEpisode, out string filenameFormat, out string exportPath))
				using(CopyFilesOperation copy = new CopyFilesOperation()) {
					List<string> missingFiles = new List<string>();
					foreach(IEpisode episode in season.Episodes)
						try {
							QueueExport(copy, episode, filenameFormat, exportPath);
						} catch(FileNotFoundException e) {
							missingFiles.Add(e.FileName);
						}
					if(missingFiles.Any())
						MessageBox.Show(_owner, string.Format(ExceptionMessages.ExportAllFilesNotFound, missingFiles.Count), ActionText.ExportAllTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
		}

		/// <inheritdoc />
		public void Export(IEpisode episode) {
			string filenameFormat = AskExportFilenameFormat(episode, false);
			if(!string.IsNullOrEmpty(filenameFormat)) {
				try {
					FileInfo rawFile = GetRecordingFile(episode);
					_chooseExportFile.DefaultExt = rawFile.Extension;
					_chooseExportFile.Filter = ActionText.ExportFiletype + "|*" + rawFile.Extension;
					_chooseExportFile.FileName = FormatEpisode(filenameFormat, episode);
					switch(_chooseExportFile.ShowDialog(_owner)) {
						case DialogResult.OK:
						case DialogResult.Yes:
							using(CopyFilesOperation copy = new CopyFilesOperation())
								copy.Queue(rawFile, new FileInfo(_chooseExportFile.FileName));
							break;
					}
				} catch(FileNotFoundException e) {
					MessageBox.Show(_owner, e.Message, ActionText.ExportTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
			}
		}

		/// <summary>
		/// Prompt for details about how to export episodes.
		/// </summary>
		/// <param name="exampleEpisode">An episode from the export used as an example to show the formats</param>
		/// <param name="filenameFormat">Chosen filename format string</param>
		/// <param name="exportPath">Chosen export directory</param>
		/// <returns>True if both a valid filename fomat and existing export directory have been chosen</returns>
		private bool AskExportDetails(IEpisode exampleEpisode, out string filenameFormat, out string exportPath) {
			exportPath = null;
			filenameFormat = AskExportFilenameFormat(exampleEpisode, true);
			if(string.IsNullOrEmpty(filenameFormat))
				return false;
			switch(_chooseExportDirectory.ShowDialog(_owner)) {
				case DialogResult.OK:
				case DialogResult.Yes:
					exportPath = _chooseExportFile.InitialDirectory = _settings.LastExportDirectory = _chooseExportDirectory.SelectedPath;
					return true;
				case DialogResult.Cancel:
				case DialogResult.Abort:
				default:
					return false;
			}
		}

		/// <summary>
		/// Prompt the user to choose a filename format for export.
		/// </summary>
		/// <param name="exampleEpisode">An episode from the export used as an example to show the formats</param>
		/// <param name="plural">True if exporting more than one episode</param>
		/// <returns>Chosen filename format, or null if canceled</returns>
		private string AskExportFilenameFormat(IEpisode exampleEpisode, bool plural) {
			return TaskDialog.ShowDialog(_owner, new TaskDialogPage {
				Caption = plural
					? ActionText.ExportAllTitle
					: ActionText.ExportTitle,
				Heading = ActionText.ExportInstructions,
				Text = ActionText.ExportNote,
				Buttons = GetFilenameFormatButtons(exampleEpisode)
			}).Tag as string;
		}

		/// <summary>
		/// Get export filename format options based on an example episode.
		/// </summary>
		/// <param name="exampleEpisode">Example episode used to decide which options apply and to show a real example of each format</param>
		/// <returns>Reasonable options for export filename</returns>
		private static TaskDialogButtonCollection GetFilenameFormatButtons(IEpisode exampleEpisode) {
			TaskDialogButtonCollection buttons = new TaskDialogButtonCollection();
			if(!string.IsNullOrEmpty(exampleEpisode.SubTitle))
				AddEpisodicFilenameFormatOptions(buttons, exampleEpisode);
			else
				AddNonEpisodicFilenameFormatOptions(buttons, exampleEpisode);
			buttons.Add(TaskDialogButton.Cancel);
			return buttons;
		}

		/// <summary>
		/// Add export filename format options for episodic recordings based on an example episode to the button collection.
		/// </summary>
		/// <param name="buttons">Button collection options will be added to</param>
		/// <param name="exampleEpisode">Example episode used to decide which options apply and to show a real example of each format</param>
		private static void AddEpisodicFilenameFormatOptions(TaskDialogButtonCollection buttons, IEpisode exampleEpisode) {
			if(exampleEpisode.SeasonNumber > 0 && exampleEpisode.Number > 0)
				buttons.Add(new TaskDialogCommandLinkButton(ActionText.ExportOptionSeasonEpisode, FormatEpisode(ActionText.ExportFormatSeasonEpisode, exampleEpisode)) {
					Tag = ActionText.ExportFormatSeasonEpisode
				});
			buttons.Add(new TaskDialogCommandLinkButton(ActionText.ExportOptionDateEpisode, FormatEpisode(ActionText.ExportFormatDateEpisode, exampleEpisode)) {
				Tag = ActionText.ExportFormatDateEpisode
			});
			buttons.Add(new TaskDialogCommandLinkButton(ActionText.ExportOptionEpisode, FormatEpisode(ActionText.ExportFormatEpisode, exampleEpisode)) {
				Tag = ActionText.ExportFormatEpisode
			});
		}

		/// <summary>
		/// Add export filename format options for non-episodic recordings based on an example episode to the button collection.
		/// </summary>
		/// <param name="buttons">Button collection options will be added to</param>
		/// <param name="exampleEpisode">Example episode used to decide which options apply and to show a real example of each format</param>
		private static void AddNonEpisodicFilenameFormatOptions(TaskDialogButtonCollection buttons, IEpisode exampleEpisode) {
			buttons.Add(new TaskDialogCommandLinkButton(ActionText.ExportOptionYear, FormatEpisode(ActionText.ExportFormatYear, exampleEpisode)) {
				Tag = ActionText.ExportFormatYear
			});
			buttons.Add(new TaskDialogCommandLinkButton(ActionText.ExportOptionDate, FormatEpisode(ActionText.ExportFormatDate, exampleEpisode)) {
				Tag = ActionText.ExportFormatDate
			});
			buttons.Add(new TaskDialogCommandLinkButton(ActionText.ExportOptionTitle, FormatEpisode(ActionText.ExportFormatTitle, exampleEpisode)) {
				Tag = ActionText.ExportFormatTitle
			});
		}

		/// <summary>
		/// Add an episode to the export queue.
		/// </summary>
		/// <param name="copy">Copy operation the episode should be queued on</param>
		/// <param name="episode">Episode to add to the export queue</param>
		/// <param name="filenameFormat">Chosen export filename format</param>
		/// <param name="exportPath">Chosen export path</param>
		private void QueueExport(CopyFilesOperation copy, IEpisode episode, string filenameFormat, string exportPath) {
			FileInfo rawFile = GetRecordingFile(episode);
			FileInfo exportFile = new FileInfo(Path.Combine(exportPath, FormatEpisode(filenameFormat, episode)) + rawFile.Extension);
			copy.Queue(rawFile, exportFile);
		}

		/// <summary>
		/// Apply a filename format to an episode.
		/// </summary>
		/// <param name="format">Chosen filename format</param>
		/// <param name="episode">Episode to format</param>
		/// <returns>Filename (without extension) for the episode</returns>
		private static string FormatEpisode(string format, IEpisode episode)
			=> SanitizeFilename(string.Format(format, episode.ShowTitle, episode.SubTitle, episode.FirstAired, episode.SeasonNumber, episode.Number));

		/// <summary>
		/// Strip disallowed characters from a string so it can be used as a filename.
		/// </summary>
		/// <param name="dirty">String which needs to be sanitized</param>
		/// <returns>String with disallowed characters removed</returns>
		private static string SanitizeFilename(string dirty) {
			foreach(char c in Path.GetInvalidFileNameChars())
				dirty = dirty.Replace(c.ToString(), "");
			return dirty;
		}
	}
}
