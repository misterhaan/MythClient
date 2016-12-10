namespace au.Applications.MythClient {
  partial class MythClient {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if(disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MythClient));
      this._cmnuEpisode = new System.Windows.Forms.ContextMenuStrip(this.components);
      this._cmnuEpisodePlay = new System.Windows.Forms.ToolStripMenuItem();
      this._cmnuEpisodePlayWith = new System.Windows.Forms.ToolStripMenuItem();
      this._cmnuEpisodeExport = new System.Windows.Forms.ToolStripMenuItem();
      this._cmnuEpisodeDelete = new System.Windows.Forms.ToolStripMenuItem();
      this._pnlMain = new System.Windows.Forms.FlowLayoutPanel();
      this._pnlHead = new System.Windows.Forms.Panel();
      this._lblTitle = new System.Windows.Forms.Label();
      this._pbMenu = new System.Windows.Forms.PictureBox();
      this._pbReload = new System.Windows.Forms.PictureBox();
      this._pbBack = new System.Windows.Forms.PictureBox();
      this._pnlInfo = new System.Windows.Forms.Panel();
      this._cmnuSeason = new System.Windows.Forms.ContextMenuStrip(this.components);
      this._cmnuSeasonPlay = new System.Windows.Forms.ToolStripMenuItem();
      this._cmnuSeasonDelete = new System.Windows.Forms.ToolStripMenuItem();
      this._cmnuShow = new System.Windows.Forms.ContextMenuStrip(this.components);
      this._cmnuShowPlay = new System.Windows.Forms.ToolStripMenuItem();
      this._cmnuShowDelete = new System.Windows.Forms.ToolStripMenuItem();
      this._tip = new System.Windows.Forms.ToolTip(this.components);
      this._cmnuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
      this._cmnuMainSettings = new System.Windows.Forms.ToolStripMenuItem();
      this._cmnuMainAbout = new System.Windows.Forms.ToolStripMenuItem();
      this._dlgExportFolder = new System.Windows.Forms.FolderBrowserDialog();
      this._cmnuShowExport = new System.Windows.Forms.ToolStripMenuItem();
      this._cmnuSeasonExport = new System.Windows.Forms.ToolStripMenuItem();
      this._cmnuEpisode.SuspendLayout();
      this._pnlHead.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this._pbMenu)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this._pbReload)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this._pbBack)).BeginInit();
      this._cmnuSeason.SuspendLayout();
      this._cmnuShow.SuspendLayout();
      this._cmnuMain.SuspendLayout();
      this.SuspendLayout();
      // 
      // _cmnuEpisode
      // 
      this._cmnuEpisode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._cmnuEpisodePlay,
            this._cmnuEpisodePlayWith,
            this._cmnuEpisodeExport,
            this._cmnuEpisodeDelete});
      this._cmnuEpisode.Name = "_cmnuEpisode";
      this._cmnuEpisode.Size = new System.Drawing.Size(153, 114);
      this._cmnuEpisode.Opening += new System.ComponentModel.CancelEventHandler(this._cmnuEpisode_Opening);
      // 
      // _cmnuEpisodePlay
      // 
      this._cmnuEpisodePlay.Image = global::au.Applications.MythClient.Properties.Resources.Play;
      this._cmnuEpisodePlay.Name = "_cmnuEpisodePlay";
      this._cmnuEpisodePlay.Size = new System.Drawing.Size(131, 22);
      this._cmnuEpisodePlay.Text = "&Play";
      this._cmnuEpisodePlay.Click += new System.EventHandler(this._cmnuEpisodePlay_Click);
      // 
      // _cmnuEpisodePlayWith
      // 
      this._cmnuEpisodePlayWith.Image = global::au.Applications.MythClient.Properties.Resources.PlayWith;
      this._cmnuEpisodePlayWith.Name = "_cmnuEpisodePlayWith";
      this._cmnuEpisodePlayWith.Size = new System.Drawing.Size(131, 22);
      this._cmnuEpisodePlayWith.Text = "Play &with...";
      this._cmnuEpisodePlayWith.Click += new System.EventHandler(this._cmnuEpisodePlayWith_Click);
      // 
      // _cmnuEpisodeExport
      // 
      this._cmnuEpisodeExport.Image = global::au.Applications.MythClient.Properties.Resources.Export;
      this._cmnuEpisodeExport.Name = "_cmnuEpisodeExport";
      this._cmnuEpisodeExport.Size = new System.Drawing.Size(152, 22);
      this._cmnuEpisodeExport.Text = "Dow&nload...";
      this._cmnuEpisodeExport.Click += new System.EventHandler(this._cmnuEpisodeExport_Click);
      // 
      // _cmnuEpisodeDelete
      // 
      this._cmnuEpisodeDelete.Image = global::au.Applications.MythClient.Properties.Resources.Delete;
      this._cmnuEpisodeDelete.Name = "_cmnuEpisodeDelete";
      this._cmnuEpisodeDelete.Size = new System.Drawing.Size(131, 22);
      this._cmnuEpisodeDelete.Text = "&Delete";
      this._cmnuEpisodeDelete.Click += new System.EventHandler(this._cmnuEpisodeDelete_Click);
      // 
      // _pnlMain
      // 
      this._pnlMain.AutoScroll = true;
      this._pnlMain.BackColor = System.Drawing.SystemColors.AppWorkspace;
      this._pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this._pnlMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._pnlMain.Location = new System.Drawing.Point(0, 40);
      this._pnlMain.Margin = new System.Windows.Forms.Padding(21);
      this._pnlMain.Name = "_pnlMain";
      this._pnlMain.Padding = new System.Windows.Forms.Padding(21);
      this._pnlMain.Size = new System.Drawing.Size(404, 281);
      this._pnlMain.TabIndex = 1;
      // 
      // _pnlHead
      // 
      this._pnlHead.BackColor = System.Drawing.SystemColors.Window;
      this._pnlHead.Controls.Add(this._lblTitle);
      this._pnlHead.Controls.Add(this._pbMenu);
      this._pnlHead.Controls.Add(this._pbReload);
      this._pnlHead.Controls.Add(this._pbBack);
      this._pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
      this._pnlHead.ForeColor = System.Drawing.SystemColors.WindowText;
      this._pnlHead.Location = new System.Drawing.Point(0, 0);
      this._pnlHead.Name = "_pnlHead";
      this._pnlHead.Size = new System.Drawing.Size(624, 40);
      this._pnlHead.TabIndex = 1;
      // 
      // _lblTitle
      // 
      this._lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this._lblTitle.AutoEllipsis = true;
      this._lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._lblTitle.Location = new System.Drawing.Point(83, 0);
      this._lblTitle.Name = "_lblTitle";
      this._lblTitle.Size = new System.Drawing.Size(498, 40);
      this._lblTitle.TabIndex = 3;
      this._lblTitle.Text = "MythTV Recorded Programs";
      this._lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._lblTitle.UseMnemonic = false;
      // 
      // _pbMenu
      // 
      this._pbMenu.Anchor = System.Windows.Forms.AnchorStyles.Right;
      this._pbMenu.Image = global::au.Applications.MythClient.Properties.Resources.Menu;
      this._pbMenu.Location = new System.Drawing.Point(588, 4);
      this._pbMenu.Margin = new System.Windows.Forms.Padding(4);
      this._pbMenu.Name = "_pbMenu";
      this._pbMenu.Size = new System.Drawing.Size(32, 32);
      this._pbMenu.TabIndex = 6;
      this._pbMenu.TabStop = false;
      this._pbMenu.MouseClick += new System.Windows.Forms.MouseEventHandler(this._pbMenu_MouseClick);
      this._pbMenu.MouseEnter += new System.EventHandler(this.pbButton_MouseEnter);
      this._pbMenu.MouseLeave += new System.EventHandler(this.pbButton_MouseLeave);
      // 
      // _pbReload
      // 
      this._pbReload.Anchor = System.Windows.Forms.AnchorStyles.Left;
      this._pbReload.Image = global::au.Applications.MythClient.Properties.Resources.Reload;
      this._pbReload.Location = new System.Drawing.Point(44, 4);
      this._pbReload.Margin = new System.Windows.Forms.Padding(4);
      this._pbReload.Name = "_pbReload";
      this._pbReload.Size = new System.Drawing.Size(32, 32);
      this._pbReload.TabIndex = 5;
      this._pbReload.TabStop = false;
      this._tip.SetToolTip(this._pbReload, "Reload recording list from MythTV");
      this._pbReload.Click += new System.EventHandler(this._pbReload_Click);
      this._pbReload.MouseEnter += new System.EventHandler(this.pbButton_MouseEnter);
      this._pbReload.MouseLeave += new System.EventHandler(this.pbButton_MouseLeave);
      // 
      // _pbBack
      // 
      this._pbBack.Anchor = System.Windows.Forms.AnchorStyles.Left;
      this._pbBack.Enabled = false;
      this._pbBack.Image = global::au.Applications.MythClient.Properties.Resources.BackDisabled;
      this._pbBack.Location = new System.Drawing.Point(4, 4);
      this._pbBack.Margin = new System.Windows.Forms.Padding(4);
      this._pbBack.Name = "_pbBack";
      this._pbBack.Size = new System.Drawing.Size(32, 32);
      this._pbBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this._pbBack.TabIndex = 4;
      this._pbBack.TabStop = false;
      this._pbBack.EnabledChanged += new System.EventHandler(this._pbBack_EnabledChanged);
      this._pbBack.Click += new System.EventHandler(this._pbBack_Click);
      this._pbBack.MouseEnter += new System.EventHandler(this.pbButton_MouseEnter);
      this._pbBack.MouseLeave += new System.EventHandler(this.pbButton_MouseLeave);
      // 
      // _pnlInfo
      // 
      this._pnlInfo.Dock = System.Windows.Forms.DockStyle.Right;
      this._pnlInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._pnlInfo.Location = new System.Drawing.Point(404, 40);
      this._pnlInfo.Name = "_pnlInfo";
      this._pnlInfo.Size = new System.Drawing.Size(220, 281);
      this._pnlInfo.TabIndex = 2;
      // 
      // _cmnuSeason
      // 
      this._cmnuSeason.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._cmnuSeasonPlay,
            this._cmnuSeasonExport,
            this._cmnuSeasonDelete});
      this._cmnuSeason.Name = "_cmnuSeason";
      this._cmnuSeason.Size = new System.Drawing.Size(202, 70);
      this._cmnuSeason.Opening += new System.ComponentModel.CancelEventHandler(this._cmnuSeason_Opening);
      // 
      // _cmnuSeasonPlay
      // 
      this._cmnuSeasonPlay.Image = global::au.Applications.MythClient.Properties.Resources.Play;
      this._cmnuSeasonPlay.Name = "_cmnuSeasonPlay";
      this._cmnuSeasonPlay.Size = new System.Drawing.Size(201, 22);
      this._cmnuSeasonPlay.Text = "&Play next episode";
      this._cmnuSeasonPlay.Click += new System.EventHandler(this._cmnuSeasonPlay_Click);
      // 
      // _cmnuSeasonDelete
      // 
      this._cmnuSeasonDelete.Image = global::au.Applications.MythClient.Properties.Resources.Delete;
      this._cmnuSeasonDelete.Name = "_cmnuSeasonDelete";
      this._cmnuSeasonDelete.Size = new System.Drawing.Size(201, 22);
      this._cmnuSeasonDelete.Text = "&Delete next episode";
      this._cmnuSeasonDelete.Click += new System.EventHandler(this._cmnuSeasonDelete_Click);
      // 
      // _cmnuShow
      // 
      this._cmnuShow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._cmnuShowPlay,
            this._cmnuShowExport,
            this._cmnuShowDelete});
      this._cmnuShow.Name = "_cmnuShow";
      this._cmnuShow.Size = new System.Drawing.Size(202, 70);
      this._cmnuShow.Opening += new System.ComponentModel.CancelEventHandler(this._cmnuShow_Opening);
      // 
      // _cmnuShowPlay
      // 
      this._cmnuShowPlay.Image = global::au.Applications.MythClient.Properties.Resources.Play;
      this._cmnuShowPlay.Name = "_cmnuShowPlay";
      this._cmnuShowPlay.Size = new System.Drawing.Size(201, 22);
      this._cmnuShowPlay.Text = "&Play next episode";
      this._cmnuShowPlay.Click += new System.EventHandler(this._cmnuShowPlay_Click);
      // 
      // _cmnuShowDelete
      // 
      this._cmnuShowDelete.Image = global::au.Applications.MythClient.Properties.Resources.Delete;
      this._cmnuShowDelete.Name = "_cmnuShowDelete";
      this._cmnuShowDelete.Size = new System.Drawing.Size(201, 22);
      this._cmnuShowDelete.Text = "&Delete next episode";
      this._cmnuShowDelete.Click += new System.EventHandler(this._cmnuShowDelete_Click);
      // 
      // _cmnuMain
      // 
      this._cmnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._cmnuMainSettings,
            this._cmnuMainAbout});
      this._cmnuMain.Name = "_cmnuMain";
      this._cmnuMain.Size = new System.Drawing.Size(126, 48);
      // 
      // _cmnuMainSettings
      // 
      this._cmnuMainSettings.Image = global::au.Applications.MythClient.Properties.Resources.Settings;
      this._cmnuMainSettings.Name = "_cmnuMainSettings";
      this._cmnuMainSettings.Size = new System.Drawing.Size(125, 22);
      this._cmnuMainSettings.Text = "&Settings...";
      this._cmnuMainSettings.Click += new System.EventHandler(this._cmnuMainSettings_Click);
      // 
      // _cmnuMainAbout
      // 
      this._cmnuMainAbout.Name = "_cmnuMainAbout";
      this._cmnuMainAbout.Size = new System.Drawing.Size(125, 22);
      this._cmnuMainAbout.Text = "&About";
      this._cmnuMainAbout.Click += new System.EventHandler(this._cmnuMainAbout_Click);
      // 
      // _dlgExportFolder
      // 
      this._dlgExportFolder.Description = "Choose a directory for downloaded recordings.";
      // 
      // _cmnuShowExport
      // 
      this._cmnuShowExport.Image = global::au.Applications.MythClient.Properties.Resources.Export;
      this._cmnuShowExport.Name = "_cmnuShowExport";
      this._cmnuShowExport.Size = new System.Drawing.Size(201, 22);
      this._cmnuShowExport.Text = "Dow&nload all episodes...";
      this._cmnuShowExport.Click += new System.EventHandler(this._cmnuShowExport_Click);
      // 
      // _cmnuSeasonExport
      // 
      this._cmnuSeasonExport.Image = global::au.Applications.MythClient.Properties.Resources.Export;
      this._cmnuSeasonExport.Name = "_cmnuSeasonExport";
      this._cmnuSeasonExport.Size = new System.Drawing.Size(201, 22);
      this._cmnuSeasonExport.Text = "Dow&nload all episodes...";
      this._cmnuSeasonExport.Click += new System.EventHandler(this._cmnuSeasonExport_Click);
      // 
      // MythClient
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(624, 321);
      this.Controls.Add(this._pnlMain);
      this.Controls.Add(this._pnlInfo);
      this.Controls.Add(this._pnlHead);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Name = "MythClient";
      this.Text = "MythTV Recorded Programs";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MythClient_FormClosed);
      this.Load += new System.EventHandler(this.MythClient_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MythClient_KeyDown);
      this.Move += new System.EventHandler(this.MythClient_Move);
      this.Resize += new System.EventHandler(this.MythClient_Resize);
      this._cmnuEpisode.ResumeLayout(false);
      this._pnlHead.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this._pbMenu)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this._pbReload)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this._pbBack)).EndInit();
      this._cmnuSeason.ResumeLayout(false);
      this._cmnuShow.ResumeLayout(false);
      this._cmnuMain.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.ContextMenuStrip _cmnuEpisode;
    private System.Windows.Forms.ToolStripMenuItem _cmnuEpisodePlay;
    private System.Windows.Forms.ToolStripMenuItem _cmnuEpisodeDelete;
    private System.Windows.Forms.ToolStripMenuItem _cmnuEpisodePlayWith;
    private System.Windows.Forms.ToolStripMenuItem _cmnuEpisodeExport;
    private System.Windows.Forms.FlowLayoutPanel _pnlMain;
    private System.Windows.Forms.Panel _pnlHead;
    private System.Windows.Forms.Label _lblTitle;
    private System.Windows.Forms.Panel _pnlInfo;
    private System.Windows.Forms.ContextMenuStrip _cmnuSeason;
    private System.Windows.Forms.ToolStripMenuItem _cmnuSeasonPlay;
    private System.Windows.Forms.ToolStripMenuItem _cmnuSeasonDelete;
    private System.Windows.Forms.ContextMenuStrip _cmnuShow;
    private System.Windows.Forms.ToolStripMenuItem _cmnuShowPlay;
    private System.Windows.Forms.ToolStripMenuItem _cmnuShowDelete;
    private System.Windows.Forms.PictureBox _pbBack;
    private System.Windows.Forms.PictureBox _pbReload;
    private System.Windows.Forms.ToolTip _tip;
    private System.Windows.Forms.PictureBox _pbMenu;
    private System.Windows.Forms.ContextMenuStrip _cmnuMain;
    private System.Windows.Forms.ToolStripMenuItem _cmnuMainSettings;
    private System.Windows.Forms.ToolStripMenuItem _cmnuMainAbout;
    private System.Windows.Forms.FolderBrowserDialog _dlgExportFolder;
    private System.Windows.Forms.ToolStripMenuItem _cmnuShowExport;
    private System.Windows.Forms.ToolStripMenuItem _cmnuSeasonExport;
  }
}

