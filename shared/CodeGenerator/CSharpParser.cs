using sunamo.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.CodeGenerator
{
    public class CSharpParser
    {
        public bool IsMethod(string line)
        {
            return SH.ContainsAll(line, AllStrings.lb, AllStrings.rb);
        }
    }
}
