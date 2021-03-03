using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace au.Applications.MythClient.UI {
	/// <summary>
	/// Window showing application details.
	/// </summary>
	public partial class AboutWindow : Form {
		/// <summary>
		/// Default constructor
		/// </summary>
		public AboutWindow() {
			InitializeComponent();
			_txtCopyright.Text = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).LegalCopyright + _txtCopyright.Text;
			_lblVersion.Text += Application.ProductVersion;
		}

		private void _lnkURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
			=> Process.Start(new ProcessStartInfo(_lnkURL.Text) { UseShellExecute = true });
	}
}
