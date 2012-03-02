using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace au.Applications.MythClient {
  public partial class MythServerConfig : Form {
    private MythSettings _settings;

    public MythServerConfig(MythSettings settings) {
      InitializeComponent();
      _settings = settings;
      _txtServerName.Text = _settings.ServerName;
      _txtURL.Text = _settings.RecordedProgramsUrl;
      if(Directory.Exists(_settings.RawFilesDirectory))
        _fnbDirectory.FolderFullName = _settings.RawFilesDirectory;
    }

    private void _txtServerName_TextChanged(object sender, EventArgs e) {
      _settings.ServerName = _txtServerName.Text;
      if(string.IsNullOrEmpty(_settings.RecordedProgramsUrlSetting))
        _txtURL.Text = _settings.RecordedProgramsUrl;
    }

    private void _txtURL_TextChanged(object sender, EventArgs e) {
      if(ActiveControl == _txtURL)
        _settings.RecordedProgramsUrlSetting = _txtURL.Text;
      _lnkURL.Visible = !string.IsNullOrEmpty(_txtURL.Text);
    }

    private void _lnkURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
      Process.Start(_txtURL.Text);
    }

    private void _fnbDirectory_Changed(object sender) {
      _settings.RawFilesDirectory = _fnbDirectory.FolderFullName;
    }

    private void _btnOK_Click(object sender, EventArgs e) {
      Close();
    }
  }
}
