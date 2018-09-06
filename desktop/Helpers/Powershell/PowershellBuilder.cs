using sunamo.Constants;
using sunamo.Generators.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.Helpers.Powershell
{
    public class PowershellBuilder
    {
        TextBuilder sb = new TextBuilder();

        public PowershellBuilder()
        {
            sb.prependEveryNoWhite = AllStrings.space;
        }

        /// <summary>
        /// Automatically prepend by space
        /// </summary>
        /// <param name="v"></param>
        public void AddRaw(string v)
        {
            sb.Append(v);
        }

        public void AddArg(string argName, string argValue)
        {
            sb.Append(argName);
            sb.Append(argValue);
        }

        public void CmdC(string v)
        {
            sb.AppendLine("cmd /c " + v);
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}
