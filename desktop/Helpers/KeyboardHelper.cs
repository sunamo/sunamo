using sunamo.Essential;
using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;

namespace sunamo
{
    public class KeyboardHelper
    {
        /// <summary>
        /// USE ONLY FOR ALT+F3 AND SO. 
        /// When I check for Alt+f3 and 
        /// Keyboard.Modifiers has always only first pressed key, cannot combine
        /// When I want handle keys without modifier, must use KeyWithNoneModifier
        /// </summary>
        /// <param name="e"></param>
        /// <param name="key"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static bool KeyWithModifier(Key key, ModifierKeys modifier)
        {
            ModifierKeys modifierInt = (Keyboard.Modifiers & modifier);
            bool modifierPresent = modifierInt > 0;

            // Tady v Keyboard.IsKeyUp to prochází jakémkoliv textu


            bool result = Keyboard.IsKeyUp(key) && modifierPresent;
            // Here really must be without !
            return result;
        }

        /// <summary>
        /// Are there passed from PreviewKeyDown
        /// Working also with more modifiers specified
        /// Primary use method without KeyEventArgs param. When I try catch with this Alt+f3, for f3 returns System key.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="key"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static bool KeyWithModifier(KeyEventArgs e, Key key, ModifierKeys modifier)
        {
            /*
            stisknul jsem 1, mam 1, d1,u0
                Hned nasledne na to se zkontroluje i na 2 a 3
mam 2, d0, u1 - logicke
mam 3, d0, u1 - nechapu */

            bool keyPressed = Keyboard.IsKeyDown(key);
            bool modifierPressed = modifier == Keyboard.Modifiers;
            bool result = keyPressed && modifierPressed;

            if (result)
            {
#if DEBUG
                //DebugLogger.Instance.TwoState(result, key, modifier);
#endif
            }
            return result;
        }

        /// <summary>
        /// If was pressed ctrl+shift and want only ctrl, return also true!! Must be ==
        /// If was pressed A2 and A1, return true. Otherwise false
        /// </summary>
        /// <param name="keyPressed"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        private static bool KeyWithModifier(bool keyPressed, ModifierKeys modifier)
        {
            ModifierKeys modifierInt = (Keyboard.Modifiers & modifier);
            bool modifierPresent = modifierInt > 0;
            bool result = keyPressed && modifierPresent;
            
            return result;
        }

        public static bool IsNumLock()
        {
            return (((ushort)W32.GetKeyState(0x90)) & 0xffff) != 0;
        }

        public static bool IsCapsLock()
        {
            return (((ushort)W32.GetKeyState(0x14)) & 0xffff) != 0;
        }

        public static bool IsScrollLock()
        {
            return (((ushort)W32.GetKeyState(0x91)) & 0xffff) != 0;
        }

        public static bool KeyWithNoneModifier(KeyEventArgs e, Key key)
        {
            bool result = false;
            if (Keyboard.Modifiers == ModifierKeys.None)
            {
                result = key == e.Key;
            }
            return result;
        }

        public static int Number(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.NumPad0:
                    return 0;
                case Key.NumPad1:
                    return 1;
                case Key.NumPad2:
                    return 2;
                case Key.NumPad3:
                    return 3;
                case Key.NumPad4:
                    return 4;
                case Key.NumPad5:
                    return 5;
                case Key.NumPad6:
                    return 6;
                case Key.NumPad7:
                    return 7;
                case Key.NumPad8:
                    return 8;
                case Key.NumPad9:
                    return 9;
                case Key.D1:
                    return 1;
                case Key.D2:
                    return 2;
                case Key.D3:
                    return 3;
                case Key.D4:
                    return 4;
                case Key.D5:
                    return 5;
                case Key.D6:
                    return 6;
                case Key.D7:
                    return 7;
                case Key.D8:
                    return 8;
                case Key.D9:
                    return 9;
                case Key.D0:
                    return 0;
                default:
                    return -1;
            }
        }

        public static string DownKey(KeyEventArgs e)
        {
            string d = Keyboard.Modifiers.ToString() + ", " + e.Key.ToString();
            return d;
        }

        /// <summary>
        /// If dont be remain number, return 255
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static byte IsNumber(KeyEventArgs e)
        {
            string s = e.Key.ToString();
            if (SH.RemovePrefix(ref s, "NumPad"))
            {
                return byte.Parse(s);
            }
            return byte.MaxValue;
        }
    }
}
