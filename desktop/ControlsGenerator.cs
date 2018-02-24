using sunamo.Data;
using System.Windows.Controls;

namespace desktop
{
    public class ControlsGenerator
    {
        public static RadioButton RadioButtonWithDescription(TWithSizeInString<string> data, bool addDescription, bool tick)
        {
            RadioButton chb = new RadioButton();
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            sp.Children.Add(TextBlockHelper.Get(data.t));
            if (addDescription)
            {
                sp.Children.Add(TextBlockHelper.Get(data.sizeS));
            }
            chb.IsThreeState = false;
            chb.IsChecked = tick;
            chb.Content = sp;
            return chb;
        }

        public static CheckBox CheckBoxWithDescription(TWithSizeInString<string> data, bool addDescription, bool tick)
        {
            CheckBox chb = new CheckBox();
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            sp.Children.Add(TextBlockHelper.Get(data.t));
            if (addDescription)
            {
                sp.Children.Add(TextBlockHelper.Get(data.sizeS));
            }
            chb.IsThreeState = false;
            chb.IsChecked = tick;
            chb.Content = sp;
            return chb;
        }
    }
}
