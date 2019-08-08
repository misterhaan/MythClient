using System;
using System.Collections.Specialized;
using System.Web;
using au.IO.Web.API.MythTV.Types;

namespace au.IO.Web.API.MythTV {
	/// <summary>
	/// Functions for accessing the MythTV Content API.
	/// </summary>
	internal class ContentApi : ApiBase, IContentApi {
		private const string _apiName = "Content/";
		private const string _getPreviewImage = "GetPreviewImage";

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="useHttps">Whether to use HTTPS (false means HTTP)</param>
		/// <param name="host">MythTV Web API hostname or IP address</param>
		/// <param name="port">MythTV Web API port number</param>
		internal ContentApi(bool useHttps, string host, ushort port)
			: base(useHttps, host, port, _apiName) { }

		/// <inheritdoc />
		public Uri GetPreviewImageUrl(uint RecordedId) {
			UriBuilder uri = new UriBuilder(new Uri(_urlBase, _getPreviewImage));
			NameValueCollection parameters = HttpUtility.ParseQueryString("");
			parameters.Add(nameof(RecordedId), RecordedId.ToString());
			uri.Query = parameters.ToString();
			return uri.Uri;
		}
	}
}
