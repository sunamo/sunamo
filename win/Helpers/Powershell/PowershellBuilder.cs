using sunamo.Constants;
using sunamo.Generators.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace win.Helpers.Powershell
{
    public class PowershellBuilder
    {
        TextBuilder sb = null;
        static PowershellBuilder instance = new PowershellBuilder();

        public PowershellBuilder()
        {
            sb = new TextBuilder(true);
            sb.prependEveryNoWhite = AllStrings.space;
        }

        public void Clear()
        {
            sb.Clear();
        }

        /// <summary>
        /// Dont postfix with NewLine
        /// Automatically prepend by space
        /// </summary>
        /// <param name="v"></param>
        public void AddRaw(string v)
        {
            sb.Append(v);
        }

        public void AddRawLine(string v)
        {
            sb.AppendLine( v);
        }

        public void AddArg(string argName, string argValue)
        {
            sb.Append(argName);
            sb.Append(argValue);
        }

        public static PowershellBuilder GetInstance()
        {
            instance.Clear();
            return instance;
        }

        /// <summary>
        /// Returns string because of PowershellRunner
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string Cd(string path)
        {
            sb.AppendLine("cd \"" + path + AllStrings.qm);
            return sb.ToString();
        }

        public void CmdC(string v)
        {
            sb.AppendLine("cmd /c " + v);
        }

        public override string ToString()
        {
            return sb.ToString();
        }

        public List<string> ToList()
        {
            return sb.list;
        }
    }
}
