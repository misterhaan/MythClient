using au.Applications.MythClient.Recordings.Types;

namespace au.Applications.MythClient.UI.Actions {
	internal interface IRecordingsExporter {
		void Export(IEpisode episode);
		void Export(ISeason season);
		void Export(IShow show);
	}
}