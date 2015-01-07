using System;
using System.Windows.Forms;

namespace au.Applications.MythClient {
  public partial class ExportVideos : Form {
    ExportSettings _settings;

    public ExportVideos(ExportSettings settings, ListViewItem selection, int iShow, int iEpisode, int iAirDate) {
      InitializeComponent();
      _settings = settings;
      switch(_settings.What) {
        case ExportSettings.WhatType.Episode:
          _rbEpisode.Checked = true;
          break;
        case ExportSettings.WhatType.Show:
          _rbShow.Checked = true;
          break;
        case ExportSettings.WhatType.All:
          _rbAll.Checked = true;
          break;
      }
      switch(_settings.How) {
        case ExportSettings.HowType.ShowDateEpisode:
          _rbShowDateEpisode.Checked = true;
          break;
        case ExportSettings.HowType.Show:
          _rbShowTitle.Checked = true;
          break;
      }
      if(!string.IsNullOrEmpty(_settings.Where))
        _fnbWhere.FolderFullName = _settings.Where;
      else
        _fnbWhere.FolderFullName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
      _btnOK.Enabled = _fnbWhere.FolderExists;
      string show = selection.SubItems[iShow].Text;
      string episode = selection.SubItems[iEpisode].Text;
      string airdate = selection.SubItems[iAirDate].Text;
      try {
        airdate = DateTime.Parse(airdate).ToString("yyyy.MM.dd");
      } catch { }
      _rbEpisode.Text += episode + " (" + show + ")";
      _rbShow.Text += show;
      _rbShowDateEpisode.Text = show + " - " + airdate + " - " + episode;
      _rbShowTitle.Text = show;
    }

    private void _fnbWhere_Validated(object sender, EventArgs e) {
      _btnOK.Enabled = _fnbWhere.FolderExists;
    }

    private void ExportVideos_Load(object sender, EventArgs e) {
      _fnbWhere.Validate();
    }

    private void _btnOK_Click(object sender, EventArgs e) {
      if(_rbEpisode.Checked)
        _settings.What = ExportSettings.WhatType.Episode;
      else if(_rbShow.Checked)
        _settings.What = ExportSettings.WhatType.Show;
      else if(_rbAll.Checked)
        _settings.What = ExportSettings.WhatType.All;
      if(_rbShowDateEpisode.Checked)
        _settings.How = ExportSettings.HowType.ShowDateEpisode;
      else if(_rbShowTitle.Checked)
        _settings.How = ExportSettings.HowType.Show;
      _settings.Where = _fnbWhere.FolderFullName;
    }
  }
}
