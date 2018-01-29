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
#if DEBUG
                DebugLogger.Instance.TwoState(result, key, modifier);
#endif
            }
            return result;
        }
    }
}
