using System;
using System.Xml.Serialization;

namespace au.IO.Web.API.MythTV.Types {
	public class Program : TimeBasedXmlBase {
		[XmlIgnore]
		public DateTime? StartTime
			=> DeserializeDateTime(SerializedStartTime);

		[XmlIgnore]
		public DateTime? EndTime
			=> DeserializeDateTime(SerializedEndTime);

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

		[XmlIgnore]
		public DateTime? LastModified
			=> DeserializeDateTime(SerializedLastModified);

		public int ProgramFlags { get; set; }

		[XmlIgnore]
		public DateTime? Airdate
			=> DeserializeDateTime(SerializedAirdate);

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

		[XmlElement("Airdate")]
		public string SerializedAirdate { get; set; }

		[XmlElement("StartTime")]
		public string SerializedStartTime { get; set; }

		[XmlElement("EndTime")]
		public string SerializedEndTime { get; set; }

		[XmlElement("LastModified")]
		public string SerializedLastModified { get; set; }
	}
}
