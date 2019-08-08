using System;

namespace au.IO.Web.API.MythTV.Types {
	public class RecordingInfo {
		public uint RecordedId { get; set; }

		public RecStatusType Status { get; set; }

		public int Priority { get; set; }

		public DateTime? StartTs { get; set; }

		public DateTime? EndTs { get; set; }

		public long FileSize { get; set; }

		public string FileName { get; set; }

		public string HostName { get; set; }

		public DateTime? LastModified { get; set; }

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
	}
}
