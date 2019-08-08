namespace au.IO.Web.API.MythTV.Types {
	/// <summary>
	/// Factory for building clients for MythTV APIs.
	/// </summary>
	public interface IApiFactory {
		/// <summary>
		/// Build a DVR API object using the specified connection settings.
		/// </summary>
		/// <param name="useHttps">Whether to use HTTPS (false means HTTP)</param>
		/// <param name="host">MythTV Web API hostname or IP address</param>
		/// <param name="port">MythTV Web API port number</param>
		/// <returns>DVR API object</returns>
		IDvrApi BuildDvrApi(bool useHttps, string host, ushort port);

		/// <summary>
		/// Build a Content API object using the specified connection settings.
		/// </summary>
		/// <param name="useHttps">Whether to use HTTPS (false means HTTP)</param>
		/// <param name="host">MythTV Web API hostname or IP address</param>
		/// <param name="port">MythTV Web API port number</param>
		/// <returns>Content API object</returns>
		IContentApi BuildContentApi(bool useHttps, string host, ushort port);
	}
}
