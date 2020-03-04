namespace au.Applications.MythClient.UI {
	partial class RecordingsWindow {
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
			System.Windows.Forms.Panel pnlHead;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordingsWindow));
			this._btnMenu = new System.Windows.Forms.Button();
			this._btnSort = new System.Windows.Forms.Button();
			this._lblTitle = new System.Windows.Forms.Label();
			this._btnReload = new System.Windows.Forms.Button();
			this._btnBack = new System.Windows.Forms.Button();
			this._pnlInfo = new System.Windows.Forms.FlowLayoutPanel();
			this._tip = new System.Windows.Forms.ToolTip(this.components);
			this._pnlContents = new System.Windows.Forms.FlowLayoutPanel();
			this._cmnuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
			this._cmnuMainSettings = new System.Windows.Forms.ToolStripMenuItem();
			this._cmnuMainCheckForUpdate = new System.Windows.Forms.ToolStripMenuItem();
			this._cmnuMainAbout = new System.Windows.Forms.ToolStripMenuItem();
			this._cmnuSort = new System.Windows.Forms.ContextMenuStrip(this.components);
			this._cmnuSortDate = new System.Windows.Forms.ToolStripMenuItem();
			this._cmnuSortTitle = new System.Windows.Forms.ToolStripMenuItem();
			pnlHead = new System.Windows.Forms.Panel();
			pnlHead.SuspendLayout();
			this._cmnuMain.SuspendLayout();
			this._cmnuSort.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlHead
			// 
			pnlHead.BackColor = System.Drawing.SystemColors.Window;
			pnlHead.Controls.Add(this._btnMenu);
			pnlHead.Controls.Add(this._btnSort);
			pnlHead.Controls.Add(this._lblTitle);
			pnlHead.Controls.Add(this._btnReload);
			pnlHead.Controls.Add(this._btnBack);
			pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
			pnlHead.ForeColor = System.Drawing.SystemColors.WindowText;
			pnlHead.Location = new System.Drawing.Point(0, 0);
			pnlHead.Name = "pnlHead";
			pnlHead.Size = new System.Drawing.Size(942, 40);
			pnlHead.TabIndex = 0;
			// 
			// _btnMenu
			// 
			this._btnMenu.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this._btnMenu.FlatAppearance.BorderColor = System.Drawing.SystemColors.Window;
			this._btnMenu.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this._btnMenu.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this._btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._btnMenu.Image = global::au.Applications.MythClient.UI.Icons.Material_Menu24;
			this._btnMenu.Location = new System.Drawing.Point(906, 4);
			this._btnMenu.Margin = new System.Windows.Forms.Padding(0, 4, 4, 4);
			this._btnMenu.Name = "_btnMenu";
			this._btnMenu.Size = new System.Drawing.Size(32, 32);
			this._btnMenu.TabIndex = 4;
			this._btnMenu.TabStop = false;
			this._tip.SetToolTip(this._btnMenu, "More options");
			this._btnMenu.Click += new System.EventHandler(this._btnMenu_Click);
			this._btnMenu.MouseEnter += new System.EventHandler(this._btnMenu_MouseEnter);
			this._btnMenu.MouseLeave += new System.EventHandler(this._btnMenu_MouseLeave);
			// 
			// _btnSort
			// 
			this._btnSort.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this._btnSort.FlatAppearance.BorderColor = System.Drawing.SystemColors.Window;
			this._btnSort.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this._btnSort.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this._btnSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._btnSort.Image = global::au.Applications.MythClient.UI.Icons.Material_SortTitle24;
			this._btnSort.Location = new System.Drawing.Point(870, 4);
			this._btnSort.Margin = new System.Windows.Forms.Padding(0, 4, 4, 4);
			this._btnSort.Name = "_btnSort";
			this._btnSort.Size = new System.Drawing.Size(32, 32);
			this._btnSort.TabIndex = 3;
			this._btnSort.TabStop = false;
			this._tip.SetToolTip(this._btnSort, "Sort options");
			this._btnSort.Click += new System.EventHandler(this._btnSort_Click);
			this._btnSort.MouseEnter += new System.EventHandler(this._btnSort_MouseEnter);
			this._btnSort.MouseLeave += new System.EventHandler(this._btnSort_MouseLeave);
			// 
			// _lblTitle
			// 
			this._lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._lblTitle.AutoEllipsis = true;
			this._lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._lblTitle.Location = new System.Drawing.Point(75, 0);
			this._lblTitle.Name = "_lblTitle";
			this._lblTitle.Size = new System.Drawing.Size(792, 40);
			this._lblTitle.TabIndex = 2;
			this._lblTitle.Text = "MythTV Recorded Programs";
			this._lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._lblTitle.UseMnemonic = false;
			// 
			// _btnReload
			// 
			this._btnReload.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this._btnReload.FlatAppearance.BorderColor = System.Drawing.SystemColors.Window;
			this._btnReload.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this._btnReload.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this._btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._btnReload.Image = global::au.Applications.MythClient.UI.Icons.Material_Reload24;
			this._btnReload.Location = new System.Drawing.Point(40, 4);
			this._btnReload.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
			this._btnReload.Name = "_btnReload";
			this._btnReload.Size = new System.Drawing.Size(32, 32);
			this._btnReload.TabIndex = 1;
			this._btnReload.TabStop = false;
			this._tip.SetToolTip(this._btnReload, "Reload recording list from MythTV");
			this._btnReload.EnabledChanged += new System.EventHandler(this._btnReload_EnabledChanged);
			this._btnReload.Click += new System.EventHandler(this._btnReload_ClickAsync);
			this._btnReload.MouseEnter += new System.EventHandler(this._btnReload_MouseEnter);
			this._btnReload.MouseLeave += new System.EventHandler(this._btnReload_MouseLeave);
			// 
			// _btnBack
			// 
			this._btnBack.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this._btnBack.Enabled = false;
			this._btnBack.FlatAppearance.BorderColor = System.Drawing.SystemColors.Window;
			this._btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this._btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this._btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._btnBack.Image = global::au.Applications.MythClient.UI.Icons.Material_BackDisabled24;
			this._btnBack.Location = new System.Drawing.Point(4, 4);
			this._btnBack.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
			this._btnBack.Name = "_btnBack";
			this._btnBack.Size = new System.Drawing.Size(32, 32);
			this._btnBack.TabIndex = 0;
			this._btnBack.TabStop = false;
			this._btnBack.EnabledChanged += new System.EventHandler(this._btnBack_EnabledChanged);
			this._btnBack.Click += new System.EventHandler(this._btnBack_Click);
			this._btnBack.MouseEnter += new System.EventHandler(this._btnBack_MouseEnter);
			this._btnBack.MouseLeave += new System.EventHandler(this._btnBack_MouseLeave);
			// 
			// _pnlInfo
			// 
			this._pnlInfo.Dock = System.Windows.Forms.DockStyle.Right;
			this._pnlInfo.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this._pnlInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._pnlInfo.Location = new System.Drawing.Point(714, 40);
			this._pnlInfo.Name = "_pnlInfo";
			this._pnlInfo.Size = new System.Drawing.Size(228, 478);
			this._pnlInfo.TabIndex = 1;
			this._pnlInfo.WrapContents = false;
			this._pnlInfo.ControlAdded += new System.Windows.Forms.ControlEventHandler(this._pnlInfo_ControlAdded);
			// 
			// _pnlContents
			// 
			this._pnlContents.AutoScroll = true;
			this._pnlContents.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this._pnlContents.Dock = System.Windows.Forms.DockStyle.Fill;
			this._pnlContents.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._pnlContents.Location = new System.Drawing.Point(0, 40);
			this._pnlContents.Name = "_pnlContents";
			this._pnlContents.Padding = new System.Windows.Forms.Padding(15);
			this._pnlContents.Size = new System.Drawing.Size(714, 478);
			this._pnlContents.TabIndex = 2;
			// 
			// _cmnuMain
			// 
			this._cmnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
						this._cmnuMainSettings,
						this._cmnuMainCheckForUpdate,
						this._cmnuMainAbout});
			this._cmnuMain.Name = "_cmnuMain";
			this._cmnuMain.Size = new System.Drawing.Size(181, 92);
			// 
			// _cmnuMainSettings
			// 
			this._cmnuMainSettings.Image = global::au.Applications.MythClient.UI.Icons.Material_Settings18;
			this._cmnuMainSettings.Name = "_cmnuMainSettings";
			this._cmnuMainSettings.Size = new System.Drawing.Size(175, 22);
			this._cmnuMainSettings.Text = "&Settings...";
			this._cmnuMainSettings.Click += new System.EventHandler(this._cmnuMainSettings_ClickAsync);
			// 
			// _cmnuMainCheckForUpdate
			// 
			this._cmnuMainCheckForUpdate.Image = global::au.Applications.MythClient.UI.Icons.Material_Update18;
			this._cmnuMainCheckForUpdate.Name = "_cmnuMainCheckForUpdate";
			this._cmnuMainCheckForUpdate.Size = new System.Drawing.Size(180, 22);
			this._cmnuMainCheckForUpdate.Text = "Check for &Update...";
			this._cmnuMainCheckForUpdate.Click += new System.EventHandler(this._cmnuMainCheckForUpdate_Click);
			// 
			// _cmnuMainAbout
			// 
			this._cmnuMainAbout.Image = global::au.Applications.MythClient.UI.Icons.Material_About18;
			this._cmnuMainAbout.Name = "_cmnuMainAbout";
			this._cmnuMainAbout.Size = new System.Drawing.Size(175, 22);
			this._cmnuMainAbout.Text = "&About";
			this._cmnuMainAbout.Click += new System.EventHandler(this._cmnuMainAbout_Click);
			// 
			// _cmnuSort
			// 
			this._cmnuSort.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
						this._cmnuSortDate,
						this._cmnuSortTitle});
			this._cmnuSort.Name = "_cmnuSort";
			this._cmnuSort.Size = new System.Drawing.Size(143, 48);
			// 
			// _cmnuSortDate
			// 
			this._cmnuSortDate.Image = global::au.Applications.MythClient.UI.Icons.Material_Date18;
			this._cmnuSortDate.Name = "_cmnuSortDate";
			this._cmnuSortDate.Size = new System.Drawing.Size(142, 22);
			this._cmnuSortDate.Text = "&Earliest Aired";
			this._cmnuSortDate.Click += new System.EventHandler(this._cmnuSortDate_ClickAsync);
			// 
			// _cmnuSortTitle
			// 
			this._cmnuSortTitle.Checked = true;
			this._cmnuSortTitle.CheckState = System.Windows.Forms.CheckState.Checked;
			this._cmnuSortTitle.Image = global::au.Applications.MythClient.UI.Icons.Material_Title18;
			this._cmnuSortTitle.Name = "_cmnuSortTitle";
			this._cmnuSortTitle.Size = new System.Drawing.Size(142, 22);
			this._cmnuSortTitle.Text = "&Title";
			this._cmnuSortTitle.Click += new System.EventHandler(this._cmnuSortTitle_ClickAsync);
			// 
			// RecordingsWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(942, 518);
			this.Controls.Add(this._pnlContents);
			this.Controls.Add(this._pnlInfo);
			this.Controls.Add(pnlHead);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(516, 443);
			this.Name = "RecordingsWindow";
			this.Text = "MythTV Recorded Programs";
			this.Load += new System.EventHandler(this.RecordingsWindow_Load);
			this.Shown += new System.EventHandler(this.RecordingsWindow_ShownAsync);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RecordingsWindow_KeyDown);
			pnlHead.ResumeLayout(false);
			this._cmnuMain.ResumeLayout(false);
			this._cmnuSort.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button _btnBack;
		private System.Windows.Forms.Button _btnReload;
		private System.Windows.Forms.Label _lblTitle;
		private System.Windows.Forms.Button _btnSort;
		private System.Windows.Forms.ToolTip _tip;
		private System.Windows.Forms.Button _btnMenu;
		private System.Windows.Forms.FlowLayoutPanel _pnlContents;
		private System.Windows.Forms.ContextMenuStrip _cmnuMain;
		private System.Windows.Forms.ToolStripMenuItem _cmnuMainSettings;
		private System.Windows.Forms.ToolStripMenuItem _cmnuMainCheckForUpdate;
		private System.Windows.Forms.ToolStripMenuItem _cmnuMainAbout;
		private System.Windows.Forms.ContextMenuStrip _cmnuSort;
		private System.Windows.Forms.ToolStripMenuItem _cmnuSortDate;
		private System.Windows.Forms.ToolStripMenuItem _cmnuSortTitle;
		private System.Windows.Forms.FlowLayoutPanel _pnlInfo;
	}
}