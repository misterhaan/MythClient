using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;
using au.Applications.MythClient.Settings.Types;
using au.Applications.MythClient.UI.Render;
using au.UI.LatestVersion;

namespace au.Applications.MythClient.UI {
	/// <summary>
	/// Main window of the MythClient application
	/// </summary>
	public partial class RecordingsWindow : Form {
		private static readonly Dictionary<RecordingSortOption, Bitmap> _sortIconMap = new() {
			[RecordingSortOption.OldestRecorded] = Icons.Material_SortDate24,
			[RecordingSortOption.Title] = Icons.Material_SortTitle24
		};

		/// <summary>
		/// Application settings
		/// </summary>
		private readonly IMythSettings _settings;

		/// <summary>
		/// Application updater
		/// </summary>
		private readonly VersionManager _versionManager;

		/// <summary>
		/// Collection of recordings
		/// </summary>
		private readonly IRecordings _recordings;

		/// <summary>
		/// Keeps track of the current navigation state in the recordings window
		/// </summary>
		private readonly RecordingsNavigator _navigator;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="settings">Application settings</param>
		/// <param name="versionManager">Application updater</param>
		/// <param name="recordings">Collection of recordings</param>
		public RecordingsWindow(IMythSettings settings, VersionManager versionManager, IRecordings recordings) {
			InitializeComponent();

			_settings = settings;
			_versionManager = versionManager;
			_recordings = recordings;
			_navigator = new RecordingsNavigator(this, _settings, _recordings, _pnlContents, _pnlInfo);
			_navigator.DepthChanged += _navigator_DepthChanged;
		}

		/// <summary>
		/// Navigate to the previous display level.
		/// </summary>
		private void GoBack()
			=> _navigator.Close();

		/// <summary>
		/// Update the window with the latest recordings.
		/// </summary>
		private async Task RefreshRecordingsAsync() {
			_btnReload.Enabled = false;
			await _navigator.RefreshAsync().ConfigureAwait(true);
			_btnReload.Enabled = true;
		}

		/// <summary>
		/// Change sort settings and refresh recordings.
		/// </summary>
		/// <param name="sortOption">How to sort</param>
		private async Task SortAsync(RecordingSortOption sortOption) {
			if(_settings.RecordingSortOption != sortOption) {
				_settings.RecordingSortOption = sortOption;
				await RefreshRecordingsAsync().ConfigureAwait(true);
				_cmnuSortDate.Checked = sortOption == RecordingSortOption.OldestRecorded;
				_cmnuSortTitle.Checked = sortOption == RecordingSortOption.Title;
				_btnSort.Image = _sortIconMap[sortOption];
			}
		}

		/// <summary>
		/// Launch the settings window and refresh recordings.
		/// </summary>
		private async Task ShowSettingsWindowAsync() {
			using SettingsWindow settingsWindow = new(_settings.Server);
			switch(settingsWindow.ShowDialog(this)) {
				case DialogResult.OK:
				case DialogResult.Yes:
					// assume they changed something and grab the latest recordings
					await RefreshRecordingsAsync().ConfigureAwait(false);
					break;
			}
		}

		private void ShowAboutWindow() {
			using AboutWindow about = new();
			about.ShowDialog(this);
		}

		/// <summary>
		/// Check if the mouse is over the specified control
		/// </summary>
		/// <param name="control">Control to check for mouse hover</param>
		/// <returns>True if the mouse is over the control</returns>
		private static bool IsMouseOver(Control control) {
			Point mousePosition = control.PointToClient(MousePosition);
			return mousePosition.X >= 0 && mousePosition.X <= control.Width
				&& mousePosition.Y >= 0 && mousePosition.Y <= control.Height;
		}

		/// <summary>
		/// Make a toolbar button appear highlighted.
		/// </summary>
		/// <param name="button">Button the mouse is over</param>
		private static void HighlightToolbarButton(Button button) {
			if(button.Enabled)
				button.FlatAppearance.BorderColor = SystemColors.Highlight;
		}

		/// <summary>
		/// Make a toolbar button appear unhighlighted.
		/// </summary>
		/// <param name="button">Button the mouse is no longer over</param>
		private static void UnhighlightToolbarButton(Button button)
			=> button.FlatAppearance.BorderColor = button.Parent.BackColor;

		#region events
		#region form
		private void RecordingsWindow_Load(object sender, EventArgs e) {
			switch(_settings.RecordingSortOption) {
				case RecordingSortOption.OldestRecorded:
					_btnSort.Image = Icons.Material_SortDate24;
					_cmnuSortTitle.Checked = false;
					_cmnuSortDate.Checked = true;
					break;
			}

			if(_settings.MainForm.Size.HasValue)
				Size = _settings.MainForm.Size.Value;
			else
				_settings.MainForm.Size = Size;

			if(_settings.MainForm.Location.HasValue)
				Location = _settings.MainForm.Location.Value;
			else {
				CenterToScreen();
				_settings.MainForm.Location = Location;
			}

			WindowState = _settings.MainForm.WindowState;

			ResizeEnd += new EventHandler(RecordingsWindow_ResizeEnd);
			LocationChanged += new EventHandler(RecordingsWindow_LocationChanged);
		}

		private async void RecordingsWindow_ShownAsync(object sender, EventArgs e)
			=> await Task.WhenAll(RefreshRecordingsAsync(), _versionManager.PromptForUpdate(this)).ConfigureAwait(false);

		private void RecordingsWindow_LocationChanged(object sender, EventArgs e) {
			if(WindowState == FormWindowState.Normal)
				_settings.MainForm.Location = Location;
		}

