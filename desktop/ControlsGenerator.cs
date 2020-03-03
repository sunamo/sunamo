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
            sp.Children.Add(TextBlockHelper.Get(new ControlInitData { text = data.t }));
            if (addDescription)
            {
                sp.Children.Add(TextBlockHelper.Get(new ControlInitData { text = data.sizeS }));
            }
            chb.IsThreeState = false;
            chb.IsChecked = tick;
            chb.Content = sp;
            return chb;
        }

        /// <summary>
        /// For get from simple string use CheckBox.Get
        /// </summary>
        /// <param name="data"></param>
        /// <param name="addDescription"></param>
        /// <param name="tick"></param>
        public static CheckBox CheckBoxWithDescription(TWithSizeInString<string> data, bool addDescription, bool tick)
        {
            var s = TextBlockHelper.Get(new ControlInitData { text = data.sizeS });

            CheckBox chb = new CheckBox();
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            sp.Children.Add(TextBlockHelper.Get(new ControlInitData { text = data.t }));
            if (addDescription)
            {
                sp.Children.Add(s);
            }
            chb.IsThreeState = false;
            chb.IsChecked = tick;
            chb.Content = sp;
            return chb;
        }

       
    }
}