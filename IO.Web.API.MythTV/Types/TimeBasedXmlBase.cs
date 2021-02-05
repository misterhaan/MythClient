using System;

namespace au.IO.Web.API.MythTV.Types {
	/// <summary>
	/// DateTime deserialization doesn't work directly from XML, so this function handles it correctly.
	/// </summary>
	/// <remarks>
	/// Use a DateTime? property marked with [XmlIgnore] that returns a function
	/// call to DeserializeDateTime() with the serialized value.  The serialized
	/// value can be stored in a property named with the Serialized prefix, using
	/// [XmlElement(string)] to specify the XML element name.
	/// </remarks>
	public abstract class TimeBasedXmlBase {
		/// <summary>
		/// Deserialize a string from XML into a DateTime object
		/// </summary>
		/// <param name="serializedDateTime">String value from XML representing a DateTime</param>
		/// <returns>DateTime the string represents, or null if empty or invalid</returns>
		protected static DateTime? DeserializeDateTime(string serializedDateTime)
			=> string.IsNullOrEmpty(serializedDateTime)
				? null
				: DateTime.TryParse(serializedDateTime, out DateTime dateTime)
					? dateTime
					: null;
	}
}
