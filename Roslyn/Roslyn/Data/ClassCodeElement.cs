using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class ClassCodeElement : CodeElement<ClassCodeElementsType>
    {
    public override string ToString()
    {
        return SourceCodeIndexerRoslyn.e2sClassCodeElements[Type] + AllStrings.space + Name;
    }
}