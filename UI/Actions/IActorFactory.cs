using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;
using au.Applications.MythClient.Settings.Types;

namespace au.Applications.MythClient.UI.Actions {
	/// <summary>
	/// ActorFactory classes can create objects that can act on recordings.
	/// </summary>
	internal interface IActorFactory {
		/// <summary>
		/// Create the recordings deleter object.
		/// </summary>
		/// <param name="owner">Owner window for dialogs</param>
		/// <param name="recordings">Collection of recordings</param>
		/// <param name="navigator"></param>
		/// <returns>Recordings deleter object</returns>
		IRecordingsDeleter BuildRecordingsDeleter(IWin32Window owner, IRecordings recordings, RecordingsNavigator navigator);

		/// <summary>
		/// Create the recordings exporter object.
		/// </summary>
		/// <param name="owner">Owner window for dialogs</param>
		/// <param name="settings">Application settings</param>
		/// <returns>Recordings exporter object</returns>
		IRecordingsExporter BuildRecordingsExporter(IWin32Window owner, IMythSettings settings);
	}
}