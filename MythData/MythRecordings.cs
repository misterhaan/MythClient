using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Xml;
using au.util.io;

namespace au.Applications.MythClient.Data {
  public class MythRecordings {
    // written for <Version>0.28.20161120-1</Version><ProtoVer>88</ProtoVer>

    public const int DefaultMythtvPort = 6544;
    private const string GET_LIST = "http://{0}:{1}/Dvr/GetRecordedList";
    private const string GET_THUMB = "http://{0}:{1}/Content/GetPreviewImage?RecordedId={2}";  // 320 x 180
    private const string DELETE_RECORDING = "http://{0}:{1}/Dvr/DeleteRecording?RecordedId={2}&ForceDelete=true&AllowRerecord={3}";  // without ForceDelete it just gets marked deleted

    private string _mythHost;
    private int _mythPort;

    /// <summary>
    /// Shows that have at least one recording.
    /// </summary>
    public List<Show> Shows { get { return _shows; } }
    private List<Show> _shows = new List<Show>();

    public int NumEpisodes {
      get {
        int num = 0;
        foreach(Show s in Shows)
          num += s.NumEpisodes;
        return num;
      }
    }

    /// <summary>
    /// Create a new set of recordings data for the specified MythTV backend.
    /// </summary>
    /// <param name="mythhost">Hostname or IP address for the MythTV backend</param>
    /// <param name="mythport">Services API port for the MythTV backend</param>
    public MythRecordings(string mythhost, int mythport) {
      _mythHost = mythhost;
      _mythPort = mythport;
      Refresh();
    }

    /// <summary>
    /// Create a new set of recordings data for the specified MythTV backend hostname.
    /// </summary>
    /// <param name="mythhost">Hostname or IP address for the MythTV backend</param>
    public MythRecordings(string host) : this(host, DefaultMythtvPort) { }

    /// <summary>
    /// Check whether the specified show has any recordings.
    /// </summary>
    /// <param name="s">Show to find</param>
    /// <returns>True if there are recordings for the specified show</returns>
    public bool Contains(Show s) {
      return Shows.Contains(s);
    }
    /// <summary>
    /// Check whether the specified season has any recordings.
    /// </summary>
    /// <param name="s">Season to find</param>
    /// <returns>True if there are recordings for the specified Season</returns>
    public bool Contains(Season s) {
      return Shows.Contains(s.Show) && s.Show.Seasons.Contains(s);
    }
    /// <summary>
    /// Check whether the specified episode is recorded.
    /// </summary>
    /// <param name="e">Episode to find</param>
    /// <returns>True if the specified Episode is recorded</returns>
    public bool Contains(Episode e) {
      return Shows.Contains(e.Season.Show) && e.Season.Show.Seasons.Contains(e.Season) && e.Season.Episodes.Contains(e);
    }
    /// <summary>
    /// Check whether the specified episode, season, or show has any recordings.
    /// </summary>
    /// <param name="o">Episode, Season, or Show to find</param>
    /// <returns>True if the specified Episode, Season, or Show has any recordings</returns>
    public bool Contains(object o) {
      if(o is Show) return Contains((Show)o);
      if(o is Season) return Contains((Season)o);
      if(o is Episode) return Contains((Episode)o);
      return false;
    }

    /// <summary>
    /// Get the list of recordings from the MythTV backend.
    /// </summary>
    public void Refresh() {
      XmlDocument xml = new XmlDocument();
      HttpWebRequest req = (HttpWebRequest)WebRequest.Create(string.Format(GET_LIST, _mythHost, _mythPort));
      using(HttpWebResponse res = (HttpWebResponse)req.GetResponse())
      using(Stream s = res.GetResponseStream()) {
        xml.Load(s);
        Shows.Clear();
        XmlElement programList = xml["ProgramList"];
        XmlElement programs = programList["Programs"];
        foreach(XmlNode node in programs.ChildNodes)
          if(node.NodeType == XmlNodeType.Element && node.Name == "Program") {
            XmlElement program = (XmlElement)node;
            if(!program.ElementValue("Recording/RecGroup").Equals("Deleted", System.StringComparison.CurrentCultureIgnoreCase)) {
              Show show = GetShow(program.ElementValue("Title"));
              Season season = show.GetSeason(program.ElementValue("Season"), FindCoverArt(program));
              Episode episode = new Episode(season,
                program.ElementValue("Recording/RecordedId"),
                program.ElementValue("FileName"),
                program.ElementValue("SubTitle"),
                program.ElementValue("Episode"),
                program.ElementValue("Airdate"),
                program.ElementValue("StartTime"),
                program.ElementValue("EndTime"),
                program.ElementValue("Recording/Status")
              );
              season.Episodes.Add(episode);
            }
          }
      }
      Sort();
    }

