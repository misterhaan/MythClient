using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using au.Applications.MythClient.Recordings.Types;
using au.Applications.MythClient.Settings.Types;
using au.Applications.MythClient.UI.Actions;
using au.Applications.MythClient.UI.Render;

namespace au.Applications.MythClient.UI {
	/// <summary>
	/// Keeps track of the current navigation state in the recordings window.
	/// </summary>
	internal class RecordingsNavigator : NeedsRecordingFilesBase {
		/// <summary>
		/// Window to own dialogs
		/// </summary>
		private readonly IWin32Window _owner;

		/// <summary>
		/// Collection of recordings
		/// </summary>
		protected readonly IRecordings _recordings;

		/// <summary>
		/// Renders the contents pane
		/// </summary>
		protected readonly IContentsRenderer _contents;

		/// <summary>
		/// Renders the info pane
		/// </summary>
		protected readonly IInfoRenderer _info;

		/// <summary>
		/// Deletes recordings
		/// </summary>
		protected readonly IRecordingsDeleter _deleter;

		/// <summary>
		/// Exports recordings
		/// </summary>
		protected readonly IRecordingsExporter _exporter;

		/// <summary>
		/// Context menu for shows that are movies
		/// </summary>
		private readonly Lazy<ContextMenuStrip> _cmnuMovie;

		/// <summary>
		/// Context menu for shows that have episodes
		/// </summary>
		private readonly Lazy<ContextMenuStrip> _cmnuShow;

		/// <summary>
		/// Context menu for seasons
		/// </summary>
		private readonly Lazy<ContextMenuStrip> _cmnuSeason;

		/// <summary>
		/// Context menu for episodes
		/// </summary>
		private readonly Lazy<ContextMenuStrip> _cmnuEpisode;

		/// <summary>
		/// Which level of the hierarchy is currently open
		/// </summary>
		protected BrowsingDepth _depth = BrowsingDepth.Recordings;

		/// <summary>
		/// Currently selected / open show
		/// </summary>
		protected IShow _show = null;

		/// <summary>
		/// Currently selected / open season (null when at recordings depth)
		/// </summary>
		protected ISeason _season = null;

		/// <summary>
		/// Currentnly selected episode (null when at recordings or show depth)
		/// </summary>
		protected IEpisode _episode = null;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="owner">Owner window for dialogs</param>
		/// <param name="settings">Application settings</param>
		/// <param name="recordings">Collection of recordings</param>
		/// <param name="contents">The contents pane</param>
		/// <param name="info">The info pane</param>
		internal RecordingsNavigator(IWin32Window owner, IMythSettings settings, IRecordings recordings, Panel contents, Panel info)
			: this(owner, settings, recordings, new ContentsRenderer(contents), new InfoRenderer(info), new ActorFactory()) {
			contents.ControlAdded += Contents_ControlAdded;
			info.ControlAdded += Info_ControlAdded;
		}

		/// <summary>
		/// Testing constructor
		/// </summary>
		/// <param name="owner">Owner window for dialogs</param>
		/// <param name="settings">Application settings</param>
		/// <param name="recordings">Collection of recordings</param>
		/// <param name="contents">Renders the contents pane</param>
		/// <param name="info">Renders the info pane</param>
		/// <param name="actorFactory">Creates objects that can act on recordings</param>
		protected RecordingsNavigator(IWin32Window owner, IMythSettings settings, IRecordings recordings, IContentsRenderer contents, IInfoRenderer info, IActorFactory actorFactory)
			: base(settings) {
			_owner = owner;
			_recordings = recordings;

			_contents = contents;
			_info = info;

			_deleter = actorFactory.BuildRecordingsDeleter(owner, recordings, this);
			_exporter = actorFactory.BuildRecordingsExporter(owner, settings);

			_cmnuMovie = new Lazy<ContextMenuStrip>(BuildMovieContextMenu);
			_cmnuShow = new Lazy<ContextMenuStrip>(BuildShowContextMenu);
			_cmnuSeason = new Lazy<ContextMenuStrip>(BuildSeasonContextMenu);
			_cmnuEpisode = new Lazy<ContextMenuStrip>(BuildEpisodeContextMenu);
		}

