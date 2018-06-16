using sunamo.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.Helpers.Powershell
{
    public class PowershellBuilder
    {
        InstantSB sb = new InstantSB(AllStrings.space);

        /// <summary>
        /// A1 without ./
        /// </summary>
        /// <param name="command"></param>
        public PowershellBuilder(string command)
        {
            sb.AddItem("./" + command);
        }

        public void AddRaw(string content)
        {
            sb.AddItem(content);
        }

        public void Add(string arg, string value)
        {
            sb.AddItem(arg);
            sb.AddItem(value);
        }
    }
}
