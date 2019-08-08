﻿namespace au.IO.Web.API.MythTV.Types {
	public enum RecStatusType {
		Pending = -15,
		Failing = -14,
		MissedFuture = -11,
		Tuning = -10,
		Failed = -9,
		TunerBusy = -8,
		LowDiskSpace = -7,
		Cancelled = -6,
		Missed = -5,
		Aborted = -4,
		Recorded = -3,
		Recording = -2,
		WillRecord = -1,
		Unknown = 0,
		DontRecord = 1,
		PreviousRecording = 2,
		CurrentRecording = 3,
		EarlierShowing = 4,
		TooManyRecordings = 5,
		NotListed = 6,
		Conflict = 7,
		LaterShowing = 8,
		Repeat = 9,
		Inactive = 10,
		NeverRecord = 11,
		Offline = 12
	}
}
