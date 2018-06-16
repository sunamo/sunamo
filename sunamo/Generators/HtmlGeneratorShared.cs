using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Generators
{
    public class HtmlGeneratorShared : IHtmlGeneratorShared
    {
        HtmlGenerator generator = null;
        

        public HtmlGeneratorShared( HtmlGenerator generator)
        {
            
            this.generator = generator;
        }

        public void WriteBr()
        {
            generator.WriteNonPairTag("br");
        }
    }
}
