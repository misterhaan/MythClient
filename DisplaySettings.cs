using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using au.util.io;
using System.Collections.Generic;

namespace au.Applications.MythClient {
  public class DisplaySettings {
    public FormWindowState WindowState = FormWindowState.Normal;
    public Point Location = new Point(-42, -42);
    public Size Size = new Size(-42, -42);

    public List<string> ColumnHeadings = new List<string>();
    public List<int> ColumnWidths = new List<int>();

    public DisplaySettings() { }

    /// <summary>
    /// Create new display settings based on the contents of a display XML node.
    /// </summary>
    /// <param name="display">Display XML node.</param>
    public DisplaySettings(XmlNode display) {
      foreach(XmlNode node in display.ChildNodes)
        try {
          switch(node.Name) {
            case "WindowState":
              WindowState = (FormWindowState)int.Parse(node.InnerText);
              break;
            case "Location":
              string[] coords = node.InnerText.Split(',');
              Location = new Point(int.Parse(coords[0]), int.Parse(coords[1]));
              break;
            case "Size":
              string[] size = node.InnerText.Split('x');
              Size = new Size(int.Parse(size[0]), int.Parse(size[1]));
              break;
            case "Columns":
              ColumnHeadings.Clear();
              ColumnWidths.Clear();
              List<int> widths = new List<int>();
              foreach(XmlNode col in node.ChildNodes) {
                string[] coldata = col.InnerText.Split(':');
                ColumnHeadings.Add(coldata[0]);
                ColumnWidths.Add(int.Parse(coldata[1]));
              }
              break;
          }
        } catch(Exception e) {
          Trace.WriteLine("au.Applications.MythClient.DisplaySettings.DisplaySettings() ERROR reading node " + node.Name + " so it was skipped.  Details:\n" + e);
        }
    }

    /// <summary>
    /// Save the display settings to the specified XML node.
    /// </summary>
    /// <param name="display">XML node display settings should be added to.</param>
    public void SaveTo(XmlNode display) {
      if(WindowState != FormWindowState.Normal)
        display.AddElement("WindowState", (int)WindowState);
      if(Location.X != -42 && Location.Y != -42)
        display.AddElement("Location", string.Format("{0},{1}", Location.X, Location.Y));
      if(Size.Width != -42 && Size.Height != -42)
        display.AddElement("Size", string.Format("{0}x{1}", Size.Width, Size.Height));
      if(ColumnHeadings.Count > 0) {
        XmlElement cols = display.AddElement("Columns");
        for(int c = 0; c < ColumnHeadings.Count; c++)
          cols.AddElement("Column", string.Format("{0}:{1}", ColumnHeadings[c], ColumnWidths[c]));
      }
    }
  }
}
