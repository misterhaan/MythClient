using au.util.comctl;
using Microsoft.Samples;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace au.Applications.MythClient {
  public partial class MythClient : Form {
    [STAThread]
    static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MythClient());
    }

    private MythSettings _settings;
    private Dictionary<string, DeleteInfo> _deleteInfo;

    public MythClient() {
      InitializeComponent();
    }

    /// <summary>
    /// Get list of recorded programs and the information needed to delete them
    /// from MythWeb, and show them in the ListView.
    /// </summary>
    public void RefreshRecordings() {
      if(!string.IsNullOrEmpty(_settings.RecordedProgramsUrl)) {
        _lvRecorded.BeginUpdate();
        _lvRecorded.Groups.Clear();
        _lvRecorded.Items.Clear();
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(_settings.RecordedProgramsUrl);
        req.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-us,en;q=0.5");  // without this we get spanish or something
        string html = new StreamReader(req.GetResponse().GetResponseStream()).ReadToEnd();
        MatchCollection dispMatches = Regex.Matches(html, "\\/get_pixmap\\/.+?([0-9]+_[0-9]+\\.[^\\.]+)\\..+?class=\"x-title\".+?title=\"Recording Details\">([^<]*)<\\/a>.+?class=\"x-subtitle\".+?title=\"Recording Details\">([^<]*)<\\/a>.+?class=\"x-originalairdate\">([^<]+)<\\/td>.+?class=\"x-airdate\">([^<]+)<\\/td>", RegexOptions.Singleline);
        foreach(Match m in dispMatches) {
          string filename = m.Groups[1].Captures[0].Value;
          string[] items = new string[_lvRecorded.Columns.Count];
          items[_colShow.Index] = m.Groups[2].Captures[0].Value;
          items[_colEpisode.Index] = m.Groups[3].Captures[0].Value;
          items[_colAired.Index] = DateTime.Parse(m.Groups[4].Captures[0].Value).ToShortDateString();
          items[_colRecorded.Index] = DateTime.Parse(m.Groups[5].Captures[0].Value.Replace("(", "").Replace(")", "")).ToString();
          ListViewItem lvi = new ListViewItem(items);
          lvi.Tag = filename;
          _lvRecorded.Items.Add(lvi);
        }
        _lvRecorded.EndUpdate();
        _deleteInfo = new Dictionary<string, DeleteInfo>();
        MatchCollection jsMatches = Regex.Matches(html, "file\\.title[^=]*= '(([^']*\\\\')*[^']*)';.+?file\\.subtitle[^=]*= '(([^']*\\\\')*[^']*)';.+?file\\.chanid[^=]*= '([^']*)';.+?file\\.starttime[^=]*= '([^']*)';.+?file\\.recgroup[^=]*= '([^']*)';.+?file\\.filename[^=]*= '([^']*)';.+?file\\.size[^=]*= '([^']*)';.+?file\\.length[^=]*= '([^']*)';.+?file\\.autoexpire[^=]*= ([0-9]+);", RegexOptions.Singleline | RegexOptions.Multiline);
        foreach(Match m in jsMatches)
          _deleteInfo.Add(Path.GetFileName(m.Groups[8].Captures[0].Value), new DeleteInfo(m));
      }
    }

    /// <summary>
    /// Launch the selected video file in the default application.
    /// </summary>
    private void PlaySelected() {
      Process.Start(Path.Combine(_settings.RawFilesDirectory, (string)_lvRecorded.SelectedItems[0].Tag));
    }

    /// <summary>
    /// Launch the selected video file using a different application.
    /// </summary>
    private void PlaySelectedWith() {
      Process.Start("rundll32.exe", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "shell32.dll") + ",OpenAs_RunDLL " + Path.Combine(_settings.RawFilesDirectory, (string)_lvRecorded.SelectedItems[0].Tag));
    }

    /// <summary>
    /// Export the selected video file with a readable name.
    /// </summary>
    private void ExportSelected() {
      ExportVideos export = new ExportVideos(_settings.Export, _lvRecorded.SelectedItems[0], _colShow.Index, _colEpisode.Index, _colAired.Index);
      if(export.ShowDialog(this) == DialogResult.OK) {
        switch(_settings.Export.How) {
          case ExportSettings.HowType.Show:
            switch(_settings.Export.What) {
              case ExportSettings.WhatType.Episode:
                if(!ExportNonEpisodic(_lvRecorded.SelectedItems[0]))
                  MessageBox.Show(this, "Unable to save selected recording.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
              case ExportSettings.WhatType.Show:
                string match = _lvRecorded.SelectedItems[0].SubItems[_colShow.Index].Text;
                int errors = 0;
                foreach(ListViewItem lvi in _lvRecorded.Items)
                  if(lvi.SubItems[_colShow.Index].Text == match)
                    if(!ExportNonEpisodic(lvi))
                      errors++;
                if(errors > 0)
                  MessageBox.Show(this, "Unable to save " + errors + " recordings.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
              case ExportSettings.WhatType.All:
                errors = 0;
                foreach(ListViewItem lvi in _lvRecorded.Items)
                  if(!ExportNonEpisodic(lvi))
                    errors++;
                if(errors > 0)
                  MessageBox.Show(this, "Unable to save " + errors + " recordings.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
            }
            break;
          case ExportSettings.HowType.ShowDateEpisode:
            switch(_settings.Export.What) {
              case ExportSettings.WhatType.Episode:
                if(!ExportEpisodic(_lvRecorded.SelectedItems[0]))
                  MessageBox.Show(this, "Unable to save selected recording.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
              case ExportSettings.WhatType.Show:
                string match = _lvRecorded.SelectedItems[0].SubItems[_colShow.Index].Text;
                int errors = 0;
                foreach(ListViewItem lvi in _lvRecorded.Items)
                  if(lvi.SubItems[_colShow.Index].Text == match)
                    if(!ExportEpisodic(lvi))
                      errors++;
                if(errors > 0)
                  MessageBox.Show(this, "Unable to save " + errors + " recordings.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
              case ExportSettings.WhatType.All:
                errors = 0;
                foreach(ListViewItem lvi in _lvRecorded.Items)
                  if(!ExportEpisodic(lvi))
                    errors++;
                if(errors > 0)
                  MessageBox.Show(this, "Unable to save " + errors + " recordings.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
            }
            break;
        }
      }
    }

    /// <summary>
    /// Export the specified recording named as the show title.
    /// </summary>
    /// <param name="recording"></param>
    /// <returns></returns>
    private bool ExportNonEpisodic(ListViewItem recording) {
      FileInfo source = new FileInfo(Path.Combine(_settings.RawFilesDirectory, (string)_lvRecorded.SelectedItems[0].Tag));
      try {
        source.CopyTo(Path.Combine(_settings.Export.Where, SanitizeFilename(recording.SubItems[_colShow.Index].Text)) + "." + source.Extension);
      } catch(Exception e) {
        Trace.WriteLine("MythClient.ExportNonEpisodic() ERROR exporting file.  Details:\n" + e);
        return false;
      }
      return true;
    }

    /// <summary>
    /// Export the specified recording named as the show title, airdate, and episode title.
    /// </summary>
    /// <param name="recording"></param>
    /// <returns></returns>
    private bool ExportEpisodic(ListViewItem recording) {
      FileInfo source = new FileInfo(Path.Combine(_settings.RawFilesDirectory, (string)_lvRecorded.SelectedItems[0].Tag));
      try {
        source.CopyTo(Path.Combine(_settings.Export.Where, SanitizeFilename(recording.SubItems[_colShow.Index].Text + " - " + DateTime.Parse(recording.SubItems[_colAired.Index].Text).ToString("yyyyy.MM.dd") + " - " + recording.SubItems[_colEpisode.Index].Text)) + source.Extension);
      } catch(Exception e) {
        Trace.WriteLine("MythClient.ExportNonEpisodic() ERROR exporting file.  Details:\n" + e);
        return false;
      }
      return true;
    }

    private string SanitizeFilename(string dirty) {
      foreach(char c in Path.GetInvalidFileNameChars())
        dirty = dirty.Replace(c.ToString(), "");
      return dirty;
    }

    /// <summary>
    /// Prompt for whether the selected episode should be rerecorded (with
    /// option to cancel), then ask the web server to delete it.
    /// </summary>
    private void DeleteSelected() {
      TaskDialog td = new TaskDialog();
      td.WindowTitle = "Delete Episode";
      td.MainInstruction = "Record this episode again?";
      td.Content = "If this recording was in some way unsatisfactory, MythTV can record it again the next time it comes on.";
      td.CommonButtons = TaskDialogCommonButtons.Cancel;
      td.Buttons = new TaskDialogButton[] { new TaskDialogButton(80, "Delete\nMythTV will not record this episode again."), new TaskDialogButton(81, "Delete + Rerecord\nMythTV will record this episode if it comes on again.") };
      td.DefaultButton = 80;
      td.PositionRelativeToWindow = true;
      td.UseCommandLinks = true;
      int btnid = td.Show(this);
      if(btnid >= 80) {
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(_settings.RecordedProgramsUrl);
        req.Method = "POST";
        req.ContentType = "application/x-www-form-urlencoded";
        DeleteInfo di = _deleteInfo[(string)_lvRecorded.SelectedItems[0].Tag];
        string post = string.Format("ajax=yes&delete=yes&chanid={0}&starttime={1}&forget_old={2}&id={3}&file={4}", di.chanid, di.starttime, btnid == 81 ? "yes" : "", _lvRecorded.SelectedIndices[0], new JavaScriptSerializer().Serialize(di));
        req.ContentLength = post.Length;
        StreamWriter sw = new StreamWriter(req.GetRequestStream());
        sw.Write(post);
        sw.Close();
        StreamReader sr = new StreamReader(req.GetResponse().GetResponseStream());
        string result = sr.ReadToEnd();
        RefreshRecordings();
      }
    }

    #region Event Handlers
    private void MythClient_Load(object sender, EventArgs e) {
      _settings = new MythSettings();
      if(!_settings.Load())
        new MythServerConfig(_settings).ShowDialog(this);
      if(_settings.Display.Size.Width != -42 && _settings.Display.Size.Height != -42)
        Size = _settings.Display.Size;
      if(_settings.Display.Location.X != -42 && _settings.Display.Location.Y != -42)
        Location = _settings.Display.Location;
      else
        CenterToScreen();
      WindowState = _settings.Display.WindowState;
      if(_settings.Display.ColumnHeadings.Count > 0) {
        _lvRecorded.Columns.Clear();
        for(int c = 0; c < _settings.Display.ColumnHeadings.Count; c++) {
          ColumnHeader col = null;
          if(_settings.Display.ColumnHeadings[c] == _colEpisode.Text)
            col = _colEpisode;
          else if(_settings.Display.ColumnHeadings[c] == _colShow.Text)
            col = _colShow;
          else if(_settings.Display.ColumnHeadings[c] == _colAired.Text)
            col = _colAired;
          else if(_settings.Display.ColumnHeadings[c] == _colRecorded.Text)
            col = _colRecorded;
          col.Width = _settings.Display.ColumnWidths[c];
          _lvRecorded.Columns.Add(col);
        }
        _lvRecorded.DetailSort(_colRecorded, SortType.Date);
        _lvRecorded.DetailSort(_colAired, SortType.Date);
        _lvRecorded.DetailSort(_colShow, SortType.Title);
      }
      RefreshRecordings();
    }

    private void MythClient_Resize(object sender, EventArgs e) {
      if(WindowState == FormWindowState.Normal && _settings != null)
        _settings.Display.Size = Size;
    }

    private void MythClient_Move(object sender, EventArgs e) {
      if(WindowState == FormWindowState.Normal && _settings != null)
        _settings.Display.Location = Location;
    }

    private void MythClient_FormClosed(object sender, FormClosedEventArgs e) {
      _settings.Display.WindowState = WindowState;
      ColumnHeader[] orderedColumns = _lvRecorded.GetOrderedColumns();
      _settings.Display.ColumnHeadings.Clear();
      _settings.Display.ColumnWidths.Clear();
      for(int c = 0; c < orderedColumns.Length; c++) {
        _settings.Display.ColumnHeadings.Add(orderedColumns[c].Text);
        _settings.Display.ColumnWidths.Add(orderedColumns[c].Width);
      }
      _settings.Save();
    }

    private void _tsPlay_Click(object sender, EventArgs e) {
      PlaySelected();
    }

    private void _tsPlayWith_Click(object sender, EventArgs e)
    {
      PlaySelectedWith();
    }

    private void _tsExport_Click(object sender, EventArgs e) {
      ExportSelected();
    }

    private void _tsDelete_Click(object sender, EventArgs e) {
      DeleteSelected();
    }

    private void _tsRefresh_Click(object sender, EventArgs e) {
      RefreshRecordings();
    }

    private void _tsSettings_Click(object sender, EventArgs e) {
      new MythServerConfig(_settings).ShowDialog(this);
    }

    private void _lvRecorded_SelectedIndexChanged(object sender, EventArgs e) {
      _cmnuDelete.Enabled = _cmnuExport.Enabled = _cmnuPlayWith.Enabled = _cmnuPlay.Enabled = _tsDelete.Enabled = _tsExport.Enabled = _tsPlayWith.Enabled = _tsPlay.Enabled = _lvRecorded.SelectedItems.Count > 0;
    }

    private void _lvRecorded_DoubleClick(object sender, EventArgs e) {
      if(_lvRecorded.SelectedItems.Count > 0)
        PlaySelected();
    }

    private void _lvRecorded_ColumnClick(object sender, ColumnClickEventArgs e) {
      int col = e.Column;
      SortType sort = SortType.Title;
      if(col == _colAired.Index || col == _colRecorded.Index)
        sort = SortType.Date;
      _lvRecorded.DetailSort(col, sort);
    }

    private void _lvRecorded_KeyDown(object sender, KeyEventArgs e) {
      if(_lvRecorded.SelectedItems.Count > 0)
        if(e.KeyCode == Keys.Enter)
          PlaySelected();
        else if(e.KeyCode == Keys.Delete)
          DeleteSelected();
        else if(e.KeyCode == Keys.F5)
          RefreshRecordings();
    }
    #endregion Event Handlers
  }
}
