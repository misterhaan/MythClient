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
      _tip.SetToolTip(_txtServerName, _tip.GetToolTip(_lblServerName));
      _txtServerName.Text = _settings.ServerName;
      _tip.SetToolTip(_numPort, _tip.GetToolTip(_lblPort));
      _numPort.Value = _settings.ServerPort;
      _tip.SetToolTip(_fnbDirectory, _tip.GetToolTip(_lblDirectory));
      if(Directory.Exists(_settings.RawFilesDirectory))
        _fnbDirectory.FolderFullName = _settings.RawFilesDirectory;
    }

    private void _txtServerName_TextChanged(object sender, EventArgs e) {
      _settings.ServerName = _txtServerName.Text;
    }

    private void _fnbDirectory_Changed(object sender) {
      _settings.RawFilesDirectory = _fnbDirectory.FolderFullName;
    }

    private void _btnOK_Click(object sender, EventArgs e) {
      Close();
    }

    private void _numPort_ValueChanged(object sender, EventArgs e) {
      _settings.ServerPort = (int)_numPort.Value;
    }

    private void _lnkTest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
      Process.Start(string.Format("http://{0}:{1}/", _settings.ServerName, _settings.ServerPort));
    }
  }
}
