using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

namespace au.util.FileOperation {
	/// <summary>
	/// Queued file copy based on IFileOperation.  Requires Windows Vista or newer.
	/// </summary>
	public class FileOperation : IDisposable {
		private static readonly Guid CLSID_FileOperation = new Guid("3ad05575-8857-4850-9277-11b85bdb8e09");
		private static readonly Type _fileOperationType = Type.GetTypeFromCLSID(CLSID_FileOperation);
		private static Guid _shellItemGuid = typeof(IShellItem).GUID;

		[DllImport("shell32.dll", SetLastError = true, CharSet = CharSet.Unicode, PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Interface)]
		private static extern object SHCreateItemFromParsingName([MarshalAs(UnmanagedType.LPWStr)] string pszPath, IBindCtx pbc, ref Guid riid);

		private static Disposer<IShellItem> CreateShellItem(string path) {
			return new Disposer<IShellItem>((IShellItem)SHCreateItemFromParsingName(path, null, ref _shellItemGuid));
		}

		private IFileOperation _fileOperation;

		/// <summary>
		/// Create a new FileOperation object that will perform all queued operations when disposed.
		/// </summary>
		public FileOperation() : this(null) { }
		/// <summary>
		/// Create a new FileOperation object that will perform all queued operations when disposed.
		/// </summary>
		/// <param name="owner">Owner of the file operations</param>
		public FileOperation(IWin32Window owner) {
			_fileOperation = (IFileOperation)Activator.CreateInstance(_fileOperationType);
			_fileOperation.SetOperationFlags(FileOperationFlags.FOF_NOCONFIRMMKDIR);
			if(owner != null)
				_fileOperation.SetOwnerWindow((uint)owner.Handle);
		}

		/// <summary>
		/// Add a file copy operation to the queue.
		/// </summary>
		/// <param name="source">File to copy from (must exist)</param>
		/// <param name="dest">File to copy to (will be created or overwritten)</param>
		public void QueueFileCopy(FileInfo source, FileInfo dest) { QueueFileCopy(source.FullName, dest.Directory.FullName, dest.Name); }

		/// <summary>
		/// Add a file copy operation to the queue.
		/// </summary>
		/// <param name="source">File to copy from (must exist)</param>
		/// <param name="destPath">Full path without filename to copy to (must exist)</param>
		/// <param name="destFilename">Filename without path to copy to (will be created or overwritten)</param>
		public void QueueFileCopy(FileInfo source, string destPath, string destFilename) { QueueFileCopy(source.FullName, destPath, destFilename); }

		/// <summary>
		/// Add a file copy operation to the queue.
		/// </summary>
		/// <param name="source">Full path and filename to copy from (must exist)</param>
		/// <param name="destPath">Full path without filename to copy to (must exist)</param>
		/// <param name="destFilename">Filename without path to copy to (will be created or overwritten)</param>
		public void QueueFileCopy(string source, string destPath, string destFilename) {
			using(Disposer<IShellItem> sourceItem = CreateShellItem(source))
			using(Disposer<IShellItem> destinationItem = CreateShellItem(destPath)) {
				_fileOperation.CopyItem(sourceItem.Item, destinationItem.Item, destFilename, null);
			}
		}

		public void Dispose() {
			try { _fileOperation.PerformOperations(); } catch { }  // if something goes wrong the IFileOperation UI will tell the user
			Marshal.FinalReleaseComObject(_fileOperation);
		}
	}
}
