namespace au.Applications.MythClient.UI {
	partial class AboutWindow {
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
			System.Windows.Forms.Panel pnlHeading;
			System.Windows.Forms.Label lblTitle;
			System.Windows.Forms.PictureBox picIcon;
			System.Windows.Forms.TextBox txtDescription;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutWindow));
			this._lblVersion = new System.Windows.Forms.Label();
			this._txtCopyright = new System.Windows.Forms.TextBox();
			this._btnOK = new System.Windows.Forms.Button();
			this._lnkURL = new System.Windows.Forms.LinkLabel();
			pnlHeading = new System.Windows.Forms.Panel();
			lblTitle = new System.Windows.Forms.Label();
			picIcon = new System.Windows.Forms.PictureBox();
			txtDescription = new System.Windows.Forms.TextBox();
			pnlHeading.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(picIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlHeading
			// 
			pnlHeading.BackColor = System.Drawing.SystemColors.Window;
			pnlHeading.Controls.Add(this._lblVersion);
			pnlHeading.Controls.Add(lblTitle);
			pnlHeading.Controls.Add(picIcon);
			pnlHeading.Dock = System.Windows.Forms.DockStyle.Top;
			pnlHeading.ForeColor = System.Drawing.SystemColors.WindowText;
			pnlHeading.Location = new System.Drawing.Point(0, 0);
			pnlHeading.Name = "pnlHeading";
			pnlHeading.Size = new System.Drawing.Size(417, 48);
			pnlHeading.TabIndex = 1;
			// 
			// _lblVersion
			// 
			this._lblVersion.Location = new System.Drawing.Point(55, 28);
			this._lblVersion.Name = "_lblVersion";
			this._lblVersion.Size = new System.Drawing.Size(350, 13);
			this._lblVersion.TabIndex = 2;
			this._lblVersion.Text = "Version ";
			// 
			// lblTitle
			// 
			lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			lblTitle.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			lblTitle.Location = new System.Drawing.Point(54, 6);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new System.Drawing.Size(351, 22);
			lblTitle.TabIndex = 3;
			lblTitle.Text = "MythTV Recorded Programs";
			lblTitle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// picIcon
			// 
			picIcon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			picIcon.Image = global::au.Applications.MythClient.UI.Icons.MythTV32;
			picIcon.Location = new System.Drawing.Point(0, 0);
			picIcon.Name = "picIcon";
			picIcon.Padding = new System.Windows.Forms.Padding(6);
			picIcon.Size = new System.Drawing.Size(48, 48);
			picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			picIcon.TabIndex = 1;
			picIcon.TabStop = false;
			// 
			// txtDescription
			// 
			txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
			txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			txtDescription.Location = new System.Drawing.Point(15, 63);
			txtDescription.Multiline = true;
			txtDescription.Name = "txtDescription";
			txtDescription.ReadOnly = true;
			txtDescription.Size = new System.Drawing.Size(390, 81);
			txtDescription.TabIndex = 8;
			txtDescription.TabStop = false;
			txtDescription.Text = resources.GetString("txtDescription.Text");
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
			this._txtCopyright.TabIndex = 7;
			this._txtCopyright.TabStop = false;
			this._txtCopyright.Text = ".\r\nThis program may be distributed freely to anyone.\r\nSource code and updates are" +
		" available at:";
			// 
			// _btnOK
			// 
			this._btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._btnOK.Location = new System.Drawing.Point(330, 202);
			this._btnOK.Name = "_btnOK";
			this._btnOK.Size = new System.Drawing.Size(75, 23);
			this._btnOK.TabIndex = 9;
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
			this._lnkURL.Size = new System.Drawing.Size(245, 16);
			this._lnkURL.TabIndex = 10;
			this._lnkURL.TabStop = true;
			this._lnkURL.Text = "http://www.track7.org/code/vs/mythclient";
			this._lnkURL.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this._lnkURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._lnkURL_LinkClicked);
			// 
			// AboutWindow
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
			this.Controls.Add(txtDescription);
			this.Controls.Add(pnlHeading);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutWindow";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			pnlHeading.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(picIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label _lblVersion;
		private System.Windows.Forms.Button _btnOK;
		private System.Windows.Forms.LinkLabel _lnkURL;
		private System.Windows.Forms.TextBox _txtCopyright;
	}
}