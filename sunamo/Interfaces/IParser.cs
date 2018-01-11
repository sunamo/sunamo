using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
    public interface IParseCollection
    {
        /// <summary>
        /// Pro opacny proces slouzi M ToString().
        /// </summary>
        void ParseCollection(IEnumerable<string> soubory);
    }

    public interface IParser
    {
        void Parse(string co);
    }
}
