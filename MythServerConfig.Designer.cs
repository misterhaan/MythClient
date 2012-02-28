namespace au.Applications.MythClient {
  partial class MythServerConfig {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MythServerConfig));
      this._btnOK = new System.Windows.Forms.Button();
      this._lblServerName = new System.Windows.Forms.Label();
      this._txtServerName = new System.Windows.Forms.TextBox();
      this._lblURL = new System.Windows.Forms.Label();
      this._txtURL = new System.Windows.Forms.TextBox();
      this._lnkURL = new System.Windows.Forms.LinkLabel();
      this._lblDirectory = new System.Windows.Forms.Label();
      this._fnbDirectory = new au.util.comctl.FoldernameBox();
      this._tip = new System.Windows.Forms.ToolTip(this.components);
      this.SuspendLayout();
      // 
      // _btnOK
      // 
      this._btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this._btnOK.Location = new System.Drawing.Point(430, 175);
      this._btnOK.Name = "_btnOK";
      this._btnOK.Size = new System.Drawing.Size(75, 23);
      this._btnOK.TabIndex = 7;
      this._btnOK.Text = "OK";
      this._btnOK.UseVisualStyleBackColor = true;
      this._btnOK.Click += new System.EventHandler(this._btnOK_Click);
      // 
      // _lblServerName
      // 
      this._lblServerName.AutoSize = true;
      this._lblServerName.Location = new System.Drawing.Point(24, 15);
      this._lblServerName.Name = "_lblServerName";
      this._lblServerName.Size = new System.Drawing.Size(79, 13);
      this._lblServerName.TabIndex = 0;
      this._lblServerName.Text = "MythTV server:";
      // 
      // _txtServerName
      // 
      this._txtServerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this._txtServerName.Location = new System.Drawing.Point(31, 31);
      this._txtServerName.Name = "_txtServerName";
      this._txtServerName.Size = new System.Drawing.Size(446, 20);
      this._txtServerName.TabIndex = 1;
      this._txtServerName.TextChanged += new System.EventHandler(this._txtServerName_TextChanged);
      // 
      // _lblURL
      // 
      this._lblURL.AutoSize = true;
      this._lblURL.Location = new System.Drawing.Point(24, 63);
      this._lblURL.Name = "_lblURL";
      this._lblURL.Size = new System.Drawing.Size(128, 13);
      this._lblURL.TabIndex = 2;
      this._lblURL.Text = "Recorded programs URL:";
      // 
      // _txtURL
      // 
      this._txtURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this._txtURL.Location = new System.Drawing.Point(31, 79);
      this._txtURL.Name = "_txtURL";
      this._txtURL.Size = new System.Drawing.Size(446, 20);
      this._txtURL.TabIndex = 3;
      this._txtURL.TextChanged += new System.EventHandler(this._txtURL_TextChanged);
      // 
      // _lnkURL
      // 
      this._lnkURL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this._lnkURL.AutoSize = true;
      this._lnkURL.Location = new System.Drawing.Point(477, 82);
      this._lnkURL.Name = "_lnkURL";
      this._lnkURL.Size = new System.Drawing.Size(28, 13);
      this._lnkURL.TabIndex = 4;
      this._lnkURL.TabStop = true;
      this._lnkURL.Text = "Test";
      this._lnkURL.Visible = false;
      this._lnkURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._lnkURL_LinkClicked);
      // 
      // _lblDirectory
      // 
      this._lblDirectory.AutoSize = true;
      this._lblDirectory.Location = new System.Drawing.Point(24, 111);
      this._lblDirectory.Name = "_lblDirectory";
      this._lblDirectory.Size = new System.Drawing.Size(107, 13);
      this._lblDirectory.TabIndex = 5;
      this._lblDirectory.Text = "Recordings directory:";
      // 
      // _fnbDirectory
      // 
      this._fnbDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this._fnbDirectory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this._fnbDirectory.BasePath = "";
      this._fnbDirectory.Description = "Select the directory where MythTV places its recordings files and thumbnail image" +
          "s.";
      this._fnbDirectory.FlatButton = true;
      this._fnbDirectory.FolderFullName = "";
      this._fnbDirectory.FolderMustExist = true;
      this._fnbDirectory.FolderName = "";
      this._fnbDirectory.LimitBase = false;
      this._fnbDirectory.Location = new System.Drawing.Point(31, 127);
      this._fnbDirectory.MaximumSize = new System.Drawing.Size(32767, 20);
      this._fnbDirectory.MinimumSize = new System.Drawing.Size(50, 20);
      this._fnbDirectory.Name = "_fnbDirectory";
      this._fnbDirectory.Size = new System.Drawing.Size(466, 20);
      this._fnbDirectory.StripBase = false;
      this._fnbDirectory.TabIndex = 6;
      this._fnbDirectory.Changed += new au.util.comctl.FolderChangedHandler(this._fnbDirectory_Changed);
      // 
      // MythServerConfig
      // 
      this.AcceptButton = this._btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(517, 210);
      this.Controls.Add(this._fnbDirectory);
      this.Controls.Add(this._lblDirectory);
      this.Controls.Add(this._lnkURL);
      this.Controls.Add(this._txtURL);
      this.Controls.Add(this._lblURL);
      this.Controls.Add(this._txtServerName);
      this.Controls.Add(this._lblServerName);
      this.Controls.Add(this._btnOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "MythServerConfig";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "MythTV Server Setup";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button _btnOK;
    private System.Windows.Forms.Label _lblServerName;
    private System.Windows.Forms.TextBox _txtServerName;
    private System.Windows.Forms.Label _lblURL;
    private System.Windows.Forms.TextBox _txtURL;
    private System.Windows.Forms.LinkLabel _lnkURL;
    private System.Windows.Forms.Label _lblDirectory;
    private util.comctl.FoldernameBox _fnbDirectory;
    private System.Windows.Forms.ToolTip _tip;
  }
}