using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using au.Applications.MythClient.Data;
using au.util.io;

namespace au.Applications.MythClient {
  public class MythSettings {
    private const string FILENAME = "MythClient.settings.xml";

    /// <summary>
    /// Full path to the settings file on disk.
    /// </summary>
    private string SettingsFilePath {
      get {
        if(string.IsNullOrEmpty(_settingsFilePath)) {
          _settingsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FILENAME);
        }
        return _settingsFilePath;
      }
    }
    private string _settingsFilePath = null;

    public string ServerName = null;
    public int ServerPort = MythRecordings.DefaultMythtvPort;
    public string RawFilesDirectory = null;
    public string LastExportDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
    public RecordingSortOption SortOption = RecordingSortOption.Title;

    /// <summary>
    /// Settings pertaining to the display.
    /// </summary>
    public DisplaySettings Display {
      get {
        if(_display == null)
          _display = new DisplaySettings();
        return _display;
      }
      private set { _display = value; }
    }
    private DisplaySettings _display;

    public bool Load() {
      if(File.Exists(SettingsFilePath)) {
        using(FileStream stream = File.Open(SettingsFilePath, FileMode.Open, FileAccess.Read, FileShare.Read)) {
          XmlDocument xml = new XmlDocument();
          xml.Load(stream);
          foreach(XmlNode node in xml.LastChild.ChildNodes)  // LastChild because the document's children are the xml declaration and then the root node
            try {
              switch(node.Name) {
                case "ServerName":
                  ServerName = node.InnerText;
                  break;
                case "ServerPort":
                  int.TryParse(node.InnerText, out ServerPort);
                  break;
                case "RawFilesDirectory":
                  RawFilesDirectory = node.InnerText;
                  break;
                case "LastExportDirectory":
                  LastExportDirectory = node.InnerText;
                  try {
                    while(!Directory.Exists(LastExportDirectory)) {
                      LastExportDirectory = Directory.GetParent(LastExportDirectory).FullName;
                    }
                  } catch {
                    LastExportDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
                  }
                  break;
                case "SortOption":
                  int so;
                  if(int.TryParse(node.InnerText, out so))
                    switch(so) {
                      case (int)RecordingSortOption.Title:
                        SortOption = RecordingSortOption.Title;
                        break;
                      case (int)RecordingSortOption.OldestRecorded:
                        SortOption = RecordingSortOption.OldestRecorded;
                        break;
                    }
                  break;
                case "Display":
                  Display = new DisplaySettings(node);
                  break;
                case "Export":  // pre-1.0 setting that translates to 1.0
                  LastExportDirectory = ((XmlElement)node).ElementValue("Where");
                  try {
                    while(!Directory.Exists(LastExportDirectory)) {
                      LastExportDirectory = Directory.GetParent(LastExportDirectory).FullName;
                    }
                  } catch {
                    LastExportDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
                  }
                  break;
              }
            } catch(Exception e) {
              Trace.WriteLine("MythSettings.Load() ERROR reading node " + node.Name + " so it was skipped.  Details:\n" + e);
            }
        }
        return true;
      }
      return false;
    }

    public void Save() {
      XmlDocument xml = new XmlDocument();
      xml.AppendChild(xml.CreateXmlDeclaration("1.0", "UTF-8", null));
      XmlNode myth = xml.CreateElement("MythClient");
      xml.AppendChild(myth);
      if(!string.IsNullOrEmpty(ServerName))
        myth.AddElement("ServerName", ServerName);
      if(ServerPort != MythRecordings.DefaultMythtvPort)
        myth.AddElement("ServerPort", ServerPort);
      if(!string.IsNullOrEmpty(RawFilesDirectory))
        myth.AddElement("RawFilesDirectory", RawFilesDirectory);
      if(LastExportDirectory != Environment.GetFolderPath(Environment.SpecialFolder.MyVideos))
        myth.AddElement("LastExportDirectory", LastExportDirectory);
      if(SortOption != RecordingSortOption.Title)
        myth.AddElement("SortOption", (int)SortOption);
      XmlNode disp = myth.AddElement("Display");
      Display.SaveTo(disp);
      using(FileStream stream = File.Open(SettingsFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
        xml.Save(stream);
    }
  }
}
