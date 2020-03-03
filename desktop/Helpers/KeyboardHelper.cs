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
        public static bool KeyWithModifier(Key key, ModifierKeys modifier)
        {
            return KeyWithModifier(null, key, modifier);
        }

        #region nonWhiteSpace
        static List<string> nonWhiteSpace = SH.GetLines(@"A
B
C
D
E
F
G
H
I
J
K
L
M
N
O
P
Q
R
S
T
U
V
W
X
Y
Z
NumPad0
NumPad1
NumPad2
NumPad3
NumPad4
NumPad5
NumPad6
NumPad7
NumPad8
NumPad9");
        #endregion
        #region whiteSpace
        static List<string> whiteSpace = SH.GetLines(@"None
Cancel
Back
Tab
LineFeed
Clear
Return
Enter
Pause
Capital
CapsLock
KanaMode
HangulMode
JunjaMode
FinalMode
HanjaMode
KanjiMode
Escape
ImeConvert
ImeNonConvert
ImeAccept
ImeModeChange
Space
Prior
PageUp
Next
PageDown
End
Home
Left
Up
Right
Down
Select
Print
Execute
Snapshot
PrintScreen
Insert
Delete
Help
D0
D1
D2
D3
D4
D5
D6
D7
D8
D9
LWin
RWin
Apps
Sleep
Multiply
Add
Separator
Subtract
Decimal
Divide
F1
F2
F3
F4
F5
F6
F7
F8
F9
F10
F11
F12
F13
F14
F15
F16
F17
F18
F19
F20
F21
F22
F23
F24
NumLock
Scroll
LeftShift
RightShift
LeftCtrl
RightCtrl
LeftAlt
RightAlt
BrowserBack
BrowserForward
BrowserRefresh
BrowserStop
BrowserSearch
BrowserFavorites
BrowserHome
VolumeMute
VolumeDown
VolumeUp
MediaNextTrack
MediaPreviousTrack
MediaStop
MediaPlayPause
LaunchMail
SelectMedia
LaunchApplication1
LaunchApplication2
Oem1
OemSemicolon
OemPlus
OemComma
OemMinus
OemPeriod
Oem2
OemQuestion
Oem3
OemTilde
AbntC1
AbntC2
Oem4
OemOpenBrackets
Oem5
OemPipe
Oem6
OemCloseBrackets
Oem7
OemQuotes
Oem8
Oem102
OemBackslash
ImeProcessed
System
OemAttn
DbeAlphanumeric
OemFinish
DbeKatakana
OemCopy
DbeHiragana
OemAuto
DbeSbcsChar
OemEnlw
DbeDbcsChar
OemBackTab
DbeRoman
Attn
DbeNoRoman
CrSel
DbeEnterWordRegisterMode
ExSel
DbeEnterImeConfigureMode
EraseEof
DbeFlushString
Play
DbeCodeInput
Zoom
DbeNoCodeInput
NoName
DbeDetermineString
Pa1
DbeEnterDialogConversionMode
OemClear
DeadCharProcessed");
        #endregion



        internal static bool IsWhitespace(KeyEventArgs e)
        {
            var k = e.Key.ToString();
            if (whiteSpace.Contains(k))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Not working in Release and NotifyIcon it's not the fault
        /// Are there passed from PreviewKeyDown
        /// Working also with more modifiers specified
        /// Primary use method without KeyEventArgs param. When I try catch with this Alt+f3, for f3 returns System key.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="key"></param>
        /// <param name="modifier"></param>
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
            return result;
        }

        /// <summary>
        /// If was pressed ctrl+shift and want only ctrl, return also true!! Must be ==
        /// If was pressed A2 and A1, return true. Otherwise false
        /// </summary>
        /// <param name="keyPressed"></param>
        /// <param name="modifier"></param>
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