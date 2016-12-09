using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using au.Applications.MythClient.Data;
using au.util.comctl;
using Microsoft.Samples;

namespace au.Applications.MythClient {
  public partial class MythClient : Form {
    [STAThread]
    static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MythClient());
    }

    private MythSettings _settings;
    private MythRecordings _recordings;
    private Control _selected = null;

    public MythClient() {
      InitializeComponent();
    }

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
      RefreshRecordings();
    }

    /// <summary>
    /// Get list of recorded programs and the information needed to delete them
    /// from MythWeb, and show them in the ListView.
    /// </summary>
    public void RefreshRecordings() {
      if(!string.IsNullOrEmpty(_settings.ServerName)) {
        _recordings = new MythRecordings(_settings.ServerName, _settings.ServerPort);
        if(_selected != null) {
          if(_selected.Tag is Show)
            ListShows(((Show)_selected.Tag).Title);
          else if(_selected.Tag is Season) {
            Season season = (Season)_selected.Tag;
            Show show = _recordings.Shows.Where(s => s.Title == season.Show.Title).FirstOrDefault();
            if(show != null)
              ListSeasons(show, season.Number);
            else
              ListShows();
          } else if(_selected.Tag is Episode) {
            Episode episode = (Episode)_selected.Tag;
            Show show = _recordings.Shows.Where(s => s.Title == episode.Season.Show.Title).FirstOrDefault();
            if(show != null) {
              Season season = show.Seasons.Where(s => s.Number == episode.Season.Number).FirstOrDefault();
              if(season != null)
                ListEpisodes(season, episode.Name);
              else
                ListSeasons(show);
            } else
              ListShows();
          }
        } else
          ListShows();
      }
    }

    /// <summary>
    /// Go back to the previous page.
    /// </summary>
    private void Back() {
      if(_pnlMain.Tag is Season) {
        Season s = (Season)_pnlMain.Tag;
        if(s.Show.Seasons.Count > 1)
          ListSeasons(s.Show, s.Number);
        else
          ListShows(s.Show.Title);
      } else
        ListShows(((Show)_pnlMain.Tag).Title);
    }

    /// <summary>
    /// List all the recorded shows in the main panel.  Select the first one.
    /// </summary>
    private void ListShows() { ListShows(null); }

    /// <summary>
    /// List all the recorded shows in the main panel.  Select the one that
    /// matches selectTitle.
    /// </summary>
    /// <param name="selectTitle">Show title to select</param>
    private void ListShows(string selectTitle) {
      _pnlMain.Controls.Clear();
      _pnlMain.Tag = _recordings;
      _pbBack.Enabled = false;
      _lblTitle.Text = Properties.Resources.RecordedShows;
      bool selectedSomething = false;
      foreach(Show s in _recordings.Shows) {
        PictureBox pb = new PictureBox();
        pb.Tag = s;
        pb.Image = s.Cover;
        pb.Width = 212;
        pb.Height = 301;
        pb.SizeMode = PictureBoxSizeMode.StretchImage;
        pb.Padding = new Padding(6);
        pb.Margin = new Padding(5);
        pb.Click += mainItem_Click;
        pb.DoubleClick += pbShow_DoubleClick;
        pb.ContextMenuStrip = _cmnuShow;
        _pnlMain.Controls.Add(pb);
        if(s.Title == selectTitle) {
          Select(pb);
          selectedSomething = true;
        }
      }
      if(!selectedSomething && _pnlMain.Controls.Count > 0)
        Select(_pnlMain.Controls[0]);
    }

    /// <summary>
    /// List all recorded seasons for the specified show.  Shows with only one
    /// recorded season will jump right into that season, otherwise the first
    /// season is selected.
    /// </summary>
    /// <param name="show">Show to list seasons for</param>
    private void ListSeasons(Show show) { ListSeasons(show, -1); }

    /// <summary>
    /// List all recorded seasons for the specified show.  Shows with only one
    /// recorded season will jump right into that season, otherwise the season
    /// matching selectNumber is selected.
    /// </summary>
    /// <param name="show">Show to list seasons for</param>
    /// <param name="selectNumber">Select the season with this number</param>
    private void ListSeasons(Show show, int selectNumber) {
      if(show.Seasons.Count == 1)
        ListEpisodes(show.Seasons[0]);
      else {
        _pnlMain.Controls.Clear();
        _pnlMain.Tag = show;
        _tip.SetToolTip(_pbBack, string.Format(Properties.Resources.BackTo, Properties.Resources.RecordedShows));
        _pbBack.Enabled = true;
        _lblTitle.Text = string.Format(Properties.Resources.RecordedSeasons, show.Title);
        bool selectedSomething = false;
        foreach(Season s in show.Seasons) {
          PictureBox pb = new PictureBox();
          pb.Tag = s;
          pb.Image = s.Cover != null ? s.Cover : Properties.Resources.Static1080p;
          pb.Width = 212;
          pb.Height = 301;
          pb.SizeMode = PictureBoxSizeMode.StretchImage;
          pb.Padding = new Padding(6);
          pb.Margin = new Padding(5);
          pb.Click += mainItem_Click;
          pb.DoubleClick += pbSeason_DoubleClick;
          pb.ContextMenuStrip = _cmnuSeason;
          _pnlMain.Controls.Add(pb);
          if(s.Number == selectNumber) {
            Select(pb);
            selectedSomething = true;
          }
        }
        if(!selectedSomething && _pnlMain.Controls.Count > 0)
          Select(_pnlMain.Controls[0]);
      }
    }

    /// <summary>
    /// List all recorded episodes for the specified season.
    /// </summary>
    /// <param name="season">Season to list episodes for</param>
    private void ListEpisodes(Season season) { ListEpisodes(season, null); }

    /// <summary>
    /// List all recorded episodes for the specified season.
    /// </summary>
    /// <param name="season">Season to list episodes for</param>
    /// <param name="selectName">Episode name to select</param>
    private void ListEpisodes(Season season, string selectName) {
      _pnlMain.Controls.Clear();
      _pnlMain.Tag = season;
      _tip.SetToolTip(_pbBack, string.Format(Properties.Resources.BackTo, string.Format(Properties.Resources.RecordedSeasons, season.Show.Title)));
      _pbBack.Enabled = true;
      _lblTitle.Text = season.Number > 0 ? string.Format(Properties.Resources.RecordedSeasonEpisodes, season.Show.Title, season.Number) : string.Format(Properties.Resources.RecordedEpisodes, season.Show.Title);
      bool selectedSomething = false;
      foreach(Episode e in season.Episodes) {
        CaptionedPictureBox cpb = new CaptionedPictureBox();
        cpb.Tag = e;
        cpb.Image = e.Thumb != null ? e.Thumb : Properties.Resources.Static1080p;
        cpb.Text = e.Name;
        cpb.Width = 212;
        cpb.Height = 125 + cpb.CaptionHeight;
        cpb.SizeMode = PictureBoxSizeMode.StretchImage;
        cpb.Padding = new Padding(6);
        cpb.Margin = new Padding(5);
        cpb.Click += mainItem_Click;
        cpb.DoubleClick += pbEpisode_DoubleClick;
        cpb.ContextMenuStrip = _cmnuEpisode;
        _pnlMain.Controls.Add(cpb);
        if(e.Name == selectName) {
          Select(cpb);
          selectedSomething = true;
        }
      }
      if(!selectedSomething && _pnlMain.Controls.Count > 0)
        Select(_pnlMain.Controls[0]);
    }

    /// <summary>
    /// Select a control that represents a show, season, or episode and display
    /// its information in the info pane.
    /// </summary>
    /// <param name="c">Control to select</param>
    private void Select(Control c) {
      if(_selected != null && _selected != c) {
        _selected.BackColor = Color.Transparent;
        _selected.ForeColor = _pnlMain.ForeColor;
      }
      _selected = c;
      if(c != null) {
        c.BackColor = SystemColors.Highlight;
        c.ForeColor = SystemColors.HighlightText;
        ShowInfo(c.Tag);
        _pnlMain.ScrollControlIntoView(c);
      } else
        HideInfo();
    }

    /// <summary>
    /// Hide the info panel, usually because there's nothing selected.
    /// </summary>
    private void HideInfo() {
      _pnlInfo.Controls.Clear();
      _pnlInfo.Tag = null;
    }

    /// <summary>
    /// Show the info panel with information for the specified Show, Season, or Episode.
    /// </summary>
    /// <param name="info"></param>
    private void ShowInfo(object info) {
      if(info is Show)
        ShowShowInfo((Show)info);
      else if(info is Season)
        ShowSeasonInfo((Season)info);
      else if(info is Episode)
        ShowEpisodeInfo((Episode)info);
    }

    /// <summary>
    /// Show the info panel with information for the specified Show.
    /// </summary>
    /// <param name="s">Show to display</param>
    private void ShowShowInfo(Show s) {
      _pnlInfo.Controls.Clear();
      _pnlInfo.Tag = s;
      _pnlInfo.Controls.Add(MakeInfoTitleLabel(s.Title));
      _pnlInfo.Controls.Add(MakeInfoLabel(string.Format(Properties.Resources.InfoNumEpisodes, s.NumEpisodes)));
      if(s.Seasons.Count > 1)
        _pnlInfo.Controls.Add(MakeInfoLabel(string.Format(Properties.Resources.InfoNumSeasons, s.Seasons.Count)));
      _pnlInfo.Controls.Add(MakeInfoLabel(string.Format(Properties.Resources.InfoRecordedRange, s.OldestEpisode.Recorded, s.NewestEpisode.Recorded)));
      _pnlInfo.Controls.Add(MakeInfoLabel(s.Duration.ToStringUnit()));
      _pnlInfo.Controls.Add(MakeInfoAction(Properties.Resources.Play, "Play oldest", "Play the oldest recorded episode of this show", btnShowPlay_Click));
      _pnlInfo.Controls.Add(MakeInfoAction(Properties.Resources.Delete, "Delete oldest", "Delete the oldest recorded episode of this show", btnShowDelete_Click));
    }

    /// <summary>
    /// Show the info panel with information for the specified Season.
    /// </summary>
    /// <param name="s">Season to display</param>
    private void ShowSeasonInfo(Season s) {
      _pnlInfo.Controls.Clear();
      _pnlInfo.Tag = s;
      // assume we'll only show season info if it's a valid season (this is not true because sometimes season info is missing)
      _pnlInfo.Controls.Add(MakeInfoTitleLabel(string.Format(Properties.Resources.InfoSeasonTitle, s.Show.Title, s.Number)));
      _pnlInfo.Controls.Add(MakeInfoLabel(string.Format(Properties.Resources.InfoNumEpisodes, s.Episodes.Count)));
      _pnlInfo.Controls.Add(MakeInfoLabel(string.Format(Properties.Resources.InfoRecordedRange, s.OldestEpisode.Recorded, s.NewestEpisode.Recorded)));
      _pnlInfo.Controls.Add(MakeInfoLabel(s.Duration.ToStringUnit()));
      _pnlInfo.Controls.Add(MakeInfoAction(Properties.Resources.Play, "Play oldest", "Play the oldest recorded episode of this season", btnSeasonPlay_Click));
      _pnlInfo.Controls.Add(MakeInfoAction(Properties.Resources.Delete, "Delete oldest", "Delete the oldest recorded episode of this season", btnSeasonDelete_Click));
    }

    /// <summary>
    /// Show the info panel with information for the specified Episode.
    /// </summary>
    /// <param name="e">Episode to display</param>
    private void ShowEpisodeInfo(Episode e) {
      _pnlInfo.Controls.Clear();
      _pnlInfo.Tag = e;
      string title = string.Format(Properties.Resources.InfoEpisodeTitle, e.Season.Show.Title, e.Name);
      _pnlInfo.Controls.Add(MakeInfoTitleLabel(title));
      if(e.InProgress) {
        Label lbl = MakeInfoLabel(string.Format(Properties.Resources.InfoEpisodeStillRecording, (e.DoneRecording - DateTime.Now).ToStringUnit()));
        lbl.ForeColor = Color.Red;
        _pnlInfo.Controls.Add(lbl);
      }
      if(e.Season.Number > 0)
        _pnlInfo.Controls.Add(MakeInfoLabel(string.Format(Properties.Resources.InfoEpisodeSeasonEpisode, e.Season.Number, e.Number)));
      if(e.FirstAired != e.Recorded)
        _pnlInfo.Controls.Add(MakeInfoLabel(string.Format(Properties.Resources.InfoEpisodeFirstAired, e.FirstAired)));
      _pnlInfo.Controls.Add(MakeInfoLabel(string.Format(Properties.Resources.InfoEpisodeRecorded, e.Recorded)));
      _pnlInfo.Controls.Add(MakeInfoLabel(e.Duration.ToStringUnit()));
      _pnlInfo.Controls.Add(MakeInfoAction(Properties.Resources.Play, "Play", string.Format("Play {0} with the default video player", title), btnEpisodePlay_Click));
      _pnlInfo.Controls.Add(MakeInfoAction(Properties.Resources.PlayWith, "Play with...", string.Format("Choose an alternate player to play {0}", title), btnEpisodePlayWith_Click));
      _pnlInfo.Controls.Add(MakeInfoAction(Properties.Resources.Export, "Export...", string.Format("Export {0} with a readable filename", title), btnEpisodeExport_Click));
      _pnlInfo.Controls.Add(MakeInfoAction(Properties.Resources.Delete, "Delete", string.Format("Delete {0} from MythTV", title), btnEpisodeDelete_Click));
    }

    /// <summary>
    /// Create a new Label to act as the title of the info panel.
    /// </summary>
    /// <param name="text">Text to display on the label</param>
    /// <returns>Label ready to add to the info panel</returns>
    private Label MakeInfoTitleLabel(string text) {
      Label lbl = MakeInfoLabel(text);
      lbl.Padding = new Padding(1, 3, 1, 3);
      lbl.Font = new Font(_pnlInfo.Font.FontFamily, 14F);
      lbl.BackColor = SystemColors.Highlight;
      lbl.ForeColor = SystemColors.HighlightText;
      return lbl;
    }

    /// <summary>
    /// Create a new Label for adding to the info panel.
    /// </summary>
    /// <param name="text">Text to display on the label</param>
    /// <returns>Label ready to add to the info panel</returns>
    private Label MakeInfoLabel(string text) {
      Label lbl = new Label();
      lbl.UseMnemonic = false;
      lbl.Text = text;
      lbl.AutoSize = true;
      lbl.MinimumSize = lbl.MaximumSize = new Size(_pnlInfo.Width, 0);
      lbl.Padding = new Padding(2, 0, 2, 0);
      if(_pnlInfo.Controls.Count > 0) {
        Control last = _pnlInfo.Controls[_pnlInfo.Controls.Count - 1];
        lbl.Top = last.Top + last.Height + 3;
      }
      return lbl;
    }

    /// <summary>
    /// Create a new Button for taking actions from the info panel.
    /// </summary>
    /// <param name="icon">Icon to display on the button</param>
    /// <param name="text">Text to display on the button</param>
    /// <param name="tooltip">Tooltip text for the button</param>
    /// <param name="clickAction">Function to call when the button is clicked</param>
    /// <returns>Button ready to add to the info panel</returns>
    private Control MakeInfoAction(Image icon, string text, string tooltip, EventHandler clickAction) {
      Button b = new Button();
      b.UseMnemonic = false;
      b.FlatAppearance.BorderColor = SystemColors.ControlDark;
      b.FlatAppearance.MouseDownBackColor = SystemColors.Highlight;
      b.FlatAppearance.MouseOverBackColor = SystemColors.Highlight;
      b.FlatStyle = FlatStyle.Flat;
      b.TabStop = false;
      b.Image = icon;
      b.ImageAlign = ContentAlignment.MiddleLeft;
      b.Text = text;
      b.TextAlign = ContentAlignment.MiddleLeft;
      b.TextImageRelation = TextImageRelation.ImageBeforeText;
      b.Padding = new Padding(2, 2, 2, 3);
      b.Size = new Size(124, 32);
      if(_pnlInfo.Controls.Count > 0) {
        Control last = _pnlInfo.Controls[_pnlInfo.Controls.Count - 1];
        b.Top = last.Top + last.Height + 12;
      }
      b.Left = (_pnlInfo.Width - b.Width) / 2;
      b.MouseEnter += ActionButton_MouseEnter;
      b.MouseLeave += ActionButton_MouseLeave;
      b.Click += clickAction;
      _tip.SetToolTip(b, tooltip);
      return b;
    }

    /// <summary>
    /// Play the specified Episode using the default player.
    /// </summary>
    /// <param name="e">Episode to play</param>
    private void PlayEpisode(Episode e) {
      Process.Start(Path.Combine(_settings.RawFilesDirectory, e.Filename));
    }

    /// <summary>
    /// Play the specified Episode using a player selected from the Open With dialog.
    /// </summary>
    /// <param name="e">Episode to play</param>
    private void PlayEpisodeWith(Episode e) {
      Process.Start("rundll32.exe", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "shell32.dll") + ",OpenAs_RunDLL " + Path.Combine(_settings.RawFilesDirectory, e.Filename));
    }

    /// <summary>
    /// Export the specified Episode with a readable filename.
    /// </summary>
    /// <param name="e">Episode to export</param>
    private void ExportEpisode(Episode e) {
      // TODO:  rewrite / adapt export
      MessageBox.Show("Not implemented");
    }

    /// <summary>
    /// Delete the specified Episode.
    /// </summary>
    /// <param name="e">Episode to delete</param>
    private void DeleteEpisode(Episode e) {
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
      if(btnid >= 80)
        if(e.Delete(btnid == 81))
          if(_selected != null)
            if(_selected.Tag is Episode)
              if(_pnlMain.Controls.Count > 1) {
                int pos = _pnlMain.Controls.IndexOf(_selected);
                _pnlMain.Controls.Remove(_selected);
                if(pos < _pnlMain.Controls.Count)
                  Select(_pnlMain.Controls[pos]);
                else
                  Select(_pnlMain.Controls[pos - 1]);
              } else if(_recordings.Contains(e.Season.Show))
                ListSeasons(e.Season.Show);
              else
                ListShows();
            else
              ShowInfo(_selected.Tag);
    }

    #region Form Event Handlers
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
      _settings.Save();
    }

    private void MythClient_KeyDown(object sender, KeyEventArgs e) {
      switch(e.KeyCode) {
        // refresh recordings
        case Keys.F5:
          RefreshRecordings();
          break;
        // select next item to the left, or wrap up to the previous line's rightmost item
        case Keys.Left:
          if(_selected != null) {
            int i = _pnlMain.Controls.IndexOf(_selected);
            if(i > 0)
              Select(_pnlMain.Controls[i - 1]);
          }
          break;
        // select next item to the right, or wrap down to the next line's leftmost item
        case Keys.Right:
          if(_selected != null) {
            int i = _pnlMain.Controls.IndexOf(_selected);
            if(i + 1 < _pnlMain.Controls.Count)
              Select(_pnlMain.Controls[i + 1]);
          }
          break;
        // select item in the same position but one row up
        case Keys.Up:
          if(_selected != null) {
            Control found = null;
            foreach(Control c in _pnlMain.Controls)
              if(c.Left == _selected.Left && c.Top < _selected.Top && (found == null || c.Top > found.Top))
                found = c;
            if(found != null)
              Select(found);
          }
          break;
        // select item in the same position but one row down
        case Keys.Down:
          if(_selected != null) {
            Control found = null;
            foreach(Control c in _pnlMain.Controls)
              if(c.Left == _selected.Left && c.Top > _selected.Top && (found == null || c.Top < found.Top))
                found = c;
            if(found != null)
              Select(found);
          }
          break;
        // double-click the selected item.  unfortunately there's no way to just call the doubleclick event so i have to figure out what type of thing is selected.
        case Keys.Enter:
          if(_selected != null)
            if(_selected.Tag is Episode)
              PlayEpisode((Episode)_selected.Tag);
            else if(_selected.Tag is Season)
              ListEpisodes((Season)_selected.Tag);
            else if(_selected.Tag is Show)
              ListSeasons((Show)_selected.Tag);
          break;
        // delete the selected episode or the oldest episode for the show or season
        case Keys.Delete:
          if(_selected != null)
            if(_selected.Tag is Episode)
              DeleteEpisode((Episode)_selected.Tag);
            else if(_selected.Tag is Season)
              DeleteEpisode(((Season)_selected.Tag).OldestEpisode);
            else if(_selected.Tag is Show)
              DeleteEpisode(((Show)_selected.Tag).OldestEpisode);
          break;
        // go back
        case Keys.Back:
          if(_pbBack.Enabled)
            Back();
          break;
      }
    }
    #endregion Form Event Handlers

    #region Header Event Handlers
    private void pbButton_MouseEnter(object sender, EventArgs e) {
      ((Control)sender).BackColor = SystemColors.Control;
    }

    private void pbButton_MouseLeave(object sender, EventArgs e) {
      ((Control)sender).BackColor = ((Control)sender).Parent.BackColor;
    }

    private void _pbBack_EnabledChanged(object sender, EventArgs e) {
      _pbBack.Image = _pbBack.Enabled ? Properties.Resources.Back : Properties.Resources.BackDisabled;
    }

    private void _pbBack_Click(object sender, EventArgs e) {
      Back();
    }

    private void _pbReload_Click(object sender, EventArgs e) {
      RefreshRecordings();
    }

    private void _pbMenu_MouseClick(object sender, MouseEventArgs e) {
      _cmnuMain.Show(_pbMenu, new Point(-_cmnuMain.Width + _pbMenu.Width, _pbMenu.Height));
    }

    private void _cmnuMainSettings_Click(object sender, EventArgs e) {
      new MythServerConfig(_settings).ShowDialog(this);
      if(!_recordings.Equals(_settings.ServerName, _settings.ServerPort)) {
        _recordings = new MythRecordings(_settings.ServerName, _settings.ServerPort);
        RefreshRecordings();
      }
    }

    private void _cmnuMainAbout_Click(object sender, EventArgs e) {
      new AboutMythClient().ShowDialog(this);
    }
    #endregion Header Event Handlers

    #region Main Event Handlers
    private void mainItem_Click(object sender, EventArgs e) {
      Select((Control)sender);
    }

    private void pbShow_DoubleClick(object sender, EventArgs e) {
      ListSeasons((Show)((PictureBox)sender).Tag);
    }

    private void pbSeason_DoubleClick(object sender, EventArgs e) {
      ListEpisodes((Season)((PictureBox)sender).Tag);
    }

    private void pbEpisode_DoubleClick(object sender, EventArgs e) {
      PlayEpisode((Episode)((Control)sender).Tag);
    }

    private void _cmnuShow_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
      Select(_cmnuShow.SourceControl);
    }

    private void _cmnuShowPlay_Click(object sender, EventArgs e) {
      PlayEpisode(((Show)_cmnuShow.SourceControl.Tag).OldestEpisode);
    }

    private void _cmnuShowDelete_Click(object sender, EventArgs e) {
      DeleteEpisode(((Show)_cmnuShow.SourceControl.Tag).OldestEpisode);
    }

    private void _cmnuSeason_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
      Select(_cmnuSeason.SourceControl);
    }

    private void _cmnuSeasonPlay_Click(object sender, EventArgs e) {
      PlayEpisode(((Season)_cmnuSeason.SourceControl.Tag).OldestEpisode);
    }

    private void _cmnuSeasonDelete_Click(object sender, EventArgs e) {
      DeleteEpisode(((Season)_cmnuSeason.SourceControl.Tag).OldestEpisode);
    }

    private void _cmnuEpisode_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
      Select(_cmnuEpisode.SourceControl);
    }

    private void _cmnuEpisodePlay_Click(object sender, EventArgs e) {
      PlayEpisode((Episode)_cmnuEpisode.SourceControl.Tag);
    }

    private void _cmnuEpisodePlayWith_Click(object sender, EventArgs e) {
      PlayEpisodeWith((Episode)_cmnuEpisode.SourceControl.Tag);
    }

    private void _cmnuEpisodeExport_Click(object sender, EventArgs e) {
      ExportEpisode((Episode)_cmnuEpisode.SourceControl.Tag);
    }

    private void _cmnuEpisodeDelete_Click(object sender, EventArgs e) {
      DeleteEpisode((Episode)_cmnuEpisode.SourceControl.Tag);
    }
    #endregion Main Event Handlers

    #region Info Event Handlers
    private void ActionButton_MouseEnter(object sender, EventArgs e) {
      ((Button)sender).ForeColor = SystemColors.HighlightText;
    }

    private void ActionButton_MouseLeave(object sender, EventArgs e) {
      ((Button)sender).ForeColor = _pnlInfo.ForeColor;
    }

    private void btnShowPlay_Click(object sender, EventArgs e) {
      PlayEpisode(((Show)_pnlInfo.Tag).OldestEpisode);
    }

    private void btnShowDelete_Click(object sender, EventArgs e) {
      DeleteEpisode(((Show)_pnlInfo.Tag).OldestEpisode);
    }

    private void btnSeasonPlay_Click(object sender, EventArgs e) {
      PlayEpisode(((Season)_pnlInfo.Tag).OldestEpisode);
    }

    private void btnSeasonDelete_Click(object sender, EventArgs e) {
      DeleteEpisode(((Season)_pnlInfo.Tag).OldestEpisode);
    }

    private void btnEpisodePlay_Click(object sender, EventArgs e) {
      PlayEpisode((Episode)_pnlInfo.Tag);
    }

    private void btnEpisodePlayWith_Click(object sender, EventArgs e) {
      PlayEpisodeWith((Episode)_pnlInfo.Tag);
    }

    private void btnEpisodeExport_Click(object sender, EventArgs e) {
      ExportEpisode((Episode)_pnlInfo.Tag);
    }

    private void btnEpisodeDelete_Click(object sender, EventArgs e) {
      DeleteEpisode((Episode)_pnlInfo.Tag);
    }
    #endregion Info Event Handlers

    #region old export code
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
    #endregion old code
  }

  /// <summary>
  /// Extension class for TimeSpan
  /// </summary>
  public static class TimeSpanExt {
    /// <summary>
    /// Converts the value of the current TimeSpan object to a string formatted
    /// in number of minutes, or hours and minutes.
    /// </summary>
    /// <param name="timespan"></param>
    /// <returns></returns>
    public static string ToStringUnit(this TimeSpan timespan) {
      if(timespan.TotalHours >= 2)
        if(timespan.Minutes > 0)
          return string.Format(Properties.Resources.TimeSpanHoursMinutes, Math.Floor(timespan.TotalHours), timespan.Minutes);
        else
          return string.Format(Properties.Resources.TimeSpanHours, timespan.TotalHours);
      return string.Format(Properties.Resources.TimeSpanMinutes, timespan.TotalMinutes);
    }
  }
}
