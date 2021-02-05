using System;
using System.Collections.Generic;
using au.Applications.MythClient.Recordings.Types;

namespace au.Applications.MythClient.Recordings {
	/// <summary>
	/// Compare two shows for sorting.
	/// </summary>
	internal abstract class ShowComparer : IComparer<IShow> {
		/// <summary>
		/// Sort shows by title
		/// </summary>
		internal static ShowComparer Title => _titleComparer.Value;
		private static readonly Lazy<ShowTitleComparer> _titleComparer = new Lazy<ShowTitleComparer>();

		/// <summary>
		/// Sort shows by oldest recorded
		/// </summary>
		internal static ShowComparer OldestRecorded => _oldestRecordedComparer.Value;
		private static readonly Lazy<ShowOldestRecordedComparer> _oldestRecordedComparer = new Lazy<ShowOldestRecordedComparer>();

		/// <summary>
		/// Compare two shows for sorting
		/// </summary>
		/// <param name="x">First show to compare</param>
		/// <param name="y">Second show to compare</param>
		/// <returns></returns>
		public abstract int Compare(IShow x, IShow y);

		/// <summary>
		/// Sort shows by title
		/// </summary>
		private class ShowTitleComparer : ShowComparer {
			/// <inheritdoc />
			public override int Compare(IShow x, IShow y)
				=> StringComparer.CurrentCultureIgnoreCase.Compare(TitleSort(x.Title), TitleSort(y.Title));

			/// <summary>
			/// Remove prefix words from title so they sort as expected
			/// </summary>
			/// <param name="title">Display title</param>
			/// <returns>Sortable title</returns>
			private static string TitleSort(string title) {
#pragma warning disable IDE0046 // Convert to conditional expression
				if(title.StartsWith("The ", StringComparison.CurrentCultureIgnoreCase))
					return title[4..];
				if(title.StartsWith("An ", StringComparison.CurrentCultureIgnoreCase))
					return title[3..];
				if(title.StartsWith("A ", StringComparison.CurrentCultureIgnoreCase))
					return title[2..];
				return title;
#pragma warning restore IDE0046 // Convert to conditional expression
			}
		}

		/// <summary>
		/// Sort shows by oldest recorded
		/// </summary>
		private class ShowOldestRecordedComparer : ShowComparer {
			public override int Compare(IShow x, IShow y)
				=> DateTime.Compare(x.OldestEpisode.FirstAired, y.OldestEpisode.FirstAired);
		}
	}
}
