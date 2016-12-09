namespace au.Applications.MythClient {
  partial class AboutMythClient {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutMythClient));
      this.panel1 = new System.Windows.Forms.Panel();
      this._lblVersion = new System.Windows.Forms.Label();
      this._lblTitle = new System.Windows.Forms.Label();
      this._picIcon = new System.Windows.Forms.PictureBox();
      this._btnOK = new System.Windows.Forms.Button();
      this._lnkURL = new System.Windows.Forms.LinkLabel();
      this._txtCopyright = new System.Windows.Forms.TextBox();
      this._txtDescription = new System.Windows.Forms.TextBox();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this._picIcon)).BeginInit();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.SystemColors.Window;
      this.panel1.Controls.Add(this._lblVersion);
      this.panel1.Controls.Add(this._lblTitle);
      this.panel1.Controls.Add(this._picIcon);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.ForeColor = System.Drawing.SystemColors.WindowText;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(417, 48);
      this.panel1.TabIndex = 0;
      // 
      // _lblVersion
      // 
      this._lblVersion.Location = new System.Drawing.Point(55, 28);
      this._lblVersion.Name = "_lblVersion";
      this._lblVersion.Size = new System.Drawing.Size(350, 13);
      this._lblVersion.TabIndex = 2;
      this._lblVersion.Text = "Version ";
      // 
      // _lblTitle
      // 
      this._lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._lblTitle.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._lblTitle.Location = new System.Drawing.Point(54, 6);
      this._lblTitle.Name = "_lblTitle";
      this._lblTitle.Size = new System.Drawing.Size(351, 22);
      this._lblTitle.TabIndex = 3;
      this._lblTitle.Text = "MythTV Recorded Programs";
      this._lblTitle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
      // 
      // _picIcon
      // 
      this._picIcon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this._picIcon.Location = new System.Drawing.Point(0, 0);
      this._picIcon.Name = "_picIcon";
      this._picIcon.Padding = new System.Windows.Forms.Padding(6);
      this._picIcon.Size = new System.Drawing.Size(48, 48);
      this._picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this._picIcon.TabIndex = 1;
      this._picIcon.TabStop = false;
      // 
      // _btnOK
      // 
      this._btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this._btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this._btnOK.Location = new System.Drawing.Point(330, 202);
      this._btnOK.Name = "_btnOK";
      this._btnOK.Size = new System.Drawing.Size(75, 23);
      this._btnOK.TabIndex = 5;
      this._btnOK.Text = "OK";
      this._btnOK.UseVisualStyleBackColor = true;
      // 
      // _lnkURL
      // 
      this._lnkURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._lnkURL.AutoSize = true;
      this._lnkURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._lnkURL.Location = new System.Drawing.Point(12, 209);
      this._lnkURL.Name = "_lnkURL";
      this._lnkURL.Size = new System.Drawing.Size(271, 16);
      this._lnkURL.TabIndex = 6;
      this._lnkURL.TabStop = true;
      this._lnkURL.Text = "http://www.track7.org/analogu/net/mythclient/";
      this._lnkURL.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this._lnkURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._lnkURL_LinkClicked);
      // 
      // _txtCopyright
      // 
      this._txtCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._txtCopyright.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this._txtCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._txtCopyright.Location = new System.Drawing.Point(15, 161);
      this._txtCopyright.Multiline = true;
      this._txtCopyright.Name = "_txtCopyright";
      this._txtCopyright.ReadOnly = true;
      this._txtCopyright.Size = new System.Drawing.Size(300, 51);
      this._txtCopyright.TabIndex = 3;
      this._txtCopyright.TabStop = false;
      this._txtCopyright.Text = "© 2012 - 2016 the analog underground.\r\nThis program may be distributed freely to " +
    "anyone.\r\nSource code and updates are available at:";
      // 
      // _txtDescription
      // 
      this._txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this._txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._txtDescription.Location = new System.Drawing.Point(15, 63);
      this._txtDescription.Multiline = true;
      this._txtDescription.Name = "_txtDescription";
      this._txtDescription.ReadOnly = true;
      this._txtDescription.Size = new System.Drawing.Size(390, 81);
      this._txtDescription.TabIndex = 4;
      this._txtDescription.TabStop = false;
      this._txtDescription.Text = resources.GetString("_txtDescription.Text");
      // 
      // AboutMythClient
      // 
      this.AcceptButton = this._btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this._btnOK;
      this.ClientSize = new System.Drawing.Size(417, 237);
      this.ControlBox = false;
      this.Controls.Add(this._btnOK);
      this.Controls.Add(this._lnkURL);
      this.Controls.Add(this._txtCopyright);
      this.Controls.Add(this._txtDescription);
      this.Controls.Add(this.panel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "AboutMythClient";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Load += new System.EventHandler(this.AboutMythClient_Load);
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this._picIcon)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.PictureBox _picIcon;
    private System.Windows.Forms.Label _lblVersion;
    private System.Windows.Forms.Label _lblTitle;
    private System.Windows.Forms.Button _btnOK;
    private System.Windows.Forms.LinkLabel _lnkURL;
    private System.Windows.Forms.TextBox _txtCopyright;
    private System.Windows.Forms.TextBox _txtDescription;
  }
}