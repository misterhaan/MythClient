using System.Threading.Tasks;
using au.Applications.MythClient.Recordings.Types;

namespace au.Applications.MythClient.UI.Actions {
	internal interface IRecordingsDeleter {
		Task DeleteAsync(IEpisode episode);
		Task DeleteAsync(IShow show);
	}
}