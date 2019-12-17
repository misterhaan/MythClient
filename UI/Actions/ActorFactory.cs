using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;
using au.Applications.MythClient.Settings.Types;

namespace au.Applications.MythClient.UI.Actions {
	/// <summary>
	/// Creates objects that can act on recordings.
	/// </summary>
	internal class ActorFactory : IActorFactory {
		/// <inheritdoc />
		public IRecordingsDeleter BuildRecordingsDeleter(IWin32Window owner, IRecordings recordings, RecordingsNavigator navigator)
			=> new RecordingsDeleter(owner, recordings, navigator);

		/// <inheritdoc />
		public IRecordingsExporter BuildRecordingsExporter(IWin32Window owner, IMythSettings settings)
			=> new RecordingsExporter(owner, settings);
	}
}
