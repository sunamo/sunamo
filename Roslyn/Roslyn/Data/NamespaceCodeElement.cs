using sunamo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    public class NamespaceCodeElement : CodeElement<NamespaceCodeElementsType>
    {
        public override string ToString()
        {
            return SourceCodeIndexerRoslyn.e2sNamespaceCodeElements[Type] + AllStrings.space + Name;
        }
    }