		private void RecordingsWindow_ResizeEnd(object sender, EventArgs e) {
			if(WindowState == FormWindowState.Normal)
				_settings.MainForm.Size = Size;
		}

		private async void RecordingsWindow_KeyDown(object sender, KeyEventArgs e) {
			switch(e.KeyCode) {
				case Keys.F5: await RefreshRecordingsAsync().ConfigureAwait(false); break;

				case Keys.Enter:
					if(e.Modifiers.HasFlag(Keys.Control))
						_navigator.Play();
					else
						_navigator.Open();
					break;

				case Keys.Back:
				case Keys.BrowserBack:
				case Keys.Escape:
					if(_btnBack.Enabled)
						_navigator.Close();
					break;

				case Keys.Home: _navigator.Navigate(NavigationDirection.First); break;
				case Keys.End: _navigator.Navigate(NavigationDirection.Last); break;
				case Keys.Left: _navigator.Navigate(NavigationDirection.Previous); break;
				case Keys.Right: _navigator.Navigate(NavigationDirection.Next); break;
				case Keys.Up: _navigator.Navigate(NavigationDirection.PreviousRow); break;
				case Keys.Down: _navigator.Navigate(NavigationDirection.NextRow); break;

				case Keys.Delete: await _navigator.DeleteAsync().ConfigureAwait(false); break;
				case Keys.Apps: _navigator.ShowContextMenu(); break;
			}
		}
		#endregion form

		private void _navigator_DepthChanged(object sender, EventArgs e) {
			_btnBack.Enabled = _navigator.Depth != BrowsingDepth.Recordings;
			_lblTitle.Text = _navigator.LocationName;
			_tip.SetToolTip(_btnBack, _navigator.BackDescription);
		}

		#region back button
		private void _btnBack_EnabledChanged(object sender, EventArgs e) {
			_btnBack.Image = _btnBack.Enabled ? Icons.Material_Back24 : Icons.Material_BackDisabled24;
			if(!_btnBack.Enabled)
				UnhighlightToolbarButton(_btnBack);
		}

		private void _btnBack_MouseEnter(object sender, EventArgs e)
			=> HighlightToolbarButton(_btnBack);

		private void _btnBack_Click(object sender, EventArgs e)
			=> GoBack();

		private void _btnBack_MouseLeave(object sender, EventArgs e)
			=> UnhighlightToolbarButton(_btnBack);
		#endregion back button

		#region reload button
		private void _btnReload_EnabledChanged(object sender, EventArgs e) {
			_btnReload.Image = _btnReload.Enabled ? Icons.Material_Reload24 : Icons.Material_ReloadDisabled24;
			if(!_btnReload.Enabled)
				UnhighlightToolbarButton(_btnReload);
			else if(IsMouseOver(_btnReload))
				HighlightToolbarButton(_btnReload);
		}

		private void _btnReload_MouseEnter(object sender, EventArgs e)
			=> HighlightToolbarButton(_btnReload);

		private async void _btnReload_ClickAsync(object sender, EventArgs e)
			=> await RefreshRecordingsAsync().ConfigureAwait(false);

		private void _btnReload_MouseLeave(object sender, EventArgs e)
			=> UnhighlightToolbarButton(_btnReload);
		#endregion reload button

		#region sort menu
		private void _btnSort_MouseEnter(object sender, EventArgs e)
			=> HighlightToolbarButton(_btnSort);

		private void _btnSort_MouseLeave(object sender, EventArgs e)
			=> UnhighlightToolbarButton(_btnSort);

		private void _btnSort_Click(object sender, EventArgs e)
			=> _cmnuSort.Show(_btnSort, _btnSort.Width - _cmnuSort.Width, _btnSort.Height);

		private async void _cmnuSortDate_ClickAsync(object sender, EventArgs e)
			=> await SortAsync(RecordingSortOption.OldestRecorded).ConfigureAwait(false);

		private async void _cmnuSortTitle_ClickAsync(object sender, EventArgs e)
			=> await SortAsync(RecordingSortOption.Title).ConfigureAwait(false);
		#endregion sort menu

		#region main menu
		private void _btnMenu_MouseEnter(object sender, EventArgs e)
			=> HighlightToolbarButton(_btnMenu);

		private void _btnMenu_MouseLeave(object sender, EventArgs e)
			=> UnhighlightToolbarButton(_btnMenu);

		private void _btnMenu_Click(object sender, EventArgs e)
			=> _cmnuMain.Show(_btnMenu, new Point(-_cmnuMain.Width + _btnMenu.Width, _btnMenu.Height));

		private async void _cmnuMainSettings_ClickAsync(object sender, EventArgs e)
			=> await ShowSettingsWindowAsync().ConfigureAwait(false);

		private async void _cmnuMainCheckForUpdate_Click(object sender, EventArgs e)
			=> await _versionManager.PromptForUpdate(this, true).ConfigureAwait(false);

		private void _cmnuMainAbout_Click(object sender, EventArgs e)
			=> ShowAboutWindow();
		#endregion main menu

		private void _pnlInfo_ControlAdded(object sender, ControlEventArgs e) {
			if(e.Control is Button button) {
				button.Click += RemoveFocus;
				if(button.Tag is IInfoActionButtonTag tag)
					_tip.SetToolTip(button, tag.Tooltip);
			}
		}

		private void RemoveFocus(object sender, EventArgs e)
			=> _pnlContents.Focus();
		#endregion events
	}
}
