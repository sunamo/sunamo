using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum NamespaceCodeElementsType
{
    Nope = 0,
    Enum = 1,
    Class = 2,
    Interface = 4,
    Struct = 8,
    All = 255
}