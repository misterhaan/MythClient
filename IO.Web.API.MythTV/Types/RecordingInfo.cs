using System;
using System.Xml.Serialization;

namespace au.IO.Web.API.MythTV.Types {
	public class RecordingInfo : TimeBasedXmlBase {
		public uint RecordedId { get; set; }

		public RecStatusType Status { get; set; }

		public int Priority { get; set; }

		[XmlIgnore]
		public DateTime? StartTs
			=> DeserializeDateTime(SerializedStartTs);

		[XmlIgnore]
		public DateTime? EndTs
		=> DeserializeDateTime(SerializedEndTs);

		public long FileSize { get; set; }

		public string FileName { get; set; }

		public string HostName { get; set; }

		[XmlIgnore]
		public DateTime? LastModified
			=> DeserializeDateTime(SerializedLastModified);

		public int RecordId { get; set; }

		public string RecGroup { get; set; }

		public string PlayGroup { get; set; }

		public string StorageGroup { get; set; }

		public int RecType { get; set; }

		public int DupInType { get; set; }

		public int DupMethod { get; set; }

		public int EncoderId { get; set; }

		public string EncoderName { get; set; }

		public string Profile { get; set; }

		[XmlElement("StartTs")]
		public string SerializedStartTs { get; set; }

		[XmlElement("EndTs")]
		public string SerializedEndTs { get; set; }

		[XmlElement("LastModified")]
		public string SerializedLastModified { get; set; }
	}
}
