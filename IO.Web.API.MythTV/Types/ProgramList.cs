using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace au.IO.Web.API.MythTV.Types {
	public class ProgramList : TimeBasedXmlBase {
		public int StartIndex { get; set; }

		public int Count { get; set; }

		public int TotalAvailable { get; set; }

		[XmlIgnore]
		public DateTime? AsOf
			=> DeserializeDateTime(SerializedAsOf);

		public string Version { get; set; }

		public string ProtoVer { get; set; }

		public List<Program> Programs { get; set; } = new List<Program>();

		[XmlElement("AsOf")]
		public string SerializedAsOf { get; set; }
	}
}
