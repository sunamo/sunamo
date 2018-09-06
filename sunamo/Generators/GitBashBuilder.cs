using sunamo.Constants;
using sunamo.Essential;
using sunamo.Generators.Text;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Generators
{
    public class GitBashBuilder
    {
        static TextBuilder sb = new TextBuilder();

        static GitBashBuilder()
        {
            sb.prependEveryNoWhite = AllStrings.space;
        }

        static void Git(string remainCommand)
        {
            Git(sb.sb, remainCommand);
        }

        public List<string> Commands { get => SH.GetLines(ToString()); }

        public static string CreateGitAddForFiles(StringBuilder sb, List<string> linesFiles)
        {
            return Git(sb, "add " + SH.Join(AllChars.space, linesFiles));
        }

        public void Cd(string key)
        {
            sb.AppendLine("cd " + SH.WrapWith( key, AllChars.qm)); 
        }

        

        public void Append(string text)
        {
            sb.Append(text);
        }

        public void AppendLine(string text)
        {
            sb.AppendLine(text);
        }

        public void AppendLine()
        {
            sb.AppendLine();
        }

        public override string ToString()
        {
            return sb.ToString();
        }

        #region Git commands
        public void Clone(string repoUri)
        {
            Git("clone " + repoUri);
            AppendLine();
        }

        public void Commit(bool addAllUntrackedFiles, string commitMessage)
        {
            Git("commit ");
            if (addAllUntrackedFiles)
            {
                Append("-a");
            }
            if (!string.IsNullOrWhiteSpace(commitMessage))
            {
                Append("-m " + SH.WrapWithQm(commitMessage));
            }
            AppendLine();

        }

        public void Push(bool force)
        {
            Git("push");
            if (force)
            {
                Append("-f");
            }
            AppendLine();
        }

        public void Push(string arg)
        {
            Git("push");
            AppendLine(arg);
            AppendLine();
        }

        /// <summary>
        /// myslim si ze chyba spise ne z v initu byla v clone, init se musi udelat i kdyz chci udelat git remote
        /// nikdy nepoustet na adresar ktery ma jiz adresar .git!! jinak se mi zapise s prazdnym obsahem a pri pristim pushi mam po vsem!!! soubory mi odstrani z disku a ne do zadneho kose!!!
        /// </summary>
        public void Init()
        {
            Git("init");
            AppendLine();
        }

        public void Add(string v)
        {
            Git("add");
            Append(v);
            AppendLine();
        }

        /// <summary>
        /// never use, special with dfx argument
        /// d - Remove untracked directories in addition to untracked files.
        /// f - delete all files although conf variable clean.requireForce
        /// x - ignore rules from all .gitignore
        /// 
        /// A1 - arguments without dash
        /// </summary>
        /// <param name="v"></param>
        public void Clean(string v)
        {
            Git("clean");
            Arg(v);
            AppendLine();
        }

        /// <summary>
        /// Not automatically append new line - due to conditionals adding arguments
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="remainCommand"></param>
        /// <returns></returns>
        static string Git(StringBuilder sb, string remainCommand)
        {
            sb.Append("git " + remainCommand);
            return sb.ToString();
        } 
        #endregion


        private void Arg(string v)
        {
            Append("-" + v);
        }

        public void Remote(string arg)
        {
            Git("remote");
            Append(arg);
            AppendLine();
        }
    }
}
