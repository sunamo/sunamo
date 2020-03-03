using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;

namespace sunamo
{
    public class KeyboardHelper
    {
        /// <summary>
        /// Not working in Release and NotifyIcon it's not the fault
        /// USE ONLY FOR ALT+F3 AND SO. 
        /// When I check for Alt+f3 and 
        /// Keyboard.Modifiers has always only first pressed key, cannot combine
        /// When I want handle keys without modifier, must use KeyWithNoneModifier
        /// </summary>
        /// <param name="e"></param>
        /// <param name="key"></param>
        /// <param name="modifier"></param>
        
        public static byte IsNumber(KeyEventArgs e)
        {
            string s = e.Key.ToString();
            if (SH.RemovePrefix(ref s, "NumPad"))
            {
                return byte.Parse(s);
            }
            return byte.MaxValue;
        }

        public static bool IsModifier(Key k)
        {
            if (k == Key.LeftShift || k == Key.RightShift)
            {
                if (Keyboard.IsKeyDown(Key.RightShift) || Keyboard.IsKeyDown(Key.LeftShift))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
