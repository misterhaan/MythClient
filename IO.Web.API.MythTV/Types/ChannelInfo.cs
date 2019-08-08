using System.Collections.Generic;

namespace au.IO.Web.API.MythTV.Types {
	public class ChannelInfo {
		public uint ChanId { get; set; }

		public string ChanNum { get; set; }

		public string CallSign { get; set; }

		public string IconURL { get; set; }

		public string ChannelName { get; set; }

		public uint MplexId { get; set; }

		public uint ServiceId { get; set; }

		public uint ATSCMajorChan { get; set; }

		public uint ATSCMinorChan { get; set; }

		public string Format { get; set; }

		public string FrequencyId { get; set; }

		public int FineTune { get; set; }

		public string ChanFilters { get; set; }

		public int SourceId { get; set; }

		public int InputId { get; set; }

		public bool CommFree { get; set; }

		public bool UseEIT { get; set; }

		public bool Visible { get; set; }

		public string XMLTVID { get; set; }

		public string DefaultAuth { get; set; }

		public List<Program> Programs { get; set; } = new List<Program>();
	}
}
