using System;

namespace au.IO.Web.API.MythTV.Types {
	public abstract class TimeBasedXmlBase {
		protected static DateTime? DeserializeDateTime(string serializedDateTime)
			=> string.IsNullOrEmpty(serializedDateTime)
				? null
				: DateTime.TryParse(serializedDateTime, out DateTime dateTime)
					? (DateTime?)dateTime
					: null;
	}
}
