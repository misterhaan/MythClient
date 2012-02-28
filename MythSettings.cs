﻿using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
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
    public string RecordedProgramsUrlSetting = null;
    public string RawFilesDirectory = null;

    public string RecordedProgramsUrl {
      get {
        if(string.IsNullOrEmpty(RecordedProgramsUrlSetting) && !string.IsNullOrEmpty(ServerName))
          return "http://" + ServerName + "/mythweb/tv/recorded";
        return RecordedProgramsUrlSetting;
      }
    }

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
                case "RecordedProgramsUrl":
                  RecordedProgramsUrlSetting = node.InnerText;
                  break;
                case "RawFilesDirectory":
                  RawFilesDirectory = node.InnerText;
                  break;
                case "Display":
                  Display = new DisplaySettings(node);
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
      if(!string.IsNullOrEmpty(RecordedProgramsUrlSetting))
        myth.AddElement("RecordedProgramsUrl", RecordedProgramsUrlSetting);
      if(!string.IsNullOrEmpty(RawFilesDirectory))
        myth.AddElement("RawFilesDirectory", RawFilesDirectory);
      XmlNode disp = myth.AddElement("Display");
      Display.SaveTo(disp);
      using(FileStream stream = File.Open(SettingsFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
        xml.Save(stream);
    }
  }
}
