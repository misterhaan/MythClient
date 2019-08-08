namespace au.Applications.MythClient.UI {
	partial class SettingsWindow {
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
			System.Windows.Forms.Label lblServerName;
			System.Windows.Forms.ToolTip tip;
			System.Windows.Forms.Label lblPort;
			System.Windows.Forms.Label lblDirectory;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
			this._txtServerName = new System.Windows.Forms.TextBox();
			this._numPort = new System.Windows.Forms.NumericUpDown();
			this._lnkTest = new System.Windows.Forms.LinkLabel();
			this._dirRecordings = new au.UI.DirectoryBox.DirectoryBox();
			this._btnOK = new System.Windows.Forms.Button();
			this._btnCancel = new System.Windows.Forms.Button();
			lblServerName = new System.Windows.Forms.Label();
			tip = new System.Windows.Forms.ToolTip(this.components);
			lblPort = new System.Windows.Forms.Label();
			lblDirectory = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this._numPort)).BeginInit();
			this.SuspendLayout();
			// 
			// lblServerName
			// 
			lblServerName.AutoSize = true;
			lblServerName.Location = new System.Drawing.Point(12, 15);
			lblServerName.Name = "lblServerName";
			lblServerName.Size = new System.Drawing.Size(79, 13);
			lblServerName.TabIndex = 1;
			lblServerName.Text = "MythTV server:";
			tip.SetToolTip(lblServerName, "Enter the hostname or IP address of the MythTV server");
			// 
			// _txtServerName
			// 
			this._txtServerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._txtServerName.Location = new System.Drawing.Point(12, 31);
			this._txtServerName.Name = "_txtServerName";
			this._txtServerName.Size = new System.Drawing.Size(393, 20);
			this._txtServerName.TabIndex = 2;
			tip.SetToolTip(this._txtServerName, "Enter the hostname or IP address of the MythTV server");
			// 
			// lblPort
			// 
			lblPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			lblPort.AutoSize = true;
			lblPort.Location = new System.Drawing.Point(408, 33);
			lblPort.Margin = new System.Windows.Forms.Padding(0);
			lblPort.Name = "lblPort";
			lblPort.Size = new System.Drawing.Size(10, 13);
			lblPort.TabIndex = 3;
			lblPort.Text = ":";
			tip.SetToolTip(lblPort, "Enter the port used by the MythTV Services API.  Default is 6544.");
			// 
			// lblDirectory
			// 
			lblDirectory.AutoSize = true;
			lblDirectory.Location = new System.Drawing.Point(12, 63);
			lblDirectory.Name = "lblDirectory";
			lblDirectory.Size = new System.Drawing.Size(107, 13);
			lblDirectory.TabIndex = 6;
			lblDirectory.Text = "Recordings directory:";
			tip.SetToolTip(lblDirectory, "Directory where MythTV saves its recordings.");
			// 
			// _numPort
			// 
			this._numPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._numPort.Location = new System.Drawing.Point(421, 31);
			this._numPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this._numPort.Name = "_numPort";
			this._numPort.Size = new System.Drawing.Size(50, 20);
			this._numPort.TabIndex = 4;
			tip.SetToolTip(this._numPort, "Enter the port used by the MythTV Services API.  Default is 6544.");
			// 
			// _lnkTest
			// 
			this._lnkTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._lnkTest.AutoSize = true;
			this._lnkTest.Location = new System.Drawing.Point(477, 34);
			this._lnkTest.Name = "_lnkTest";
			this._lnkTest.Size = new System.Drawing.Size(28, 13);
			this._lnkTest.TabIndex = 5;
			this._lnkTest.TabStop = true;
			this._lnkTest.Text = "Test";
			tip.SetToolTip(this._lnkTest, "Launch MythTV services in the default browser to verify the hostname and port.");
			this._lnkTest.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._lnkTest_LinkClicked);
			// 
			// _dirRecordings
			// 
			this._dirRecordings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._dirRecordings.BasePath = null;
			this._dirRecordings.Description = "Select the directory where MythTV places its recordings files and thumbnail image" +
    "s.";
			this._dirRecordings.Location = new System.Drawing.Point(12, 79);
			this._dirRecordings.Name = "_dirRecordings";
			this._dirRecordings.Size = new System.Drawing.Size(493, 20);
			this._dirRecordings.TabIndex = 7;
			tip.SetToolTip(this._dirRecordings, "Directory where MythTV saves its recordings.");
			// 
			// _btnOK
			// 
			this._btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this._btnOK.Location = new System.Drawing.Point(349, 114);
			this._btnOK.Name = "_btnOK";
			this._btnOK.Size = new System.Drawing.Size(75, 23);
			this._btnOK.TabIndex = 8;
			this._btnOK.Text = "OK";
			this._btnOK.UseVisualStyleBackColor = true;
			this._btnOK.Click += new System.EventHandler(this._btnOK_Click);
			// 
			// _btnCancel
			// 
			this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._btnCancel.Location = new System.Drawing.Point(430, 114);
			this._btnCancel.Name = "_btnCancel";
			this._btnCancel.Size = new System.Drawing.Size(75, 23);
			this._btnCancel.TabIndex = 9;
			this._btnCancel.Text = "Cancel";
			this._btnCancel.UseVisualStyleBackColor = true;
			// 
			// SettingsWindow
			// 
			this.AcceptButton = this._btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._btnCancel;
			this.ClientSize = new System.Drawing.Size(517, 149);
			this.Controls.Add(this._btnCancel);
			this.Controls.Add(this._btnOK);
			this.Controls.Add(this._dirRecordings);
			this.Controls.Add(lblDirectory);
			this.Controls.Add(this._lnkTest);
			this.Controls.Add(this._numPort);
			this.Controls.Add(lblPort);
			this.Controls.Add(this._txtServerName);
			this.Controls.Add(lblServerName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "SettingsWindow";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "MythTV Server Setup";
			this.Shown += new System.EventHandler(this.SettingsWindow_Shown);
			((System.ComponentModel.ISupportInitialize)(this._numPort)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox _txtServerName;
		private System.Windows.Forms.NumericUpDown _numPort;
		private System.Windows.Forms.LinkLabel _lnkTest;
		private au.UI.DirectoryBox.DirectoryBox _dirRecordings;
		private System.Windows.Forms.Button _btnOK;
		private System.Windows.Forms.Button _btnCancel;
	}
}