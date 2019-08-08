using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using au.Applications.MythClient.Settings.Types;
using au.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.Applications.MythClient.Settings.Tests {
	[TestClass]
	public class MythSettingsTests {
		private const string _pathOnFakeDrive = @"W:\drive\does\not\exist";

		[TestMethod]
		public void MainForm_NonNull() {
			IMythSettings settings = GetSettings();

			Assert.IsNotNull(settings.MainForm, $"{nameof(settings.MainForm)} should be initialized to a default value.");
		}

		[TestMethod]
		public void Server_NonNull() {
			IMythSettings settings = GetSettings();

			Assert.IsNotNull(settings.Server, $"{nameof(settings.Server)} should be initialized to a default value.");
		}

		[TestMethod]
		public void RecordingSortOption_DefaultTitle() {
			MythSettings settings = GetSettings();

			RecordingSortOption sortOption = settings.RecordingSortOption;

			Assert.AreEqual(RecordingSortOption.Title, sortOption);
		}

		#region LastExportDirectory
		[TestMethod]
		public void LastExportDirectory_Exists_SetEqual() {
			MythSettings settings = GetSettings();
			string tmpPath = Path.GetTempPath();

			settings.LastExportDirectory = tmpPath;

			Assert.IsTrue(Directory.Exists(tmpPath), "Test requires that the temporary path exists.");
			PathAssert.AreSame(tmpPath, settings.LastExportDirectory, $"{nameof(settings.LastExportDirectory)} should be the same path as it was set to when it exists.");
		}

		[DataTestMethod]
		[DataRow("doesNotExist")]
		[DataRow(@"does\not\exist")]
		public void LastExportDirectory_ParentExists_SetParent(string subdirs) {
			MythSettings settings = GetSettings();
			string tmpPath = Path.GetTempPath();
			string childPath = Path.Combine(tmpPath, subdirs);

			settings.LastExportDirectory = childPath;

			Assert.IsTrue(Directory.Exists(tmpPath), "Test requires that the temporary path exists.");
			Assert.IsFalse(Directory.Exists(Path.Combine(tmpPath, subdirs.Split('\\')[0])), "Test requires that none of the child path subdirectories exist.");
			PathAssert.AreSame(tmpPath, settings.LastExportDirectory, $"{nameof(settings.LastExportDirectory)} should be the first existing parent of the value it was set to.");
		}

		[TestMethod]
		public void LastExportDirectory_NoParentExists_Unchanged() {
			MythSettings settings = GetSettings();
			string defaultPath = settings.LastExportDirectory;

			settings.LastExportDirectory = _pathOnFakeDrive;

			Assert.IsFalse(Directory.Exists(Path.GetPathRoot(_pathOnFakeDrive)), $"Test requires that there is no {_pathOnFakeDrive[0]} drive.");
			PathAssert.AreSame(defaultPath, settings.LastExportDirectory, $"{nameof(settings.LastExportDirectory)} should not change when set to a path on a drive that doesn't exist.");
		}
		#endregion LastExportDirectory

		[TestMethod]
		public void SaveLoad_PropertiesMatch() {
			FileInfo file = new FileInfo(Path.Combine(Path.GetTempPath(), $"{nameof(MythSettingsTests)}.{nameof(SaveLoad_PropertiesMatch)}.test"));
			try {
				SettingsFileManager<MythSettings> saveManager = new SettingsFileManager<MythSettings>(file);
				saveManager.Settings.RecordingSortOption = RecordingSortOption.OldestRecorded;
				saveManager.Settings.LastExportDirectory = Environment.CurrentDirectory;
				saveManager.Settings.Server = new ServerSettings {
					Name = "mythtv",
					Port = 12345,
					RawFilesDirectory = @"\\mythtv\recordings"
				};
				saveManager.Settings.MainForm = new FormGeometrySettings {
					Location = new Point(1, 2),
					Size = new Size(1280, 720),
					WindowState = FormWindowState.Maximized
				};

				saveManager.Save();
				MythSettings loadedSettings = new SettingsFileManager<MythSettings>(file).Settings;

				Assert.AreEqual(saveManager.Settings.RecordingSortOption, loadedSettings.RecordingSortOption, $"{nameof(loadedSettings.RecordingSortOption)} should be the same after saving and loading.");
				Assert.AreEqual(saveManager.Settings.LastExportDirectory, loadedSettings.LastExportDirectory, $"{nameof(loadedSettings.LastExportDirectory)} should be the same after saving and loading.");
				Assert.AreEqual(saveManager.Settings.Server.Name, loadedSettings.Server.Name, $"{nameof(loadedSettings.Server)}.{nameof(loadedSettings.Server.Name)} should be the same after saving and loading.");
				Assert.AreEqual(saveManager.Settings.Server.Port, loadedSettings.Server.Port, $"{nameof(loadedSettings.Server)}.{nameof(loadedSettings.Server.Port)} should be the same after saving and loading.");
				Assert.AreEqual(saveManager.Settings.Server.RawFilesDirectory, loadedSettings.Server.RawFilesDirectory, $"{nameof(loadedSettings.Server)}.{nameof(loadedSettings.Server.RawFilesDirectory)} should be the same after saving and loading.");
				Assert.AreEqual(saveManager.Settings.MainForm.Location, loadedSettings.MainForm.Location, $"{nameof(loadedSettings.MainForm)}.{nameof(loadedSettings.MainForm.Location)} should be the same after saving and loading.");
				Assert.AreEqual(saveManager.Settings.MainForm.Size, loadedSettings.MainForm.Size, $"{nameof(loadedSettings.MainForm)}.{nameof(loadedSettings.MainForm.Size)} should be the same after saving and loading.");
				Assert.AreEqual(saveManager.Settings.MainForm.WindowState, loadedSettings.MainForm.WindowState, $"{nameof(loadedSettings.MainForm)}.{nameof(loadedSettings.MainForm.WindowState)} should be the same after saving and loading.");
			} finally {
				try { file.Delete(); } catch { }
			}
		}

		#region HasRequiredSettings
		[DataTestMethod]
		[DataRow(null)]
		[DataRow("")]
		public void HasRequiredSettings_NoServerName_False(string serverName) {
			MythSettings settings = GetSettings();
			settings.Server.RawFilesDirectory = @"C:\";

			settings.Server.Name = serverName;

			Assert.IsFalse(settings.HasRequiredSettings, $"{nameof(settings.HasRequiredSettings)} should be false when {settings.Server}.{settings.Server.Name} is not specified.");
		}

		[DataTestMethod]
		[DataRow(null)]
		[DataRow("")]
		public void HasRequiredSettings_NoRawFilesDirectory_False(string directory) {
			MythSettings settings = GetSettings();
			settings.Server.Name = "localhost";

			settings.Server.RawFilesDirectory = directory;

			Assert.IsFalse(settings.HasRequiredSettings, $"{nameof(settings.HasRequiredSettings)} should be false when {settings.Server}.{settings.Server.RawFilesDirectory} is not specified.");
		}

		[TestMethod]
		public void HasRequiredSettings_RawFilesDirectoryDoesNotExist_False() {
			MythSettings settings = GetSettings();
			settings.Server.Name = "localhost";

			settings.Server.RawFilesDirectory = @"C:\nope\this\is\not\here";

			Assert.IsFalse(settings.HasRequiredSettings, $"{nameof(settings.HasRequiredSettings)} should be false when {settings.Server}.{settings.Server.RawFilesDirectory} is a directory that does not exist.");
		}

		[TestMethod]
		public void HasRequiredSettings_NameAndRawFilesDirectoryOkay_True() {
			MythSettings settings = GetSettings();
			settings.Server.Name = "localhost";
			settings.Server.RawFilesDirectory = @"C:\";

			Assert.IsTrue(settings.HasRequiredSettings, $"{nameof(settings.HasRequiredSettings)} should be true when {settings.Server}.{settings.Server.Name} is specified and {settings.Server}.{settings.Server.RawFilesDirectory} is a directory that exists.");
		}
		#endregion HasRequiredSettings

		private MythSettings GetSettings() {
			return new MythSettings();
		}

		private static class PathAssert {
			public static void AreSame(string expectedPath, string actualPath, string message) {
				Assert.AreEqual(Path.GetFullPath(expectedPath).TrimEnd(Path.DirectorySeparatorChar), Path.GetFullPath(actualPath).TrimEnd(Path.DirectorySeparatorChar), true, CultureInfo.InvariantCulture, message);  // should be ordinal not invariant, but that's not an option
			}
		}
	}
}
