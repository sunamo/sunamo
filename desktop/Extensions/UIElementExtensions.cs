using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace desktop2.Extensions
{
    public static class UIElementExtensions2
    {
        public static Action<UIElement, bool> SetValidatedDelegate;
        public static Func<UIElement, string, ValidateData, Tuple<bool?, ValidateData>> Validate2Delegate;
        public static Func<UIElement, object> GetContentDelegate;

        public static object GetContent(this UIElement ui)
        {
            return GetContentDelegate(ui);
        }

        public static bool? Validate2(this UIElement ui, string name, ref ValidateData d)
        {
            var r = Validate2Delegate(ui, name, d);
            d = r.Item2;
            return r.Item1;
        }

        public static void SetValidated(this UIElement ui, bool b)
        {
            SetValidatedDelegate(ui, b);
        }
    }

}