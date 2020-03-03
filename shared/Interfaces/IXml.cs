using System.Xml.Linq;
public interface IXmlParser
{
    void Parse(System.Xml.XmlNode node);
    
    string ToXml();
}