using System.Xml.Linq;
public interface IXml
{
    void Parse(System.Xml.XmlNode node);
    void Parse(XElement node);
    string ToXml();
}
