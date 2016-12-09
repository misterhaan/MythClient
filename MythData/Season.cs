using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;

namespace au.Applications.MythClient.Data {
  public class Season : IComparable {
    /// <summary>
    /// Show this season belongs to.
    /// </summary>
    public Show Show { get { return _show; } }
    private Show _show;

    /// <summary>
    /// Recorded episodes from this season.
    /// </summary>
    public List<Episode> Episodes { get { return _episodes; } }
    private List<Episode> _episodes = new List<Episode>();

    /// <summary>
    /// Earliest recording in this Season.
    /// </summary>
    public Episode OldestEpisode { get { return Episodes.Aggregate((min, e) => (min == null || e.FirstAired < min.FirstAired ? e : min)); } }

    /// <summary>
    /// Most recent recording in this Season.
    /// </summary>
    public Episode NewestEpisode { get { return Episodes.Aggregate((max, e) => (max == null || e.FirstAired > max.FirstAired ? e : max)); } }

    /// <summary>
    /// Total duration of all episodes in this season.
    /// </summary>
    public TimeSpan Duration {
      get {
        TimeSpan dur = TimeSpan.FromTicks(0);
        foreach(Episode e in Episodes)
          dur += e.Duration;
        return dur;
      }
    }
    /// <summary>
    /// Season number, or 0 for shows without proper seasons.
    /// </summary>
    public int Number { get { return _number; } }
    private int _number = 0;

    private string _coverUrl;

    /// <summary>
    /// Whether this season has cover art.
    /// </summary>
    public bool HasCover { get { return !string.IsNullOrEmpty(_coverUrl); } }

    /// <summary>
    /// Cover art for this season.
    /// </summary>
    public Image Cover {
      get {
        if(_coverImage == null && HasCover) {
          HttpWebRequest req = (HttpWebRequest)WebRequest.Create(_coverUrl);
          using(HttpWebResponse res = (HttpWebResponse)req.GetResponse())
          using(Stream s = res.GetResponseStream()) {
            _coverImage = Image.FromStream(s);
          }
        }
        return _coverImage;
      }
    }
    private Image _coverImage = null;

    /// <summary>
    /// Create a new Season.
    /// </summary>
    /// <param name="parent">Show this season belongs to</param>
    /// <param name="number">Season number, or 0 for shows without proper seasons</param>
    /// <param name="cover">URL to cover art for this season</param>
    public Season(Show parent, string number, string cover) {
      _show = parent;
      int.TryParse(number, out _number);
      _coverUrl = cover;
    }

    /// <summary>
    /// Sort this season's episodes.
    /// </summary>
    public void Sort() {
      Episodes.Sort();
    }

    /// <summary>
    /// Compares this instance to a specified Season and returns an indication of their relative values.
    /// </summary>
    /// <param name="season">Season to compare</param>
    /// <returns>Comparison result for sorting</returns>
    public int CompareTo(Season season) {
      return Number.CompareTo(season.Number);
    }

    /// <summary>
    /// Compares this instance to a specified Season and returns an indication of their relative values.
    /// </summary>
    /// <param name="obj">Season to compare</param>
    /// <returns>Comparison result for sorting</returns>
    public int CompareTo(object obj) {
      if(obj is Season)
        return CompareTo(obj as Season);
      if(obj is int)
        return Number.CompareTo((int)obj);
      if(obj is string) {
        int number = 0;
        int.TryParse((string)obj, out number);
        return Number.CompareTo(number);
      }
      throw new ArgumentException("Seasons can only compared to another Season, int, or string.");
    }
  }
}
