using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace au.IO.Web.API.MythTV {
	/// <summary>
	/// MythTV Web API base class
	/// </summary>
	internal class ApiBase {
		/// <summary>
		/// MythTV Web API base URL for this API
		/// </summary>
		protected readonly Uri _urlBase;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="useHttps">Whether to use HTTPS (false means HTTP)</param>
		/// <param name="host">MythTV Web API hostname or IP address</param>
		/// <param name="port">MythTV Web API port number</param>
		/// <param name="apiName">Name of the API</param>
		protected ApiBase(bool useHttps, string host, ushort port, string apiName) {
			string scheme = useHttps ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;
			_urlBase = new UriBuilder(scheme, host, port, apiName).Uri;
		}

		/// <summary>
		/// Perform a web API GET request, returning the result as the specified type.
		/// </summary>
		/// <typeparam name="T">Request return type</typeparam>
		/// <param name="url">Full URL to the API GET request</param>
		/// <returns>API result as the requested type</returns>
		/// <exception cref="Exception">Thrown when result cannot be deserialized into the requested type</exception>
		protected async Task<T> GetRequest<T>(Uri url) {
			HttpWebRequest req = WebRequest.CreateHttp(url);
			using(WebResponse response = await req.GetResponseAsync())
			using(Stream responseStream = response.GetResponseStream())
				if(new XmlSerializer(typeof(T)).Deserialize(responseStream) is T result)
					return result;
			throw new Exception("Unable to interpret MythTV API results.");
		}

		/// <summary>
		/// Perform a web API POST request, returning the result as a boolean.
		/// </summary>
		/// <param name="url">Full URL to the API POST request</param>
		/// <returns>API result as a boolean</returns>
		protected async Task<bool> PostRequestAsBool(Uri url) {
			HttpWebRequest req = WebRequest.CreateHttp(url);
			req.Method = "POST";
			using(WebResponse response = await req.GetResponseAsync())
			using(Stream responseStream = response.GetResponseStream())
			using(StreamReader reader = new StreamReader(responseStream)) {
				string responseText = await reader.ReadToEndAsync();
				MemoryStream translatedStream = new MemoryStream(Encoding.UTF8.GetBytes(responseText.Replace("bool>", "boolean>")));
				bool? value = new XmlSerializer(typeof(bool)).Deserialize(translatedStream) as bool?;
				return value.HasValue && value.Value;
			}
		}

		/// <summary>
		/// Perform a web API GET request, returning the result as an image.
		/// </summary>
		/// <param name="url">Full URL to the API GET request</param>
		/// <returns>API result as an image</returns>
		protected async Task<Image> GetRequestAsImage(Uri url) {
			HttpWebRequest req = WebRequest.CreateHttp(url);
			using(WebResponse response = await req.GetResponseAsync())
			using(Stream responseStream = response.GetResponseStream())
				return Image.FromStream(responseStream);
		}
	}
}
