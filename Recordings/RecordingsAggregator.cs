using System;
using System.Collections.Generic;
using System.Linq;
using au.Applications.MythClient.Recordings.Types;
using au.Applications.MythClient.Settings.Types;
using au.IO.Web.API.MythTV.Types;

namespace au.Applications.MythClient.Recordings {
	/// <summary>
	/// Aggregates recordings from the MythTV API into a show / series / episode structure
	/// </summary>
	internal class RecordingsAggregator {
		/// <summary>
		/// Application settings
		/// </summary>
		private readonly IMythSettings _settings;

		/// <summary>
		/// Content API for preview image access
		/// </summary>
		private readonly IContentApi _contentApi;

		/// <summary>
		/// Show aggregator:  same title means same show
		/// </summary>
		private readonly Dictionary<string, Show> _showsByTitle = new Dictionary<string, Show>(StringComparer.CurrentCultureIgnoreCase);

		/// <summary>
		/// Season aggregator:  grouped by show tile; same number means same season; seasons are sorted by number
		/// </summary>
		private readonly Dictionary<string, SortedDictionary<int, Season>> _seasonsByShowTitleAndSeasonNumber = new Dictionary<string, SortedDictionary<int, Season>>(StringComparer.CurrentCultureIgnoreCase);

		/// <summary>
		/// Episode aggregator:  grouped by show title and episode number
		/// </summary>
		private readonly Dictionary<string, Dictionary<int, List<IEpisode>>> _episodesByShowTitleAndEpisodeNumber = new Dictionary<string, Dictionary<int, List<IEpisode>>>(StringComparer.CurrentCultureIgnoreCase);

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="settings">Application settings</param>
		/// <param name="contentApi">Content API for preview image access</param>
		internal RecordingsAggregator(IMythSettings settings, IContentApi contentApi) {
			_settings = settings;
			_contentApi = contentApi;
		}

		/// <summary>
		/// Add a recording to the aggregator.
		/// </summary>
		/// <param name="program">Recording to add</param>
		internal virtual void Add(Program program) {
			if(program.Recording.RecGroup.Equals("Deleted", StringComparison.InvariantCultureIgnoreCase))
				return;

			AddShow(program);
			AddSeason(program);
			AddEpisode(program);
		}

		/// <summary>
		/// Finalize aggregated recordings into show / season / episode structure.
		/// </summary>
		/// <returns>Collection of shows, which contain seasons, which contain episodes</returns>
		internal virtual IReadOnlyList<IShow> Finalize() {
			foreach(string title in _showsByTitle.Keys) {
				foreach(int number in _seasonsByShowTitleAndSeasonNumber[title].Keys) {
					List<IEpisode> episodes = _episodesByShowTitleAndEpisodeNumber[title][number];
					episodes.Sort();
					_seasonsByShowTitleAndSeasonNumber[title][number].Episodes = episodes.AsReadOnly();
				}
				_showsByTitle[title].Seasons = _seasonsByShowTitleAndSeasonNumber[title].Values.ToList<ISeason>().AsReadOnly();
			}

			List<IShow> shows = _showsByTitle.Values.ToList<IShow>();
			if(_settings.RecordingSortOption == RecordingSortOption.OldestRecorded)
				shows.Sort(ShowComparer.OldestRecorded);
			else
				shows.Sort(ShowComparer.Title);

			_episodesByShowTitleAndEpisodeNumber.Clear();
			_seasonsByShowTitleAndSeasonNumber.Clear();
			_showsByTitle.Clear();

			return shows.AsReadOnly();
		}

		/// <summary>
		/// Add the show for a recording if it doesn't already exist.
		/// </summary>
		/// <param name="program">Recording being added</param>
		private void AddShow(Program program) {
			string title = program.Title;

			if(_showsByTitle.ContainsKey(title))
				return;

			_showsByTitle[title] = new Show(title, program.Category, program.CatType);
			_seasonsByShowTitleAndSeasonNumber[title] = new SortedDictionary<int, Season>();
			_episodesByShowTitleAndEpisodeNumber[title] = new Dictionary<int, List<IEpisode>>();
		}

		/// <summary>
		/// Add the season for a recording if it doesn't already exist.  Also adds cover art URL if available.
		/// </summary>
		/// <param name="program">Recording being added</param>
		private void AddSeason(Program program) {
			string showTitle = program.Title;
			int number = program.Season;

			SortedDictionary<int, Season> showSeasons = _seasonsByShowTitleAndSeasonNumber[showTitle];

			if(!showSeasons.ContainsKey(number)) {
				showSeasons[number] = new Season(showTitle, number);
				_episodesByShowTitleAndEpisodeNumber[showTitle][number] = new List<IEpisode>();
			}

			AddCoverArtUrl(showSeasons[number], program.Artwork.ArtworkInfos);
		}

		/// <summary>
		/// Add the episode for a recording.
		/// </summary>
		/// <param name="program">Recording being added</param>
		private void AddEpisode(Program program) {
			string showTitle = program.Title;
			int seasonNumber = program.Season;
			_episodesByShowTitleAndEpisodeNumber[showTitle][seasonNumber].Add(new Episode(
				showTitle,
				seasonNumber,
				program.Recording.RecordedId,
				program.FileName,
				program.SubTitle,
				program.Episode,
				program.Airdate,
				program.StartTime?.ToLocalTime(),
				program.EndTime?.ToLocalTime(),
				program.Recording.Status,
				_contentApi.GetPreviewImageUrl(program.Recording.RecordedId).ToString()
			));
		}

		/// <summary>
		/// Add cover art for a season if it doesn't already have it.
		/// </summary>
		/// <param name="season">Season that may need cover art</param>
		/// <param name="artworkInfo">Art for a recording from the season</param>
		private void AddCoverArtUrl(Season season, List<ArtworkInfo> artworkInfo) {
			if(string.IsNullOrEmpty(season.CoverArtUrl)) {
				ArtworkInfo coverArt = artworkInfo.FirstOrDefault(ArtworkInfoIsCoverArt);
				season.CoverArtUrl = MakeFullUrl(coverArt?.URL);
			}
		}

		/// <summary>
		/// Check if artwork is cover art.
		/// </summary>
		/// <param name="artwork">Artwork to check</param>
		/// <returns>True if artwork is cover art</returns>
		private static bool ArtworkInfoIsCoverArt(ArtworkInfo artwork)
			=> "coverart".Equals(artwork.Type, StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(artwork.URL);

		/// <summary>
		/// Expand an absolute URL into a full URL.
		/// </summary>
		/// <param name="absoluteUrl">Absolute URL to expand</param>
		/// <returns>Full URL</returns>
		private string MakeFullUrl(string absoluteUrl)
			=> string.IsNullOrEmpty(absoluteUrl)
				? null
				: new UriBuilder(
					Uri.UriSchemeHttp,
					_settings.Server.Name,
					_settings.Server.Port,
					absoluteUrl.Split('?').First(),
					absoluteUrl[absoluteUrl.IndexOf('?')..]
				).Uri.ToString();
	}
}
