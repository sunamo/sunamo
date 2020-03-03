using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ConvertToVarcharArgs
{
    public CollectionWithoutDuplicates<char> notSupportedChars = new CollectionWithoutDuplicates<char>();
    public Dictionary<string, List<string>> changed = new Dictionary<string, List<string>>();
}