using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.Applications.MythClient.Settings.Tests {
	[TestClass]
	public class ServerSettingsTests {
		[TestMethod]
		public void Port_DefaultPort() {
			ServerSettings settings = GetSettings();

			ushort port = settings.Port;

			Assert.AreEqual(ServerSettings.DefaultPort, port, $"{nameof(settings.Port)} should have a default value of {ServerSettings.DefaultPort}.");
		}

		private static ServerSettings GetSettings()
			=> new ServerSettings();
	}
}
