using sunamo.Essential;
using System.Windows.Input;

namespace sunamo
{
    public class KeyboardHelper
    {
        public static bool KeyWithModifier(KeyEventArgs e, Key key, ModifierKeys modifier)
        {
            bool result = key == e.Key && (Keyboard.Modifiers & modifier) > 0;
            if (result)
            {
                DebugHelper.TwoState(result, key, modifier);
            }
            return result;
        }
    }
}
