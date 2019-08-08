using System;
using au.Applications.MythClient.Recordings.Types;
using au.IO.Web.API.MythTV.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.Applications.MythClient.Recordings.Tests {
	[TestClass]
	public class EpisodeTests {
		private const int _id = 1;
		private const string _filename = "recording.mp4";
		private const int _number = 2;
		private static readonly DateTime _firstAired = new DateTime(1999, 8, 7);
		private static readonly DateTime _recorded = new DateTime(2003, 2, 1);
		private static readonly DateTime _finished = new DateTime(2004, 3, 2);

		[TestMethod]
		public void Ctor_NoFirstAired_FirstAiredIsRecorded() {
			Episode e = CreateEpisode(_id, "no first aired test", _number, null, RecStatusType.Recorded);

			Assert.AreEqual(_recorded, e.FirstAired, $"{nameof(e.FirstAired)} should get set to {nameof(e.Recorded)} when not specified.");
		}

		[TestMethod]
		public void Ctor_FirstAired_FirstAiredSet() {
			Episode e = CreateEpisode(_id, "first aired test", _number, _firstAired, RecStatusType.Recorded);

			Assert.AreEqual(_firstAired, e.FirstAired);
		}

		[TestMethod]
		public void Duration_EndMinusStart() {
			Episode e = CreateEpisode(_id, "duration test", _number, _firstAired, RecStatusType.Recorded);

			Assert.AreEqual(_finished - _recorded, e.Duration, $"{nameof(e.Duration)} should be set to the difference between the doneRecording and recorded constructor parameters.");
		}

		[DataTestMethod]
		[DataRow(RecStatusType.Aborted)]
		[DataRow(RecStatusType.Cancelled)]
		[DataRow(RecStatusType.Conflict)]
		[DataRow(RecStatusType.CurrentRecording)]
		[DataRow(RecStatusType.DontRecord)]
		[DataRow(RecStatusType.EarlierShowing)]
		[DataRow(RecStatusType.Failed)]
		[DataRow(RecStatusType.Failing)]
		[DataRow(RecStatusType.Inactive)]
		[DataRow(RecStatusType.LaterShowing)]
		[DataRow(RecStatusType.LowDiskSpace)]
		[DataRow(RecStatusType.Missed)]
		[DataRow(RecStatusType.MissedFuture)]
		[DataRow(RecStatusType.NeverRecord)]
		[DataRow(RecStatusType.NotListed)]
		[DataRow(RecStatusType.Offline)]
		[DataRow(RecStatusType.Pending)]
		[DataRow(RecStatusType.PreviousRecording)]
		[DataRow(RecStatusType.Recorded)]
		[DataRow(RecStatusType.Repeat)]
		[DataRow(RecStatusType.TooManyRecordings)]
		[DataRow(RecStatusType.TunerBusy)]
		[DataRow(RecStatusType.Tuning)]
		[DataRow(RecStatusType.Unknown)]
		[DataRow(RecStatusType.WillRecord)]
		public void InProgress_StatusNotRecording_False(RecStatusType status) {
			Episode e = CreateEpisode(_id, "status not recording test", _number, _firstAired, status);

			Assert.IsFalse(e.InProgress, $"{nameof(e.InProgress)} should be false for status {status}.");
		}

		[TestMethod]
		public void InProgress_StatusRecording_True() {
			Episode e = CreateEpisode(_id, "status recording test", _number, _firstAired, RecStatusType.Recording);

			Assert.IsTrue(e.InProgress, $"{nameof(e.InProgress)} should be true for status {nameof(RecStatusType.Recording)}.");
		}

		[DataTestMethod]
		[DataRow(null)]
		[DataRow("episode")]
		[ExpectedException(typeof(ArgumentException), nameof(Episode.CompareTo) + " should throw " + nameof(ArgumentException) + " when comparing against an object that is not " + nameof(IEpisode) + ".")]
		public void CompareTo_NotIEpisode_ThrowsException(object compare) {
			Episode episode = CreateEpisode(_id, "compare not IEpisode test", _number, _firstAired, RecStatusType.Recorded);

			episode.CompareTo(compare);
		}

		[TestMethod]
		public void CompareTo_DifferentNumber_ComparesNumbers() {
			const string title = "Compare different numbers";
			Episode a = CreateEpisode(_id, title, 7, _firstAired, RecStatusType.Recorded);
			Episode b = CreateEpisode(_id + 1, title, 3, _firstAired.AddDays(1), RecStatusType.Recorded);

			int result = a.CompareTo(b);

			Assert.IsTrue(result > 0, $"{nameof(Episode)} number {a.Number} should come after number {b.Number}.");
		}

		[TestMethod]
		public void CompareTo_SameNumber_ComparesFirstAired() {
			const string title = "Compare different numbers";
			Episode a = CreateEpisode(_id, title, 0, _firstAired, RecStatusType.Recorded);
			Episode b = CreateEpisode(_id + 1, title, 0, _firstAired.AddDays(1), RecStatusType.Recorded);

			int result = a.CompareTo(b);

			Assert.IsTrue(result < 0, $"{nameof(Episode)} aired later should come after an {nameof(Episode)} with the same number.");
		}

		private static Episode CreateEpisode(uint id, string title, int number, DateTime? firstAired, RecStatusType status)
			=> new Episode("Episode Tests", 7, id, _filename, title, number, firstAired, _recorded, _finished, status, null);
	}
}
