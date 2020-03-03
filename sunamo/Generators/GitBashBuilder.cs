using sunamo.Constants;
using sunamo.Essential;
using sunamo.Generators.Text;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace sunamo.Generators
{
    /// <summary>
    /// GitBashBuilder
    /// </summary>
    public class GitBashBuilder
    {
        private static Type s_type = typeof(GitBashBuilder);
        public TextBuilder sb = new TextBuilder();

        public GitBashBuilder()
        {
            sb.prependEveryNoWhite = AllStrings.space;
        }

        private void Git(string remainCommand)
        {
            Git(sb.sb, remainCommand);
        }

        public List<string> Commands { get => SH.GetLines(ToString()); }

        /// <summary>
        /// A2 must be files prepared to cmd
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="linesFiles"></param>
        
        private static string Git(StringBuilder sb, string remainCommand)
        {
            sb.Append("git " + remainCommand);
            return sb.ToString();
        }
        #endregion


        private void Arg(string v)
        {
            Append(AllStrings.dash + v);
        }

        public void Remote(string arg)
        {
            Git("remote");
            Append(arg);
            AppendLine();
        }

        public void Status()
        {
            Git("status");
            AppendLine();
        }

        public void Fetch()
        {
            Git("fetch");
            AppendLine();
        }

        public void Merge(string v)
        {
            Git("merge" + " " + v);
            AppendLine();
        }
    }
}
