using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace apps
{
    public interface IXmlCollection
    {
        //void Parse(IEnumerable<XmlElement> node);
        void Parse(IEnumerable<XElement> node);
        string ToXml();
    }

    public interface IXmlCollectionEnumerable : IXmlCollection, IEnumerable
    { 
    }


    public interface IXmlCollectionEnumerable<T> : IXmlCollection, IEnumerable
    {
        void Add(T t);
    }

    public interface IXmlCollectionWithIndexer<Key, Value> : IXmlCollection, IEnumerable<Value> //IDictionary<Key, Value> //
    {
        Value this[Key key] { get; set; }
    }
}
