using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace au.Applications.MythClient.Data {
  public class Show : IComparable {
    /// <summary>
    /// Recordings object this show belongs to.
    /// </summary>
    public MythRecordings Recordings { get { return _recordings; } }
    private MythRecordings _recordings;

    /// <summary>
    /// Show title.
    /// </summary>
    public string Title { get { return _title; } }

    /// <summary>
    /// Title for sorting.
    /// </summary>
    private string SortTitle {
    get {
        if(_title.StartsWith("The ", StringComparison.CurrentCultureIgnoreCase))
          return _title.Substring(4);
        if(_title.StartsWith("An ", StringComparison.CurrentCultureIgnoreCase))
          return _title.Substring(3);
        if(_title.StartsWith("A ", StringComparison.CurrentCultureIgnoreCase))
          return _title.Substring(2);
        return _title;
      }
    }
    private string _title;

    /// <summary>
    /// Seasons of this show that have recorded episodes.  Shows without proper seasons will have one season numbered zero.
    /// </summary>
    public List<Season> Seasons { get { return _seasons; } }
    private List<Season> _seasons = new List<Season>();

    /// <summary>
    /// Create a show that has at least one recorded episode.
    /// </summary>
    /// <param name="parent">MythRecordings object that will contain the new show</param>
    /// <param name="title">Show title</param>
    public Show(MythRecordings parent, string title) {
      _recordings = parent;
      _title = title;
    }

    /// <summary>
    /// Number of episodes currently recorded of this show.
    /// </summary>
    public int NumEpisodes {
      get {
        int num = 0;
        foreach(Season s in Seasons)
          num += s.Episodes.Count;
        return num;
      }
    }

    /// <summary>
    /// Earliest recording of this Show.
    /// </summary>
    public Episode OldestEpisode {
      get {
        Episode oldest = null;
        foreach(Season s in Seasons)
          if(oldest == null || s.OldestEpisode.FirstAired < oldest.FirstAired)
            oldest = s.OldestEpisode;
        return oldest;
      }
    }

    /// <summary>
    /// Most recent recording of this Show.
    /// </summary>
    public Episode NewestEpisode {
      get {
        Episode newest = null;
        foreach(Season s in Seasons)
          if(newest == null || s.NewestEpisode.FirstAired > newest.FirstAired)
            newest = s.NewestEpisode;
        return newest;
      }
    }

    /// <summary>
    /// Total duration of all episodes of this show.
    /// </summary>
    public TimeSpan Duration {
      get {
        TimeSpan dur = TimeSpan.FromTicks(0);
        foreach(Season s in Seasons)
          foreach(Episode e in s.Episodes)
            dur += e.Duration;
        return dur;
      }
    }

    /// <summary>
    /// Cover art for the oldest recorded season of this show.
    /// </summary>
    public Image Cover {
      get {
        if(_coverImage == null)
          if(Seasons.Count(s => s.HasCover) > 0)
            _coverImage = Seasons.Where(s => s.HasCover).First().Cover;
        return _coverImage;
      }
    }
    private Image _coverImage = null;

    /// <summary>
    /// Sort the seasons for this show and the episodes in each season.
    /// </summary>
    public void Sort() {
      Seasons.Sort();
      foreach(Season s in Seasons)
        s.Sort();
    }

    /// <summary>
    /// Find the existing season or add the show if not yet present.
    /// </summary>
    /// <param name="number">Season number (used to match existing seasons; should parse as an integer)</param>
    /// <param name="cover">URL to cover art (only used if season is added)</param>
    /// <returns>Found or added season</returns>
    public Season GetSeason(string number, string cover) {
      int epnum = 0;
      int.TryParse(number, out epnum);
      foreach(Season s in Seasons)
        if(s.Number == epnum)
          return s;
      Season season = new Season(this, number, cover);
      Seasons.Add(season);
      return season;
    }

    /// <summary>
    /// Compares this Show with another and indicates whether this Show
    /// precedes, follows, or appears in the same position in the sort order
    /// as the specified Show.
    /// </summary>
    /// <param name="show">Show to compare</param>
    /// <returns>Sort relation between this Show and show</returns>
    public int CompareTo(Show show) {
      switch(Recordings.SortOption) {
        case RecordingSortOption.OldestRecorded:
          return OldestEpisode.FirstAired.CompareTo(show.OldestEpisode.FirstAired);
        case RecordingSortOption.Title:
        default:
          return SortTitle.CompareTo(show.SortTitle);
      }
    }

    /// <summary>
    /// Compares this Show with another and indicates whether this Show
    /// precedes, follows, or appears in the same position in the sort order
    /// as the specified Show.
    /// </summary>
    /// <param name="obj">An object that evaluates to a Show</param>
    /// <returns>Sort relation between this Show and obj</returns>
    public int CompareTo(object obj) {
      if(obj is Show)
        return CompareTo(obj as Show);
      throw new NotImplementedException();
    }
  }
}
