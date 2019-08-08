using System;

namespace au.IO.Web.API.MythTV.Types {
	/// <summary>
	/// Functions for accessing the MythTV Content API.
	/// </summary>
	public interface IContentApi {
		/// <summary>
		/// Build the URL to the preview image for a recording.
		/// </summary>
		/// <param name="RecordedId">ID of the recording</param>
		/// <returns>URL to the preview image for a recording</returns>
		Uri GetPreviewImageUrl(uint RecordedId);
	}
}
