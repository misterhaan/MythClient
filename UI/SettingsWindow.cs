using System;
using System.Diagnostics;
using System.Windows.Forms;
using au.Applications.MythClient.Settings;
using au.Applications.MythClient.Settings.Types;

namespace au.Applications.MythClient.UI {
	/// <summary>
	/// Window for editing settings for connecting to the MythTV server
	/// </summary>
	public partial class SettingsWindow : Form {
		/// <summary>
		/// Settings being edited
		/// </summary>
		private readonly IServerSettings _serverSettings;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="serverSettings">Settings to edit</param>
		public SettingsWindow(IServerSettings serverSettings) {
			InitializeComponent();

			_serverSettings = serverSettings;
		}

		/// <summary>
		/// Update fields to display the current settings.
		/// </summary>
		private void ShowCurrentSettings() {
			_txtServerName.Text = _serverSettings.Name;
			_numPort.Value = _serverSettings.Port;
			_dirRecordings.TrySetDirectory(_serverSettings.RawFilesDirectory, out _);
		}

		/// <summary>
		/// Launch the base URL to the MythTV API.
		/// </summary>
		private void LaunchMythServerApi() {
			ushort port = (ushort)_numPort.Value;
			if(port > 0)
				port = ServerSettings.DefaultPort;
			UriBuilder mythServerApiUrl = new UriBuilder(Uri.UriSchemeHttp, _txtServerName.Text.Trim(), port);
			Process.Start(mythServerApiUrl.Uri.ToString());
		}

		/// <summary>
		/// Save the field values into the settings.
		/// </summary>
		private void SaveSettings() {
			_serverSettings.Name = _txtServerName.Text.Trim();
			ushort port = (ushort)_numPort.Value;
			if(port == 0)
				port = ServerSettings.DefaultPort;
			_serverSettings.Port = port;
			_serverSettings.RawFilesDirectory = _dirRecordings.Text;
		}

		#region event handlers
		private void SettingsWindow_Shown(object sender, EventArgs e)
			=> ShowCurrentSettings();

		private void _btnOK_Click(object sender, EventArgs e)
			=> SaveSettings();

		private void _lnkTest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
			=> LaunchMythServerApi();
		#endregion event handlers
	}
}
