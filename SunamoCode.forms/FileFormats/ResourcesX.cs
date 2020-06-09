using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SunamoCode
{
    /// <summary>
    /// Čtení a zápis prostředků *.resx
    /// Navíc od *.resw mají 2 parametry type v elementu data:
    /// 1) type="System.Resources.ResXFileRef, System.Windows.Forms"
    /// 2) type="System.Byte[], mscorlib"
    /// 3) Jakýkoliv typ implementující ISerializable type="System.Drawing.Bitmap, System.Drawing" mimetype="application/x-microsoft.net.object.bytearray.base64"
    /// </summary>
    public class ResourcesX
    {
        public static void AddFileToResources()
        {
            
        }

        /// <summary>
        /// https://stackoverflow.com/a/40509690
        /// </summary>
        /// <param name="data"></param>
        /// <param name="path"></param>
        public static void UpdateResourceFile(Hashtable data, String path)
        {
            Hashtable resourceEntries = new Hashtable();

            //Get existing resources
            ResXResourceReader reader = new ResXResourceReader(path);
            reader.UseResXDataNodes = true;
            ResXResourceWriter resourceWriter = new ResXResourceWriter(path);
            System.ComponentModel.Design.ITypeResolutionService typeres = null;
            if (reader != null)
            {
                IDictionaryEnumerator id = reader.GetEnumerator();
                foreach (DictionaryEntry d in reader)
                {
                    //Read from file:
                    string val = "";
                    if (d.Value == null)
                        resourceEntries.Add(d.Key.ToString(), "");
                    else
                    {
                        //..\sunamo\multilingualresources\sunamo.cs-cz.xlf
                        // D:\Documents\Visual Studio 2017\Projects\sunamo.Tests\SunamoCode.forms.Tests\bin\sunamo\multilingualresources\sunamo.cs-cz.xlf
                        val = ((ResXDataNode)d.Value).GetValue(typeres).ToString();
                        resourceEntries.Add(d.Key.ToString(), val);

                    }

                    //Write (with read to keep xml file order)
                    ResXDataNode dataNode = (ResXDataNode)d.Value;

                    //resourceWriter.AddResource(d.Key.ToString(), val);
                    resourceWriter.AddResource(dataNode);

                }
                reader.Close();
            }

            //Add new data (at the end of the file):
            Hashtable newRes = new Hashtable();
            foreach (String key in data.Keys)
            {
                if (!resourceEntries.ContainsKey(key))
                {

                    String value = data[key].ToString();
                    if (value == null) value = "";

                    resourceWriter.AddResource(key, value);
                }
            }

            //Write to file
            resourceWriter.Generate();
            resourceWriter.Close();

        }
    }
}