		internal event EventHandler DepthChanged;

		/// <summary>
		/// Current browsing depth.
		/// </summary>
		protected internal BrowsingDepth Depth {
			get => _depth;
			protected set {
				if(_depth != value) {
					_depth = value;
					DepthChanged?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Name of the current browsing location.
		/// </summary>
		internal string LocationName {
			get {
				switch(Depth) {
					case BrowsingDepth.Recordings:
						return Titles.Recordings;
					case BrowsingDepth.Show:
						return _show.Title;
					case BrowsingDepth.Season:
						return _season.Number == 0 && _show.Seasons.Count == 1
							? _show.Title
							: string.Format(Titles.Season, _show.Title, _season.Number);
					default:
						throw new InvalidOperationException($"{nameof(LocationName)} not available at unknown depth: {Depth}!");
				}
			}
		}

		/// <summary>
		/// Description of what going back will do
		/// </summary>
		internal string BackDescription {
			get {
				switch(Depth) {
					case BrowsingDepth.Recordings: return "";
					case BrowsingDepth.Show: return string.Format(Titles.BackTo, Titles.Recordings);
					case BrowsingDepth.Season:
						return _show.Seasons.Count == 1
							? string.Format(Titles.BackTo, Titles.Recordings)
							: string.Format(Titles.BackTo, _show.Title);
					default: throw new InvalidOperationException($"{nameof(BackDescription)} not available at unknown depth: {Depth}!");
				}
			}
		}

		/// <summary>
		/// Refresh recordings from the MythTV server and update the UI.
		/// </summary>
		internal async Task RefreshAsync() {
			await _recordings.LoadAsync();
			UpdateStateObjects();
			Render();
		}

		/// <summary>
		/// After loading the recordings list, the state objects need to be matched
		/// up with what's in the new recordings list.
		/// </summary>
		private void UpdateStateObjects() {
			IShow oldShow = _show;
			_show = _recordings.FindShow(_show);
			if(oldShow?.Matches(_show) ?? false && Depth != BrowsingDepth.Recordings) {
				ISeason oldSeason = _season;
				_season = _show.FindSeason(_season);
				if(oldSeason?.Matches(_season) ?? false && Depth != BrowsingDepth.Show)
					_episode = _season.FindEpisode(_episode);
				else {
					Depth = BrowsingDepth.Show;
					_episode = null;
				}
			} else {
				Depth = BrowsingDepth.Recordings;
				_season = null;
				_episode = null;
			}
		}

		/// <summary>
		/// Render the contents and info panels to reflect the current navigation state.
		/// </summary>
		internal void Render() {
			switch(Depth) {
				case BrowsingDepth.Recordings:
					RenderRecordings();
					break;
				case BrowsingDepth.Show:
					RenderShow();
					break;
				case BrowsingDepth.Season:
					RenderSeason();
					break;
			}
		}

		/// <summary>
		/// Render the contents and info panels to reflect the current navigation state when at the recordings depth.
		/// </summary>
		private void RenderRecordings() {
			_show = _show ?? _recordings.Shows.FirstOrDefault();
			_contents.Render(_recordings.Shows, _show);
			_info.Render(_show);
		}

		/// <summary>
		/// Render the contents and info panels to reflect the current navigation state when at the show depth.
		/// </summary>
		private void RenderShow() {
			_season = _season ?? _show.Seasons.First();
			_contents.Render(_show.Seasons, _season);
			_info.Render(_season);
		}

		/// <summary>
		/// Render the contents and info panels to reflect the current navigation state when at the season depth.
		/// </summary>
		private void RenderSeason() {
			_episode = _episode ?? _season.Episodes.First();
			_contents.Render(_season.Episodes, _episode);
			_info.Render(_episode);
		}

		/// <summary>
		/// Play the selected episode or the oldest episode from the selected season or show.
		/// </summary>
		internal void Play() {
			switch(Depth) {
				case BrowsingDepth.Recordings:
					Play(_show.OldestEpisode);
					break;
				case BrowsingDepth.Show:
					Play(_season.OldestEpisode);
					break;
				case BrowsingDepth.Season:
					Play(_episode);
					break;
			}
		}

		/// <summary>
		/// Open the currently selected item.
		/// </summary>
		internal void Open() {
			switch(Depth) {
				case BrowsingDepth.Recordings:
					OpenShow();
					break;
				case BrowsingDepth.Show:
					OpenSeason();
					break;
				case BrowsingDepth.Season:
					Play(_episode);
					break;
			}
		}

		/// <summary>
		/// Change the selected item based on the current selected item.
		/// </summary>
		/// <param name="direction">Direction to move the seceltion</param>
		internal void Navigate(NavigationDirection direction) {
			switch(direction) {
				case NavigationDirection.First: NavigateFirst(); break;
				case NavigationDirection.Last: NavigateLast(); break;
				case NavigationDirection.Next: NavigateNext(); break;
				case NavigationDirection.Previous: NavigatePrevious(); break;
				case NavigationDirection.NextRow: NavigateNextRow(); break;
				case NavigationDirection.PreviousRow: NavigatePreviousRow(); break;
			}
		}

		/// <summary>
		/// Change the selection to the first item.
		/// </summary>
		private void NavigateFirst() {
			switch(Depth) {
				case BrowsingDepth.Recordings:
					if(_show != _recordings.Shows.First()) {
						_show = _recordings.Shows.First();
						_contents.UpdateSelected(0);
						_info.Render(_show);
					}
					break;
				case BrowsingDepth.Show:
					if(_season != _show.Seasons.First()) {
						_season = _show.Seasons.First();
						_contents.UpdateSelected(0);
						_info.Render(_season);
					}
					break;
				case BrowsingDepth.Season:
					if(_episode != _season.Episodes.First()) {
						_episode = _season.Episodes.First();
						_contents.UpdateSelected(0);
						_info.Render(_episode);
					}
					break;
			}
		}

		/// <summary>
		/// Change the selection to the last item.
		/// </summary>
		private void NavigateLast() {
			switch(Depth) {
				case BrowsingDepth.Recordings:
					if(_show != _recordings.Shows.Last()) {
						_show = _recordings.Shows.Last();
						_contents.UpdateSelected(_recordings.Shows.Count - 1);
						_info.Render(_show);
					}
					break;
				case BrowsingDepth.Show:
					if(_season != _show.Seasons.Last()) {
						_season = _show.Seasons.Last();
						_contents.UpdateSelected(_show.Seasons.Count - 1);
						_info.Render(_season);
					}
					break;
				case BrowsingDepth.Season:
					if(_episode != _season.Episodes.Last()) {
						_episode = _season.Episodes.Last();
						_contents.UpdateSelected(_season.Episodes.Count - 1);
						_info.Render(_episode);
					}
					break;
			}
		}

		/// <summary>
		/// Change the selection to the item immediately after the currently selected item.
		/// </summary>
		private void NavigateNext() {
			int nextIndex = _contents.GetSelectedIndex() + 1;
			switch(Depth) {
				case BrowsingDepth.Recordings:
					if(nextIndex < _recordings.Shows.Count) {
						_show = _recordings.Shows[nextIndex];
						_contents.UpdateSelected(nextIndex);
						_info.Render(_show);
					}
					break;
				case BrowsingDepth.Show:
					if(nextIndex < _show.Seasons.Count) {
						_season = _show.Seasons[nextIndex];
						_contents.UpdateSelected(nextIndex);
						_info.Render(_season);
					}
					break;
				case BrowsingDepth.Season:
					if(nextIndex < _season.Episodes.Count) {
						_episode = _season.Episodes[nextIndex];
						_contents.UpdateSelected(nextIndex);
						_info.Render(_episode);
					}
					break;
			}
		}

		/// <summary>
		/// Change the selection to the item immediately before the currently selected item.
		/// </summary>
		private void NavigatePrevious() {
			int prevIndex = _contents.GetSelectedIndex() - 1;
			if(prevIndex >= 0) {
				_contents.UpdateSelected(prevIndex);
				switch(Depth) {
					case BrowsingDepth.Recordings:
						_show = _recordings.Shows[prevIndex];
						_info.Render(_show);
						break;
					case BrowsingDepth.Show:
						_season = _show.Seasons[prevIndex];
						_info.Render(_season);
						break;
					case BrowsingDepth.Season:
						_episode = _season.Episodes[prevIndex];
						_info.Render(_episode);
						break;
				}
			}
		}

		/// <summary>
		/// Change the selection to the item directly beneath the currently selected item.
		/// </summary>
		private void NavigateNextRow() {
			int selectedIndex = _contents.GetSelectedIndex();
			int nextIndex = selectedIndex + _contents.GetControlsPerRow();
			switch(Depth) {
				case BrowsingDepth.Recordings:
					if(nextIndex >= _recordings.Shows.Count && selectedIndex < _recordings.Shows.Count - 1)
						nextIndex = _recordings.Shows.Count - 1;
					if(nextIndex < _recordings.Shows.Count) {
						_show = _recordings.Shows[nextIndex];
						_contents.UpdateSelected(nextIndex);
						_info.Render(_show);
					}
					break;
				case BrowsingDepth.Show:
					if(nextIndex >= _show.Seasons.Count && selectedIndex < _show.Seasons.Count - 1)
						nextIndex = _show.Seasons.Count - 1;
					if(nextIndex < _show.Seasons.Count) {
						_season = _show.Seasons[nextIndex];
						_contents.UpdateSelected(nextIndex);
						_info.Render(_season);
					}
					break;
				case BrowsingDepth.Season:
					if(nextIndex >= _season.Episodes.Count && selectedIndex < _season.Episodes.Count - 1)
						nextIndex = _season.Episodes.Count - 1;
					if(nextIndex < _season.Episodes.Count) {
						_episode = _season.Episodes[nextIndex];
						_contents.UpdateSelected(nextIndex);
						_info.Render(_season);
					}
					break;
			}
		}

		/// <summary>
		/// Change the selection to the item directly above the currently selected item.
		/// </summary>
		private void NavigatePreviousRow() {
			int prevIndex = _contents.GetSelectedIndex() - _contents.GetControlsPerRow();
			if(prevIndex >= 0) {
				_contents.UpdateSelected(prevIndex);
				switch(Depth) {
					case BrowsingDepth.Recordings:
						_show = _recordings.Shows[prevIndex];
						_info.Render(_show);
						break;
					case BrowsingDepth.Show:
						_season = _show.Seasons[prevIndex];
						_info.Render(_season);
						break;
					case BrowsingDepth.Season:
						_episode = _season.Episodes[prevIndex];
						_info.Render(_episode);
						break;
				}
			}
		}

		/// <summary>
		/// Close the open view and go back to its container.
		/// </summary>
		internal void Close() {
			switch(Depth) {
				case BrowsingDepth.Season:
					CloseSeason();
					break;
				case BrowsingDepth.Show:
					CloseShow();
					break;
#if DEBUG
				default:
					throw new InvalidOperationException($"Cannot close from {Depth} depth!");
#endif
			}
		}

		/// <summary>
		/// Delete the selected episode or the oldest episode of the selected season or show.
		/// </summary>
		internal async Task DeleteAsync() {
			switch(Depth) {
				case BrowsingDepth.Recordings:
					await _deleter.DeleteAsync(_show.OldestEpisode);
					break;
				case BrowsingDepth.Show:
					await _deleter.DeleteAsync(_season.OldestEpisode);
					break;
				case BrowsingDepth.Season:
					await _deleter.DeleteAsync(_episode);
					break;
			}
		}

		/// <summary>
		/// Open the context menu for the selected control.
		/// </summary>
		internal void ShowContextMenu() {
			Control control = _contents.GetSelected();
			control.ContextMenuStrip.Show(control, control.Width / 2, control.Height / 2);
		}

		/// <summary>
		/// Open the currently selected show.
		/// </summary>
		private void OpenShow() {
			if(_show.Seasons.Count > 1) {
				Depth = BrowsingDepth.Show;
				Render();
			} else {
				_season = _show.Seasons.Single();
				OpenSeason();
			}
		}

		/// <summary>
		/// Close the currently open show and go back to the recordings.
		/// </summary>
		private void CloseShow() {
			_season = null;
			Depth = BrowsingDepth.Recordings;
			Render();
		}

		/// <summary>
		/// Open the currently selected season.
		/// </summary>
		private void OpenSeason() {
			Depth = BrowsingDepth.Season;
			Render();
		}

		/// <summary>
		/// Close the currently open season and go back to the show.
		/// </summary>
		private void CloseSeason() {
			_episode = null;
			if(_show.Seasons.Count > 1)
				OpenShow();
			else
				CloseShow();
		}

		/// <summary>
		/// Play an episode with the default application.
		/// </summary>
		/// <param name="episode">Episode to play</param>
		private void Play(IEpisode episode) {
			try {
				Process.Start(GetRecordingFileName(episode));
			} catch(FileNotFoundException e) {
				MessageBox.Show(_owner, e.Message, ActionText.PlayCaption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		}

		/// <summary>
		/// Play an episode after choosing an application.
		/// </summary>
		/// <param name="episode">Episode to play</param>
		private void PlayWith(IEpisode episode) {
			try {
				Process.Start("rundll32.exe", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "shell32.dll") + ",OpenAs_RunDLL " + GetRecordingFileName(episode));
			} catch(FileNotFoundException e) {
				MessageBox.Show(_owner, e.Message, ActionText.PlayCaption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		}

		#region events
		private void Contents_ControlAdded(object sender, ControlEventArgs e) {
			e.Control.Click += ContentsItem_Click;
			e.Control.DoubleClick += ContentsItem_DoubleClick;
			e.Control.MouseDown += ContentsItem_MouseDown;
			switch(Depth) {
				case BrowsingDepth.Recordings:
					e.Control.ContextMenuStrip = e.Control.Tag is IShowItemTag tag && tag.IsSeries
						? _cmnuShow.Value
						: _cmnuMovie.Value;
					break;
				case BrowsingDepth.Show:
					e.Control.ContextMenuStrip = _cmnuSeason.Value;
					break;
				case BrowsingDepth.Season:
					e.Control.ContextMenuStrip = _cmnuEpisode.Value;
					break;
			}
		}

		private void Info_ControlAdded(object sender, ControlEventArgs e) {
			if(e.Control is Button button && button.Tag is IInfoActionButtonTag tag)
				button.Click += GetButtonClickAction(tag.ActionKey);
		}

		private EventHandler GetButtonClickAction(ActionKey key) {
			switch(key) {
				case ActionKey.ExportShow: return MenuShowExport_Click;
				case ActionKey.DeleteShow: return MenuShowDeleteAll_Click;
				case ActionKey.PlayShowOldest: return MenuShowPlayNext_Click;
				case ActionKey.PlayShowOldestWith: return MenuShowPlayNextWith_Click;
				case ActionKey.DeleteShowOldest: return MenuShowDeleteNext_Click;

				case ActionKey.ExportSeason: return MenuSeasonExport_Click;
				case ActionKey.PlaySeasonOldest: return MenuSeasonPlayNext_Click;
				case ActionKey.PlaySeasonOldestWith: return MenuSeasonPlayNextWith_Click;
				case ActionKey.DeleteSeasonOldest: return MenuSeasonDeleteNext_Click;

				case ActionKey.PlayEpisode: return MenuEpisodePlay_Click;
				case ActionKey.PlayEpisodeWith: return MenuEpisodePlayWith_Click;
				case ActionKey.ExportEpisode: return MenuEpisodeExport_Click;
				case ActionKey.DeleteEpisode: return MenuEpisodeDelete_Click;

				case ActionKey.ExportMovie: return MenuMovieExport_Click;

				default: throw new ArgumentException($"No action defined for {nameof(ActionKey)}.{key}", nameof(key));
			}
		}

		private void ContentsItem_Click(object sender, EventArgs e) {
			if(sender is Control control) {
				int newIndex = control.Parent.Controls.IndexOf(control);
				Debug.Assert(newIndex >= 0, "Clicked control was not found in parent.");
				if(newIndex < control.Parent.Controls.Count - 1)
					switch(Depth) {
						case BrowsingDepth.Recordings:
							if(_show != _recordings.Shows[newIndex]) {
								_show = _recordings.Shows[newIndex];
								_contents.UpdateSelected(control);
								_info.Render(_show);
							}
							break;
						case BrowsingDepth.Show:
							if(_season != _show.Seasons[newIndex]) {
								_season = _show.Seasons[newIndex];
								_contents.UpdateSelected(control);
								_info.Render(_season);
							}
							break;
						case BrowsingDepth.Season:
							if(_episode != _season.Episodes[newIndex]) {
								_episode = _season.Episodes[newIndex];
								_contents.UpdateSelected(control);
								_info.Render(_episode);
							}
							break;
					}
			}
		}

		private void ContentsItem_DoubleClick(object sender, EventArgs e) {
			if(sender is Control control) {
				int index = control.Parent.Controls.IndexOf(control);
				switch(Depth) {
					case BrowsingDepth.Recordings:
						_show = _recordings.Shows[index];
						if(_show.Seasons.Count > 1)
							OpenShow();
						else {
							ISeason season = _show.Seasons.First();
							if(season.Episodes.Count > 1) {
								_season = season;
								OpenSeason();
							} else
								Play(season.Episodes.First());
						}
						break;
					case BrowsingDepth.Show:
						_season = _show.Seasons[index];
						if(_season.Episodes.Count > 1)
							OpenSeason();
						else
							Play(_season.Episodes.First());
						break;
					case BrowsingDepth.Season:
						_episode = _season.Episodes[index];
						Play(_episode);
						break;
				}
			}
		}

		private void ContentsItem_MouseDown(object sender, MouseEventArgs e) {
			if(e.Button == MouseButtons.Right && sender is Control control) {
				int index = control.Parent.Controls.IndexOf(control);
				switch(Depth) {
					case BrowsingDepth.Recordings:
						if(_show != _recordings.Shows[index]) {
							_show = _recordings.Shows[index];
							_contents.UpdateSelected(control);
							_info.Render(_show);
						}
						break;
					case BrowsingDepth.Show:
						if(_season != _show.Seasons[index]) {
							_season = _show.Seasons[index];
							_contents.UpdateSelected(control);
							_info.Render(_season);
						}
						break;
					case BrowsingDepth.Season:
						if(_episode != _season.Episodes[index]) {
							_episode = _season.Episodes[index];
							_contents.UpdateSelected(control);
							_info.Render(_episode);
						}
						break;
				}
			}
		}

		private ContextMenuStrip BuildMovieContextMenu() {
			ContextMenuStrip menu = new ContextMenuStrip();
			menu.Items.AddRange(new ToolStripItem[] {
				new ToolStripMenuItem(ActionText.PlayMenu, Icons.Material_Play18, MenuShowPlayNext_Click),
				new ToolStripMenuItem(ActionText.PlayWithMenu, Icons.Material_PlayWith18, MenuShowPlayNextWith_Click),
				new ToolStripMenuItem(ActionText.ExportMenu, Icons.Material_Export18, MenuShowDeleteNext_Click),
				new ToolStripMenuItem(ActionText.DeleteMenu, Icons.Material_Delete18, MenuMovieExport_Click)
			});
			return menu;
		}

		private void MenuMovieExport_Click(object sender, EventArgs e)
			=> _exporter.Export(_show.NewestEpisode);

		private ContextMenuStrip BuildShowContextMenu() {
			ContextMenuStrip menu = new ContextMenuStrip();
			menu.Items.AddRange(new ToolStripItem[] {
				new ToolStripMenuItem(ActionText.PlayNextMenu, Icons.Material_Play18, MenuShowPlayNext_Click),
				new ToolStripMenuItem(ActionText.PlayNextWithMenu, Icons.Material_PlayWith18, MenuShowPlayNextWith_Click),
				new ToolStripMenuItem(ActionText.DeleteNextMenu, Icons.Material_Delete18, MenuShowDeleteNext_Click),
				new ToolStripSeparator(),
				new ToolStripMenuItem(ActionText.ExportAllMenu, Icons.Material_Export18, MenuShowExport_Click),
				new ToolStripMenuItem(ActionText.DeleteAllMenu, Icons.Material_Delete18, MenuShowDeleteAll_Click)
			});
			return menu;
		}

		private void MenuShowPlayNext_Click(object sender, EventArgs e)
			=> Play(_show.OldestEpisode);

		private void MenuShowPlayNextWith_Click(object sender, EventArgs e)
			=> PlayWith(_show.OldestEpisode);

		private async void MenuShowDeleteNext_Click(object sender, EventArgs e)
			=> await _deleter.DeleteAsync(_show.OldestEpisode);

		private void MenuShowExport_Click(object sender, EventArgs e)
			=> _exporter.Export(_show);

		private async void MenuShowDeleteAll_Click(object sender, EventArgs e)
			=> await _deleter.DeleteAsync(_show);

		private ContextMenuStrip BuildSeasonContextMenu() {
			ContextMenuStrip menu = new ContextMenuStrip();
			menu.Items.AddRange(new ToolStripItem[] {
				new ToolStripMenuItem(ActionText.PlayNextMenu, Icons.Material_Play18, MenuSeasonPlayNext_Click),
				new ToolStripMenuItem(ActionText.PlayNextWithMenu, Icons.Material_PlayWith18, MenuSeasonPlayNextWith_Click),
				new ToolStripMenuItem(ActionText.DeleteNextMenu, Icons.Material_Delete18, MenuSeasonDeleteNext_Click),
				new ToolStripSeparator(),
				new ToolStripMenuItem(ActionText.ExportAllMenu, Icons.Material_Export18, MenuSeasonExport_Click),
			});
			return menu;
		}

		private void MenuSeasonPlayNext_Click(object sender, EventArgs e)
			=> Play(_season.OldestEpisode);

		private void MenuSeasonPlayNextWith_Click(object sender, EventArgs e)
			=> PlayWith(_season.OldestEpisode);

		private async void MenuSeasonDeleteNext_Click(object sender, EventArgs e)
			=> await _deleter.DeleteAsync(_season.OldestEpisode);

		private void MenuSeasonExport_Click(object sender, EventArgs e)
			=> _exporter.Export(_season);

		private ContextMenuStrip BuildEpisodeContextMenu() {
			ContextMenuStrip menu = new ContextMenuStrip();
			menu.Items.AddRange(new ToolStripItem[]{
				new ToolStripMenuItem(ActionText.PlayMenu, Icons.Material_Play18, MenuEpisodePlay_Click),
				new ToolStripMenuItem(ActionText.PlayWithMenu, Icons.Material_PlayWith18, MenuEpisodePlayWith_Click),
				new ToolStripMenuItem(ActionText.ExportMenu, Icons.Material_Export18, MenuEpisodeExport_Click),
				new ToolStripMenuItem(ActionText.DeleteMenu, Icons.Material_Delete18, MenuEpisodeDelete_Click)
			});
			return menu;
		}

		private void MenuEpisodePlay_Click(object sender, EventArgs e)
			=> Play(_episode);

		private void MenuEpisodePlayWith_Click(object sender, EventArgs e)
			=> PlayWith(_episode);

		private void MenuEpisodeExport_Click(object sender, EventArgs e)
			=> _exporter.Export(_episode);

		private async void MenuEpisodeDelete_Click(object sender, EventArgs e)
			=> await _deleter.DeleteAsync(_episode);
		#endregion events
	}
}
