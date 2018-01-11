using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
    public interface IParseCollectionO
    {
        /// <summary>
        /// Pro opacny proces slouzi M ToString().
        /// </summary>
        void ParseCollection(IEnumerable<object> soubory);
    }

    public interface IParserO
    {
        void Parse(object co);
    }
}
