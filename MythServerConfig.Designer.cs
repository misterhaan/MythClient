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
      this._lblDirectory = new System.Windows.Forms.Label();
      this._fnbDirectory = new au.util.comctl.FoldernameBox();
      this._tip = new System.Windows.Forms.ToolTip(this.components);
      this._lnkTest = new System.Windows.Forms.LinkLabel();
      this._lblPort = new System.Windows.Forms.Label();
      this._numPort = new System.Windows.Forms.NumericUpDown();
      ((System.ComponentModel.ISupportInitialize)(this._numPort)).BeginInit();
      this.SuspendLayout();
      // 
      // _btnOK
      // 
      this._btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this._btnOK.Location = new System.Drawing.Point(430, 124);
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
      this._tip.SetToolTip(this._lblServerName, "Enter the hostname or IP address of the MythTV server");
      // 
      // _txtServerName
      // 
      this._txtServerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._txtServerName.Location = new System.Drawing.Point(31, 31);
      this._txtServerName.Name = "_txtServerName";
      this._txtServerName.Size = new System.Drawing.Size(380, 20);
      this._txtServerName.TabIndex = 1;
      this._txtServerName.TextChanged += new System.EventHandler(this._txtServerName_TextChanged);
      // 
      // _lblDirectory
      // 
      this._lblDirectory.AutoSize = true;
      this._lblDirectory.Location = new System.Drawing.Point(24, 63);
      this._lblDirectory.Name = "_lblDirectory";
      this._lblDirectory.Size = new System.Drawing.Size(107, 13);
      this._lblDirectory.TabIndex = 5;
      this._lblDirectory.Text = "Recordings directory:";
      this._tip.SetToolTip(this._lblDirectory, "Directory where MythTV saves its recordings.");
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
      this._fnbDirectory.Location = new System.Drawing.Point(31, 79);
      this._fnbDirectory.MaximumSize = new System.Drawing.Size(32767, 20);
      this._fnbDirectory.MinimumSize = new System.Drawing.Size(50, 20);
      this._fnbDirectory.Name = "_fnbDirectory";
      this._fnbDirectory.Size = new System.Drawing.Size(466, 20);
      this._fnbDirectory.StripBase = false;
      this._fnbDirectory.TabIndex = 6;
      this._fnbDirectory.Changed += new au.util.comctl.FolderChangedHandler(this._fnbDirectory_Changed);
      // 
      // _lnkTest
      // 
      this._lnkTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this._lnkTest.AutoSize = true;
      this._lnkTest.Location = new System.Drawing.Point(477, 34);
      this._lnkTest.Name = "_lnkTest";
      this._lnkTest.Size = new System.Drawing.Size(28, 13);
      this._lnkTest.TabIndex = 10;
      this._lnkTest.TabStop = true;
      this._lnkTest.Text = "Test";
      this._tip.SetToolTip(this._lnkTest, "Launch MythTV services in the default browser to verify the hostname and port.");
      this._lnkTest.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._lnkTest_LinkClicked);
      // 
      // _lblPort
      // 
      this._lblPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this._lblPort.AutoSize = true;
      this._lblPort.Location = new System.Drawing.Point(414, 34);
      this._lblPort.Margin = new System.Windows.Forms.Padding(0);
      this._lblPort.Name = "_lblPort";
      this._lblPort.Size = new System.Drawing.Size(10, 13);
      this._lblPort.TabIndex = 11;
      this._lblPort.Text = ":";
      this._tip.SetToolTip(this._lblPort, "Enter the port used by the MythTV Services API.  Default is 6544.");
      // 
      // _numPort
      // 
      this._numPort.Location = new System.Drawing.Point(427, 31);
      this._numPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
      this._numPort.Name = "_numPort";
      this._numPort.Size = new System.Drawing.Size(50, 20);
      this._numPort.TabIndex = 12;
      this._numPort.ValueChanged += new System.EventHandler(this._numPort_ValueChanged);
      // 
      // MythServerConfig
      // 
      this.AcceptButton = this._btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(517, 159);
      this.Controls.Add(this._numPort);
      this.Controls.Add(this._lblServerName);
      this.Controls.Add(this._txtServerName);
      this.Controls.Add(this._lblPort);
      this.Controls.Add(this._lnkTest);
      this.Controls.Add(this._lblDirectory);
      this.Controls.Add(this._fnbDirectory);
      this.Controls.Add(this._btnOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "MythServerConfig";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "MythTV Server Setup";
      ((System.ComponentModel.ISupportInitialize)(this._numPort)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button _btnOK;
    private System.Windows.Forms.TextBox _txtServerName;
    private util.comctl.FoldernameBox _fnbDirectory;
    private System.Windows.Forms.ToolTip _tip;
    private System.Windows.Forms.LinkLabel _lnkTest;
    private System.Windows.Forms.NumericUpDown _numPort;
    private System.Windows.Forms.Label _lblServerName;
    private System.Windows.Forms.Label _lblDirectory;
    private System.Windows.Forms.Label _lblPort;
  }
}