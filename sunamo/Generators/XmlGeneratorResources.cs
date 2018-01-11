using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public  class XmlGeneratorResources
    {
     XmlGenerator xml = new XmlGenerator();

    public  void AddText(string name, string text)
    {
        xml.WriteTagWith2Attrs("data", "name", name, "xml:space", "preserve");
        xml.WriteElement("value", text);
        xml.TerminateTag("data");
    }

    public override string ToString()
    {
        return xml.ToString();
    }

}
