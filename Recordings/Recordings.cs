using System.Collections.Generic;
using System.Threading.Tasks;
using au.Applications.MythClient.Recordings.Types;
using au.Applications.MythClient.Settings.Types;
using au.IO.Web.API.MythTV.Types;

namespace au.Applications.MythClient.Recordings {
	/// <summary>
	/// MythTV recordings organized into show / season / episode hierarchy.
	/// </summary>
	public class Recordings : IRecordings {
		/// <summary>
		/// Application settings
		/// </summary>
		private readonly IMythSettings _settings;

		/// <summary>
		/// Factory for creating MythTV API connections
		/// </summary>
		private readonly IApiFactory _apiFactory;

		/// <summary>
		/// MythTV DVR API client
		/// </summary>
		private IDvrApi _dvrApi;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="settings">Application settings</param>
		/// <param name="apiFactory">Factory for creating MythTV API connections</param>
		public Recordings(IMythSettings settings, IApiFactory apiFactory) {
			_settings = settings;
			_apiFactory = apiFactory;
		}

		/// <inheritdoc />
		public async Task LoadAsync() {
			_dvrApi = _apiFactory.BuildDvrApi(false, _settings.Server.Name, _settings.Server.Port);
			IContentApi contentApi = _apiFactory.BuildContentApi(false, _settings.Server.Name, _settings.Server.Port);

			RecordingsAggregator aggregator = new RecordingsAggregator(_settings, contentApi);
			ProgramList programList = await _dvrApi.GetRecordedList();

			foreach(Program program in programList.Programs)
				aggregator.Add(program);

			Shows = aggregator.Finalize();
		}

		/// <inheritdoc />
		public IReadOnlyList<IShow> Shows { get; private set; } = new List<IShow>().AsReadOnly();

		/// <inheritdoc />
		public async Task<bool> DeleteAsync(IEpisode episode, bool rerecord)
			=> await _dvrApi.DeleteRecording(episode.ID, rerecord);
	}
}
