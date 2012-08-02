﻿namespace au.Applications.MythClient {
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
      System.Windows.Forms.ToolStripSeparator _tsLine;
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MythClient));
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this._lvRecorded = new System.Windows.Forms.ListView();
      this._colEpisode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this._colShow = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this._colAired = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this._colRecorded = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this._cmnuEpisode = new System.Windows.Forms.ContextMenuStrip(this.components);
      this._cmnuPlay = new System.Windows.Forms.ToolStripMenuItem();
      this._cmnuDelete = new System.Windows.Forms.ToolStripMenuItem();
      this._ts = new System.Windows.Forms.ToolStrip();
      this._tsPlay = new System.Windows.Forms.ToolStripButton();
      this._tsDelete = new System.Windows.Forms.ToolStripButton();
      this._tsRefresh = new System.Windows.Forms.ToolStripButton();
      this._tsSettings = new System.Windows.Forms.ToolStripButton();
      this._tsPlayWith = new System.Windows.Forms.ToolStripButton();
      this._cmnuPlayWith = new System.Windows.Forms.ToolStripMenuItem();
      _tsLine = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.LeftToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this._cmnuEpisode.SuspendLayout();
      this._ts.SuspendLayout();
      this.SuspendLayout();
      // 
      // _tsLine
      // 
      _tsLine.Name = "_tsLine";
      _tsLine.Size = new System.Drawing.Size(22, 6);
      // 
      // toolStripContainer1
      // 
      this.toolStripContainer1.BottomToolStripPanelVisible = false;
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this._lvRecorded);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(600, 321);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      // 
      // toolStripContainer1.LeftToolStripPanel
      // 
      this.toolStripContainer1.LeftToolStripPanel.Controls.Add(this._ts);
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size(624, 321);
      this.toolStripContainer1.TabIndex = 0;
      this.toolStripContainer1.Text = "toolStripContainer1";
      this.toolStripContainer1.TopToolStripPanelVisible = false;
      // 
      // _lvRecorded
      // 
      this._lvRecorded.AllowColumnReorder = true;
      this._lvRecorded.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._colEpisode,
            this._colShow,
            this._colAired,
            this._colRecorded});
      this._lvRecorded.ContextMenuStrip = this._cmnuEpisode;
      this._lvRecorded.Dock = System.Windows.Forms.DockStyle.Fill;
      this._lvRecorded.FullRowSelect = true;
      this._lvRecorded.Location = new System.Drawing.Point(0, 0);
      this._lvRecorded.MultiSelect = false;
      this._lvRecorded.Name = "_lvRecorded";
      this._lvRecorded.Size = new System.Drawing.Size(600, 321);
      this._lvRecorded.TabIndex = 0;
      this._lvRecorded.UseCompatibleStateImageBehavior = false;
      this._lvRecorded.View = System.Windows.Forms.View.Details;
      this._lvRecorded.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this._lvRecorded_ColumnClick);
      this._lvRecorded.SelectedIndexChanged += new System.EventHandler(this._lvRecorded_SelectedIndexChanged);
      this._lvRecorded.DoubleClick += new System.EventHandler(this._lvRecorded_DoubleClick);
      this._lvRecorded.KeyDown += new System.Windows.Forms.KeyEventHandler(this._lvRecorded_KeyDown);
      // 
      // _colEpisode
      // 
      this._colEpisode.Text = "Episode";
      this._colEpisode.Width = 227;
      // 
      // _colShow
      // 
      this._colShow.Text = "Show";
      this._colShow.Width = 138;
      // 
      // _colAired
      // 
      this._colAired.Text = "Air Date";
      this._colAired.Width = 71;
      // 
      // _colRecorded
      // 
      this._colRecorded.Text = "Recorded";
      this._colRecorded.Width = 129;
      // 
      // _cmnuEpisode
      // 
      this._cmnuEpisode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._cmnuPlay,
            this._cmnuPlayWith,
            this._cmnuDelete});
      this._cmnuEpisode.Name = "_cmnuEpisode";
      this._cmnuEpisode.Size = new System.Drawing.Size(132, 70);
      // 
      // _cmnuPlay
      // 
      this._cmnuPlay.Enabled = false;
      this._cmnuPlay.Image = global::au.Applications.MythClient.Properties.Resources.Play;
      this._cmnuPlay.Name = "_cmnuPlay";
      this._cmnuPlay.Size = new System.Drawing.Size(131, 22);
      this._cmnuPlay.Text = "&Play";
      this._cmnuPlay.Click += new System.EventHandler(this._tsPlay_Click);
      // 
      // _cmnuDelete
      // 
      this._cmnuDelete.Enabled = false;
      this._cmnuDelete.Image = global::au.Applications.MythClient.Properties.Resources.Delete;
      this._cmnuDelete.Name = "_cmnuDelete";
      this._cmnuDelete.Size = new System.Drawing.Size(131, 22);
      this._cmnuDelete.Text = "&Delete";
      this._cmnuDelete.Click += new System.EventHandler(this._tsDelete_Click);
      // 
      // _ts
      // 
      this._ts.Dock = System.Windows.Forms.DockStyle.None;
      this._ts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._tsPlay,
            this._tsPlayWith,
            this._tsDelete,
            _tsLine,
            this._tsRefresh,
            this._tsSettings});
      this._ts.Location = new System.Drawing.Point(0, 3);
      this._ts.Name = "_ts";
      this._ts.Size = new System.Drawing.Size(24, 132);
      this._ts.TabIndex = 0;
      // 
      // _tsPlay
      // 
      this._tsPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this._tsPlay.Enabled = false;
      this._tsPlay.Image = global::au.Applications.MythClient.Properties.Resources.Play;
      this._tsPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._tsPlay.Name = "_tsPlay";
      this._tsPlay.Size = new System.Drawing.Size(22, 20);
      this._tsPlay.Text = "Play";
      this._tsPlay.ToolTipText = "Play selected episode in default media player";
      this._tsPlay.Click += new System.EventHandler(this._tsPlay_Click);
      // 
      // _tsDelete
      // 
      this._tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this._tsDelete.Enabled = false;
      this._tsDelete.Image = global::au.Applications.MythClient.Properties.Resources.Delete;
      this._tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._tsDelete.Name = "_tsDelete";
      this._tsDelete.Size = new System.Drawing.Size(22, 20);
      this._tsDelete.Text = "Delete";
      this._tsDelete.ToolTipText = "Delete selected episode from the MythTV server";
      this._tsDelete.Click += new System.EventHandler(this._tsDelete_Click);
      // 
      // _tsRefresh
      // 
      this._tsRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this._tsRefresh.Image = global::au.Applications.MythClient.Properties.Resources.Reload;
      this._tsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._tsRefresh.Name = "_tsRefresh";
      this._tsRefresh.Size = new System.Drawing.Size(22, 20);
      this._tsRefresh.Text = "Refresh";
      this._tsRefresh.ToolTipText = "Refresh the list of recordings";
      this._tsRefresh.Click += new System.EventHandler(this._tsRefresh_Click);
      // 
      // _tsSettings
      // 
      this._tsSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this._tsSettings.Image = global::au.Applications.MythClient.Properties.Resources.Settings;
      this._tsSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this._tsSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._tsSettings.Name = "_tsSettings";
      this._tsSettings.Size = new System.Drawing.Size(22, 20);
      this._tsSettings.Text = "Settings";
      this._tsSettings.ToolTipText = "Configure MythTV server settings";
      this._tsSettings.Click += new System.EventHandler(this._tsSettings_Click);
      // 
      // _tsPlayWith
      // 
      this._tsPlayWith.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this._tsPlayWith.Enabled = false;
      this._tsPlayWith.Image = global::au.Applications.MythClient.Properties.Resources.PlayWith;
      this._tsPlayWith.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._tsPlayWith.Name = "_tsPlayWith";
      this._tsPlayWith.Size = new System.Drawing.Size(22, 20);
      this._tsPlayWith.Text = "toolStripButton1";
      this._tsPlayWith.Click += new System.EventHandler(this._tsPlayWith_Click);
      // 
      // _cmnuPlayWith
      // 
      this._cmnuPlayWith.Enabled = false;
      this._cmnuPlayWith.Image = global::au.Applications.MythClient.Properties.Resources.PlayWith;
      this._cmnuPlayWith.Name = "_cmnuPlayWith";
      this._cmnuPlayWith.Size = new System.Drawing.Size(131, 22);
      this._cmnuPlayWith.Text = "Play &with...";
      this._cmnuPlayWith.Click += new System.EventHandler(this._tsPlayWith_Click);
      // 
      // MythClient
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(624, 321);
      this.Controls.Add(this.toolStripContainer1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "MythClient";
      this.Text = "MythTV Recorded Programs";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MythClient_FormClosed);
      this.Load += new System.EventHandler(this.MythClient_Load);
      this.Move += new System.EventHandler(this.MythClient_Move);
      this.Resize += new System.EventHandler(this.MythClient_Resize);
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.LeftToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.LeftToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this._cmnuEpisode.ResumeLayout(false);
      this._ts.ResumeLayout(false);
      this._ts.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.ToolStrip _ts;
    private System.Windows.Forms.ToolStripButton _tsSettings;
    private System.Windows.Forms.ListView _lvRecorded;
    private System.Windows.Forms.ColumnHeader _colShow;
    private System.Windows.Forms.ColumnHeader _colEpisode;
    private System.Windows.Forms.ColumnHeader _colAired;
    private System.Windows.Forms.ColumnHeader _colRecorded;
    private System.Windows.Forms.ToolStripButton _tsPlay;
    private System.Windows.Forms.ToolStripButton _tsDelete;
    private System.Windows.Forms.ToolStripButton _tsRefresh;
    private System.Windows.Forms.ContextMenuStrip _cmnuEpisode;
    private System.Windows.Forms.ToolStripMenuItem _cmnuPlay;
    private System.Windows.Forms.ToolStripMenuItem _cmnuDelete;
    private System.Windows.Forms.ToolStripMenuItem _cmnuPlayWith;
    private System.Windows.Forms.ToolStripButton _tsPlayWith;
  }
}

