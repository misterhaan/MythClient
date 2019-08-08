using System;

namespace au.IO.Web.API.MythTV.Types {
	public class Program {
		public DateTime? StartTime { get; set; }

		public DateTime? EndTime { get; set; }

		public string Title { get; set; }

		public string SubTitle { get; set; }

		public string Category { get; set; }

		public string CatType { get; set; }

		public bool Repeat { get; set; }

		public int VideoProps { get; set; }

		public int AudioProps { get; set; }

		public int SubProps { get; set; }

		public string SeriesId { get; set; }

		public string ProgramId { get; set; }

		public double Stars { get; set; }

		public DateTime? LastModified { get; set; }

		public int ProgramFlags { get; set; }

		public DateTime? Airdate { get; set; }

		public string Description { get; set; }

		public string Inetref { get; set; }

		public int Season { get; set; }

		public int Episode { get; set; }

		public int TotalEpisodes { get; set; }

		public long FileSize { get; set; }

		public string FileName { get; set; }

		public string HostName { get; set; }

		public ChannelInfo Channel { get; set; } = new ChannelInfo();

		public RecordingInfo Recording { get; set; } = new RecordingInfo();

		public ArtworkInfoList Artwork { get; set; } = new ArtworkInfoList();

		public CastMemberList Cast { get; set; } = new CastMemberList();
	}
}
