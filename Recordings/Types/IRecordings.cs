using System.Collections.Generic;
using System.Threading.Tasks;

namespace au.Applications.MythClient.Recordings.Types {
	/// <summary>
	/// MythTV recordings organized into show / season / episode hierarchy.
	/// </summary>
	public interface IRecordings {
		/// <summary>
		/// Load all recordings from the MythTV API.
		/// </summary>
		/// <returns>Awaitable task</returns>
		Task LoadAsync();

		/// <summary>
		/// All of the shows loaded from the MythTV API
		/// </summary>
		IReadOnlyList<IShow> Shows { get; }

		/// <summary>
		/// Attempt to find the specified show, or the next show if the actual show no longer exists.
		/// </summary>
		/// <param name="sample">Sample show to find</param>
		/// <returns>Best match for the sample show, or null if sample was null or there are no shows</returns>
		IShow FindShow(IShow sample);

		/// <summary>
		/// Delete an Episode from MythTV.  Does not remove it from the recordings collection.
		/// </summary>
		/// <param name="episode">Episode to delete</param>
		/// <param name="rerecord">Whether the server should rerecord this episode</param>
		/// <returns>True if the Episode was deleted successfully</returns>
		Task<bool> DeleteAsync(IEpisode episode, bool rerecord);
	}
}
