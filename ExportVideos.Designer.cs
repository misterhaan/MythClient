namespace au.Applications.MythClient {
  partial class ExportVideos {
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
      this._gbWhat = new System.Windows.Forms.GroupBox();
      this._rbAll = new System.Windows.Forms.RadioButton();
      this._rbShow = new System.Windows.Forms.RadioButton();
      this._rbEpisode = new System.Windows.Forms.RadioButton();
      this._gbHow = new System.Windows.Forms.GroupBox();
      this._rbShowTitle = new System.Windows.Forms.RadioButton();
      this._rbShowDateEpisode = new System.Windows.Forms.RadioButton();
      this._gbWhere = new System.Windows.Forms.GroupBox();
      this._fnbWhere = new au.util.comctl.FoldernameBox();
      this._btnCancel = new System.Windows.Forms.Button();
      this._btnOK = new System.Windows.Forms.Button();
      this._gbWhat.SuspendLayout();
      this._gbHow.SuspendLayout();
      this._gbWhere.SuspendLayout();
      this.SuspendLayout();
      // 
      // _gbWhat
      // 
      this._gbWhat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._gbWhat.Controls.Add(this._rbAll);
      this._gbWhat.Controls.Add(this._rbShow);
      this._gbWhat.Controls.Add(this._rbEpisode);
      this._gbWhat.Location = new System.Drawing.Point(12, 12);
      this._gbWhat.Name = "_gbWhat";
      this._gbWhat.Size = new System.Drawing.Size(336, 88);
      this._gbWhat.TabIndex = 0;
      this._gbWhat.TabStop = false;
      this._gbWhat.Text = "What";
      // 
      // _rbAll
      // 
      this._rbAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._rbAll.AutoEllipsis = true;
      this._rbAll.Location = new System.Drawing.Point(6, 65);
      this._rbAll.Name = "_rbAll";
      this._rbAll.Size = new System.Drawing.Size(324, 17);
      this._rbAll.TabIndex = 2;
      this._rbAll.Text = "Everything (yep, all of it)";
      this._rbAll.UseVisualStyleBackColor = true;
      // 
      // _rbShow
      // 
      this._rbShow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._rbShow.AutoEllipsis = true;
      this._rbShow.Location = new System.Drawing.Point(6, 42);
      this._rbShow.Name = "_rbShow";
      this._rbShow.Size = new System.Drawing.Size(324, 17);
      this._rbShow.TabIndex = 1;
      this._rbShow.Text = "All episodes of ";
      this._rbShow.UseVisualStyleBackColor = true;
      // 
      // _rbEpisode
      // 
      this._rbEpisode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._rbEpisode.AutoEllipsis = true;
      this._rbEpisode.Checked = true;
      this._rbEpisode.Location = new System.Drawing.Point(6, 19);
      this._rbEpisode.Name = "_rbEpisode";
      this._rbEpisode.Size = new System.Drawing.Size(324, 17);
      this._rbEpisode.TabIndex = 0;
      this._rbEpisode.TabStop = true;
      this._rbEpisode.Text = "Just ";
      this._rbEpisode.UseVisualStyleBackColor = true;
      // 
      // _gbHow
      // 
      this._gbHow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._gbHow.Controls.Add(this._rbShowTitle);
      this._gbHow.Controls.Add(this._rbShowDateEpisode);
      this._gbHow.Location = new System.Drawing.Point(12, 106);
      this._gbHow.Name = "_gbHow";
      this._gbHow.Size = new System.Drawing.Size(336, 65);
      this._gbHow.TabIndex = 1;
      this._gbHow.TabStop = false;
      this._gbHow.Text = "How";
      // 
      // _rbShowTitle
      // 
      this._rbShowTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._rbShowTitle.AutoEllipsis = true;
      this._rbShowTitle.Location = new System.Drawing.Point(6, 42);
      this._rbShowTitle.Name = "_rbShowTitle";
      this._rbShowTitle.Size = new System.Drawing.Size(324, 17);
      this._rbShowTitle.TabIndex = 3;
      this._rbShowTitle.Text = "Show";
      this._rbShowTitle.UseVisualStyleBackColor = true;
      // 
      // _rbShowDateEpisode
      // 
      this._rbShowDateEpisode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._rbShowDateEpisode.AutoEllipsis = true;
      this._rbShowDateEpisode.Checked = true;
      this._rbShowDateEpisode.Location = new System.Drawing.Point(6, 19);
      this._rbShowDateEpisode.Name = "_rbShowDateEpisode";
      this._rbShowDateEpisode.Size = new System.Drawing.Size(324, 17);
      this._rbShowDateEpisode.TabIndex = 2;
      this._rbShowDateEpisode.TabStop = true;
      this._rbShowDateEpisode.Text = "ShowDateEpisode";
      this._rbShowDateEpisode.UseVisualStyleBackColor = true;
      // 
      // _gbWhere
      // 
      this._gbWhere.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._gbWhere.Controls.Add(this._fnbWhere);
      this._gbWhere.Location = new System.Drawing.Point(12, 177);
      this._gbWhere.Name = "_gbWhere";
      this._gbWhere.Size = new System.Drawing.Size(336, 45);
      this._gbWhere.TabIndex = 2;
      this._gbWhere.TabStop = false;
      this._gbWhere.Text = "Where";
      // 
      // _fnbWhere
      // 
      this._fnbWhere.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._fnbWhere.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this._fnbWhere.BasePath = "";
      this._fnbWhere.Description = "Choose or create a location to save episodes.";
      this._fnbWhere.FlatButton = true;
      this._fnbWhere.FolderFullName = "";
      this._fnbWhere.FolderMustExist = true;
      this._fnbWhere.FolderName = "";
      this._fnbWhere.LimitBase = false;
      this._fnbWhere.Location = new System.Drawing.Point(6, 19);
      this._fnbWhere.MaximumSize = new System.Drawing.Size(32767, 20);
      this._fnbWhere.MinimumSize = new System.Drawing.Size(50, 20);
      this._fnbWhere.Name = "_fnbWhere";
      this._fnbWhere.Size = new System.Drawing.Size(324, 20);
      this._fnbWhere.StripBase = false;
      this._fnbWhere.TabIndex = 0;
      this._fnbWhere.Validated += new System.EventHandler(this._fnbWhere_Validated);
      // 
      // _btnCancel
      // 
      this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this._btnCancel.Location = new System.Drawing.Point(273, 238);
      this._btnCancel.Name = "_btnCancel";
      this._btnCancel.Size = new System.Drawing.Size(75, 23);
      this._btnCancel.TabIndex = 4;
      this._btnCancel.Text = "Cancel";
      this._btnCancel.UseVisualStyleBackColor = true;
      // 
      // _btnOK
      // 
      this._btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this._btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this._btnOK.Enabled = false;
      this._btnOK.Image = global::au.Applications.MythClient.Properties.Resources.Export;
      this._btnOK.Location = new System.Drawing.Point(192, 238);
      this._btnOK.Name = "_btnOK";
      this._btnOK.Size = new System.Drawing.Size(75, 23);
      this._btnOK.TabIndex = 3;
      this._btnOK.Text = "Save";
      this._btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btnOK.UseVisualStyleBackColor = true;
      this._btnOK.Click += new System.EventHandler(this._btnOK_Click);
      // 
      // ExportVideos
      // 
      this.AcceptButton = this._btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this._btnCancel;
      this.ClientSize = new System.Drawing.Size(360, 273);
      this.ControlBox = false;
      this.Controls.Add(this._btnCancel);
      this.Controls.Add(this._btnOK);
      this.Controls.Add(this._gbWhere);
      this.Controls.Add(this._gbHow);
      this.Controls.Add(this._gbWhat);
      this.Name = "ExportVideos";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Save Episodes";
      this.Load += new System.EventHandler(this.ExportVideos_Load);
      this._gbWhat.ResumeLayout(false);
      this._gbHow.ResumeLayout(false);
      this._gbWhere.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox _gbWhat;
    private System.Windows.Forms.RadioButton _rbAll;
    private System.Windows.Forms.RadioButton _rbShow;
    private System.Windows.Forms.RadioButton _rbEpisode;
    private System.Windows.Forms.GroupBox _gbHow;
    private System.Windows.Forms.RadioButton _rbShowTitle;
    private System.Windows.Forms.RadioButton _rbShowDateEpisode;
    private System.Windows.Forms.GroupBox _gbWhere;
    private util.comctl.FoldernameBox _fnbWhere;
    private System.Windows.Forms.Button _btnOK;
    private System.Windows.Forms.Button _btnCancel;
  }
}