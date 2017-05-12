using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace au.Applications.MythClient {
	public partial class AboutMythClient : Form {
		public AboutMythClient() {
			InitializeComponent();
		}

		private void AboutMythClient_Load(object sender, EventArgs e) {
			_picIcon.Image = Properties.Resources.MythTV32;
			_lblVersion.Text += Application.ProductVersion;
		}

		private void _lnkURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			Process.Start(_lnkURL.Text);
		}
	}
}
