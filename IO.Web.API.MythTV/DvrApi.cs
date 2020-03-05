using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Web;
using au.IO.Web.API.MythTV.Types;

namespace au.IO.Web.API.MythTV {
	/// <summary>
	/// Functions for accessing the MythTV DVR API.
	/// </summary>
	internal class DvrApi : ApiBase, IDvrApi {
		// based on <Version>29.20180316-1</Version><ProtoVer>91</ProtoVer>
		private const string _apiName = "Dvr/";

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="useHttps">Whether to use HTTPS (false means HTTP)</param>
		/// <param name="host">MythTV Web API hostname or IP address</param>
		/// <param name="port">MythTV Web API port number</param>
		internal DvrApi(bool useHttps, string host, ushort port)
			: base(useHttps, host, port, _apiName) { }

		/// <inheritdoc />
		public async Task<ProgramList> GetRecordedList()
			=> await GetRequest<ProgramList>(BuildGetRecordedListUrl()).ConfigureAwait(false);

		/// <summary>
		/// Build the full URL to the GetRecordedList endpoint.
		/// </summary>
		/// <returns>Full URL to the GetRecordedList endpoint</returns>
		private Uri BuildGetRecordedListUrl()
			=> new Uri(_urlBase, nameof(GetRecordedList));

		/// <inheritdoc />
		public async Task<bool> DeleteRecording(uint recordedId, bool allowRerecord)
			=> await PostRequestAsBool(BuildDeleteRecordingUrl(recordedId, allowRerecord)).ConfigureAwait(false);

		/// <summary>
		/// Build the full URL to the DeleteRecording endpoint.
		/// </summary>
		/// <param name="RecordedId">ID of recording to delete</param>
		/// <param name="AllowRerecord">Whether MythTV should record this episode if it sees it again</param>
		/// <returns>Full URL to the DeleteRecording endpoint</returns>
		private Uri BuildDeleteRecordingUrl(uint RecordedId, bool AllowRerecord) {
			UriBuilder uri = new UriBuilder(new Uri(_urlBase, nameof(DeleteRecording)));
			NameValueCollection parameters = HttpUtility.ParseQueryString("");
			parameters.Add(nameof(RecordedId), RecordedId.ToString());
			parameters.Add("ForceDelete", true.ToString());
			parameters.Add(nameof(AllowRerecord), AllowRerecord.ToString());
			uri.Query = parameters.ToString();
			return uri.Uri;
		}
	}
}
