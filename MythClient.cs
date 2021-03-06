﻿using System;
using System.Windows.Forms;
using au.Applications.MythClient.Settings;
using au.Applications.MythClient.Settings.Types;
using au.Applications.MythClient.UI;
using au.IO.Web.API.MythTV;
using au.UI.LatestVersion;

namespace au.Applications.MythClient {
	/// <summary>
	/// MythTV Recorded Programs application
	/// </summary>
	internal class MythClient {
		/// <summary>
		/// Main entry point
		/// </summary>
		[STAThread]
		private static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			SettingsManager settingsManager = new();

			if(PromptForRequiredSettings(settingsManager.Settings))
				Application.Run(
					new RecordingsWindow(
						settingsManager.Settings,
						new VersionManager("misterhaan", "MythClient"),
						new Recordings.Recordings(
							settingsManager.Settings,
							new ApiFactory()
						)
					)
				);

			settingsManager.Save();
		}

		/// <summary>
		/// Check if required settings have values and prompt for them if not.
		/// </summary>
		/// <param name="settings">Current application settings</param>
		/// <returns>True if required settings have values</returns>
		private static bool PromptForRequiredSettings(IMythSettings settings) {
			if(!settings.HasRequiredSettings)
				using(SettingsWindow window = new(settings.Server))
					window.ShowDialog();
			return settings.HasRequiredSettings;
		}
	}
}