    public void Sort() {
      Shows.Sort();
      foreach(Show show in Shows)
        show.Sort();
    }

    /// <summary>
    /// Find cover art for the program from the XML.
    /// </summary>
    /// <param name="program">Program element to find art in</param>
    /// <returns>URL to the cover art</returns>
    private string FindCoverArt(XmlElement program) {
      XmlElement artwork = program["Artwork"];
      if(artwork != null) {
        XmlElement infos = artwork["ArtworkInfos"];
        if(infos != null)
          foreach(XmlNode node in infos.ChildNodes)
            if(node.NodeType == XmlNodeType.Element && node.Name == "ArtworkInfo") {
              XmlElement url = node["URL"];
              if(url != null && url.HasChildNodes) {
                XmlElement type = node["Type"];
                if(type != null && type.HasChildNodes)
                  switch(type.FirstChild.Value.Trim()) {
                    case "coverart":
                      return string.Format("http://{0}:{1}{2}&Width=200&Height=289", _mythHost, _mythPort, url.FirstChild.Value.Trim());
                  }
              }
            }
      }
      return null;
    }

    /// <summary>
    /// Download a thumbnail for the episode with the specified recording ID.
    /// </summary>
    /// <param name="recordedId">ID of the recording to get a thumbnail of</param>
    /// <returns>Thumbnail image</returns>
    public Image GetEpisodeThumb(string recordedId) {
      try {
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(string.Format(GET_THUMB, _mythHost, _mythPort, recordedId));
        using(HttpWebResponse res = (HttpWebResponse)req.GetResponse())
        using(Stream s = res.GetResponseStream()) {
          return Image.FromStream(s);
        }
      } catch {
        return null;
      }
    }

    /// <summary>
    /// Delete the specified episode.
    /// </summary>
    /// <param name="recordedID">RecordedId value for the episode to delete</param>
    /// <param name="rerecord">whether the episode should be re-recorded</param>
    /// <returns>Whether the episode was deleted</returns>
    public bool DeleteRecording(string recordedId, bool rerecord) {
      HttpWebRequest req = (HttpWebRequest)WebRequest.Create(string.Format(DELETE_RECORDING, _mythHost, _mythPort, recordedId, rerecord));
      req.Method = "POST";
      using(HttpWebResponse res = (HttpWebResponse)req.GetResponse())
      using(Stream s = res.GetResponseStream()) {
        XmlDocument xml = new XmlDocument();
        xml.Load(s);
        XmlElement resval = xml["bool"];
        if(resval != null && resval.HasChildNodes)
          return bool.Parse(resval.FirstChild.Value.Trim());
      }
      return false;
    }

    /// <summary>
    /// Find the existing show or add the show if not yet present.
    /// </summary>
    /// <param name="title">Show title to find or add</param>
    /// <returns>Found or added show</returns>
    public Show GetShow(string title) {
      foreach(Show show in Shows)
        if(show.Title == title)
          return show;
      Show s = new Show(this, title);
      Shows.Add(s);
      return s;
    }

    /// <summary>
    /// Determines whether this MythRecordings uses the MythTV Service host and port specifide.
    /// </summary>
    /// <param name="mythHost">Hostname or IP of the MythTV server</param>
    /// <param name="mythPort">Port number for the MythTV Services API</param>
    /// <returns>True when matching</returns>
    public bool Equals(string mythHost, int mythPort) {
      return _mythHost.Equals(mythHost, StringComparison.InvariantCultureIgnoreCase) && _mythPort == mythPort;
    }
  }
}
