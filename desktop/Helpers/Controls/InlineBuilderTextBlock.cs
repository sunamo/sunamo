using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public class InlineBuilderTextBlock : InlineBuilder
{
    public TextBlock tb = null;

    public InlineBuilderTextBlock(TextBlock tb) : base(tb.Inlines)
    {
        this.tb = tb;
    }

    public InlineBuilderTextBlock()
    {
        tb = new TextBlock();
        inlines = tb.Inlines;
    }
}