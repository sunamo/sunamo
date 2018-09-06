using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;

namespace apps
{
    public static class XmlDocumentExtensions
    {
        public static async Task<XmlDocument> Load(this XmlDocument xd, string file)
        {
            XmlDocument xd2 = new XmlDocument();
            xd2.LoadXml(await TF.ReadFile(await StorageFile.GetFileFromPathAsync(file)));
            return xd2;
        }
    }
}
