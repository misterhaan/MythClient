using System.Threading.Tasks;

namespace au.IO.Web.API.MythTV.Types {
	/// <summary>
	/// Functions for accessing the MythTV DVR API.
	/// </summary>
	public interface IDvrApi {
		/// <summary>
		/// Get list of recorded programs.
		/// </summary>
		/// <returns>List of recorded programs</returns>
		Task<ProgramList> GetRecordedList();

		/// <summary>
		/// Delete a recording.
		/// </summary>
		/// <param name="recordedId">ID of the recording to delete</param>
		/// <param name="allowRerecord">True if MythTV should record this episode again</param>
		/// <returns>True if successful</returns>
		Task<bool> DeleteRecording(uint recordedId, bool allowRerecord);
	}
}
