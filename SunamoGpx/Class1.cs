using System;
using System.Collections.Generic;

namespace SunamoGpx
{
    public class Class1
    {
        public static void ParseGpxFile(string file)
        {
            //HtmlDocument hd = new HtmlDocument();
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
            using (XmlReader reader = XmlReader.Create(file))
            {
                gpx = (csGeoTools.Parsers.gpx.gpx10.Gpx)ser.Deserialize(reader);
            }
            foreach (var item in gpx.Waypoints)
            {
                string type = item.Type;
                if (type.StartsWith("Geocache|"))
                {
                    Console.WriteLine(item.);
                    wpt.Add(item);
                }
            }


        }
    }
}
