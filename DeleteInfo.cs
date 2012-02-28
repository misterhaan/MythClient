using System.Text.RegularExpressions;

namespace au.Applications.MythClient {
  public struct DeleteInfo {
    public string title;
    public string subtitle;
    public string chanid;
    public string starttime;
    public string recgroup;
    public string filename;
    public string size;
    public string length;
    public int autoexpire;

    public DeleteInfo(Match m) {
      title = m.Groups[1].Captures[0].Value.Replace("\\'", "'");
      subtitle = m.Groups[3].Captures[0].Value.Replace("\\'", "'");
      chanid = m.Groups[5].Captures[0].Value;
      starttime = m.Groups[6].Captures[0].Value;
      recgroup = m.Groups[7].Captures[0].Value;
      filename = m.Groups[8].Captures[0].Value;
      size = m.Groups[9].Captures[0].Value;
      length = m.Groups[10].Captures[0].Value;
      autoexpire = int.Parse(m.Groups[11].Captures[0].Value);
    }
  }
}
