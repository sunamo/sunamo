using csGeoTools.Parsers.gpx.gpx10;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SunamoGpx
{
    public class GpxHelper
    {
        /// <summary>
        /// A1 must be object to pass stream from wpf and uwp
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static List<Waypoint> ParseGpxFile(string  content)
        {
            //HtmlDocument hd = HtmlAgilityHelper.CreateHtmlDocument();
            //hd.LoadHtml(TF.ReadFile(file));

            //var wpts = HtmlAgilityHelper.Nodes(hd.DocumentNode, true, "wpt");
            //foreach (var item in wpts)
            //{
            //    var sym = HtmlAssistant.InnerText(item, false, "sym");
            //    if (sym == "Geocache")
            //    {

            //    } 
            //}

            List<Waypoint> wpt = new List<Waypoint>();

            Type gpxType = typeof(csGeoTools.Parsers.gpx.gpx10.Gpx);
            XmlSerializer ser = new XmlSerializer(gpxType);
            csGeoTools.Parsers.gpx.gpx10.Gpx gpx;
            using (XmlReader reader = XmlReader.Create(new StringReader(content)))
            {
                // Cant be use in UWP - in classic windows this project is good. In UWP parses nothing.
                gpx = (csGeoTools.Parsers.gpx.gpx10.Gpx)ser.Deserialize(reader);
            }
            foreach (var item in gpx.Waypoints)
            {
                string type = item.Type;
                if (type.StartsWith("Geocache|"))
                {
                    wpt.Add(item);
                }
            }

            return wpt;
        }
    }
}
