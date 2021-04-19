using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class KeysCatcher
{
    [Flags]
    public enum KeyStates
    {
        None = 0,
        Down = 1,
        Toggled = 2
    }

    /// <summary>
    /// Its keys, therefore must be System.Windows.Forms.Keys
    /// In comparsion with System.Windows.Input.Key have different values
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static KeyStates GetKeyState(Keys key)
    {
        

        KeyStates state = KeyStates.None;

        short retVal = W32.GetKeyState((int)key);

        //If the high-order bit is 1, the key is down
        //otherwise, it is up.
        if ((retVal & 0x8000) == 0x8000)
            state |= KeyStates.Down;

        //If the low-order bit is 1, the key is toggled.
        if ((retVal & 1) == 1)
            state |= KeyStates.Toggled;

        return state;
    }

    public static bool IsKeyDown(Keys key)
    {
        return KeyStates.Down == (GetKeyState(key) & KeyStates.Down);
    }

    public static bool IsKeyToggled(Keys key)
    {
        return KeyStates.Toggled == (GetKeyState(key) & KeyStates.Toggled);
    }
}