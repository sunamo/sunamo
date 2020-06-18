﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using sunamo;

public class ControlInitData
{
    #region Sort by alphabet (same as in intellisense popup)
    public ButtonAction action;
    public bool checkable = false;
    public object content = null;
    public Brush foreground = null;
    public string group = null;
    public double imageHeight = 16;
    public string imagePath = null;
    public bool isChecked = false;
    public double imageWidth = 16;
    public IEnumerable list = null;
    /// <summary>
    /// Only for Click, not Checked etc!
    /// </summary>
    public RoutedEventHandler OnClick = null;
    public MouseButtonEventHandler MouseDown = null;
    public object tag = null;
    public string tooltip = null;
    public bool isEditable = false;
    public TextChangedEventHandler OnTextChange;

    public string text
    {
        get
        {
            if (content != null)
            {
                return content.ToString();
            }
            return string.Empty;
        }
        set
        {
            content = value;
        }
    }
    
    #endregion
}