using System;
using System.Collections.Generic;

namespace au.IO.Web.API.MythTV.Types {
	public class ProgramList {
		public int StartIndex { get; set; }
		public int Count { get; set; }
		public int TotalAvailable { get; set; }
		public DateTime? AsOf { get; set; }
		public string Version { get; set; }
		public string ProtoVer { get; set; }
		public List<Program> Programs { get; set; } = new List<Program>();
	}
}
