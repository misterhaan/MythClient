using au.IO.Web.API.MythTV.Types;

namespace au.IO.Web.API.MythTV {
	/// <summary>
	/// Factory for building clients for MythTV APIs.
	/// </summary>
	public class ApiFactory : IApiFactory {
		/// <inheritdoc />
		public IDvrApi BuildDvrApi(bool useHttps, string host, ushort port)
			=> new DvrApi(useHttps, host, port);

		/// <inheritdoc />
		public IContentApi BuildContentApi(bool useHttps, string host, ushort port)
			=> new ContentApi(useHttps, host, port);
	}
}
