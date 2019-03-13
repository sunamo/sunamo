﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using desktop.Controls.Result;
using sunamo.Data;
using sunamo.Values;

namespace desktop.Helpers
{
    public class ControlHelper
    {
        public static readonly Size SizePositiveInfinity = new Size(double.PositiveInfinity, double.PositiveInfinity);

        public static Point GetOnCenter(Size parent, Size child)
        {
            Point vr = new Point();
            if (parent.Width > child.Width)
            {
                vr.X = ((parent.Width - child.Width) / 2d);
            }
            else if (parent.Width == child.Width)
            {
                vr.X = 0;
            }
            else
            {
                vr.X = 0;
            }
            if (parent.Height > child.Height)
            {
                vr.Y = (parent.Height - child.Height) / 2d;
            }
            else if (parent.Height == child.Height)
            {
                vr.Y = 0;
            }
            else
            {
                vr.Y = 0;
            }
            return vr;
        }

        public static void SwitchBorder(Control c, BorderData bd)
        {
            if (c != null)
            {
                var b = c.BorderThickness;
                if (!CA.IsAllTheSame<double>(Consts.zeroDouble, b.Bottom, b.Left, b.Right, b.Top))
                {
                    bd = BorderData.None;
                }
                c.BorderThickness = bd.BorderThickness;
                c.BorderBrush = bd.BorderBrush;
            }
        }
    }

}
