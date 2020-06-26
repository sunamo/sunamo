using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using desktop;

/// <summary>
/// Replacement for TextBlockHelperBase
/// Hyperlink is clickable
/// </summary>
public class InlineBuilderBlock : InlineBuilder
{
    /// <summary>
    /// Block put into FlowDocument.Blocks
    /// </summary>
    /// <param name="fd"></param>
    public InlineBuilderBlock(Paragraph fd) : base(fd.Inlines)
    {
    }
}