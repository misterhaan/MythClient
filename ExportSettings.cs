using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using au.util.io;

namespace au.Applications.MythClient {
  public class ExportSettings {
    public enum WhatType {
      Episode,
      Show,
      All
    }

    public enum HowType {
      ShowDateEpisode,
      Show
    }

    public WhatType What = WhatType.Episode;
    public HowType How = HowType.ShowDateEpisode;
    public string Where = null;

    public ExportSettings() { }

    /// <summary>
    /// Create new export settings based on the contents of an export XML node.
    /// </summary>
    /// <param name="export">Export XML node.</param>
    public ExportSettings(XmlNode export) {
      foreach(XmlNode node in export.ChildNodes)
        try {
          switch(node.Name) {
            case "What":
              What = (WhatType)int.Parse(node.InnerText);
              break;
            case "How":
              How = (HowType)int.Parse(node.InnerText);
              break;
            case "Where":
              Where = node.InnerText;
              try {
                while(!Directory.Exists(Where)) {
                  Where = Directory.GetParent(Where).FullName;
                }
              } catch {
                Where = null;
              }
              break;
          }
        } catch(Exception e) {
          Trace.WriteLine("au.Applications.MythClient.ExportSettings.ExportSettings() ERROR reading node " + node.Name + " so it was skipped.  Details:\n" + e);
        }
    }

    /// <summary>
    /// Save the export settings to the specified XML node.
    /// </summary>
    /// <param name="export">XML node export settings should be added to.</param>
    public void SaveTo(XmlNode export) {
      if(What != WhatType.Episode)
        export.AddElement("What", (int)What);
      if(How != HowType.ShowDateEpisode)
        export.AddElement("How", (int)How);
      if(!string.IsNullOrEmpty(Where))
        export.AddElement("Where", Where);
    }
  }
}
