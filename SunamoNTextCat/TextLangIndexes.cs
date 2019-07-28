using System;
using System.Collections.Generic;
using System.Text;


public class TextLangIndexes
{
    public string text;
    /// <summary>
    /// cs index
    /// </summary>
    public double cs;
    /// <summary>
    /// en index
    /// </summary>
    public double en;


    public TextLangIndexes()
    {

    }

    public TextLangIndexes(List<string> line)
    {
        text = line[0];
        cs = double.Parse( line[1]);
        en = double.Parse( line[2]);
    }
}

