﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

public partial class GridHelper
{
    public static readonly GridLength glAuto = new System.Windows.GridLength(1, System.Windows.GridUnitType.Auto);
    /// <summary>
    /// Cant be used due to
    /// ''value' already belongs to another 'RowDefinitionCollection'.'
    /// </summary>
    public static readonly RowDefinition rdAuto = GetRowDefinition(glAuto);
    /// <summary>
    /// Cant be used due to
    /// ''value' already belongs to another 'RowDefinitionCollection'.'
    /// </summary>
    public static readonly ColumnDefinition cdAuto = GetColumnDefinition(glAuto);


    /// <summary>
    /// After can be use with XamlGeneratorDesktop.Write*Definitions 
    /// </summary>
    /// <param name="columns"></param>
    /// <returns></returns>
    public static List<string> ForAllTheSame(int columns)
    {
        List<string> result = new List<string>(columns);
        var d = 100d / (double)columns;
        for (int i = 0; i < columns; i++)
        {
            result.Add(d + AllStrings.asterisk);
        }

        return result;
    }

    public static void ForAllTheSameRuntime(Grid grid, int count, bool columns)
    {
        var d = 100d / (double)count;
        for (int i = 0; i < count; i++)
        {
            if (columns)
            {
                grid.ColumnDefinitions.Add(GetColumnDefinitionDirect(1, System.Windows.GridUnitType.Star));
            }
            else
            {
                grid.RowDefinitions.Add(GetRowDefinitionDirect(1, System.Windows.GridUnitType.Star));
            }
        }

    }
}