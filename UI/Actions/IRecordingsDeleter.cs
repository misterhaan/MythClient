using System.Threading.Tasks;
using au.Applications.MythClient.Recordings.Types;

namespace au.Applications.MythClient.UI.Actions {
	/// <summary>
	/// Handles deletion of recordings.
	/// </summary>
	internal interface IRecordingsDeleter {
		/// <summary>
		/// Delete a single recording.
		/// </summary>
		/// <param name="episode">Episode to delete</param>
		Task DeleteAsync(IEpisode episode);

		/// <summary>
		/// Delete all recordings of a show.
		/// </summary>
		/// <param name="show">Show to delete</param>
		Task DeleteAsync(IShow show);
	}
}