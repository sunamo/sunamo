using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class CodeElements
    {
        public Dictionary<string, NamespaceCodeElements> namespaces = new Dictionary<string, NamespaceCodeElements>();
        public Dictionary<string, ClassCodeElements> classes = new Dictionary<string, ClassCodeElements>();
    }

