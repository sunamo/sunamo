using sunamo.Constants;
using sunamo.Essential;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Generators
{
    public class GitBashGenerator
    {
        

        public static string CreateGitAddForFiles( List<string> linesFiles)
        {
            return "git add " + SH.Join(AllChars.space, linesFiles);
        }
    }
}
