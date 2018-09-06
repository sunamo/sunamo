using apps;
using sunamo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace apps
{
    /// <summary>
    /// Nepovolí tlačítko OK dokud není povolená aspoň 1 položka
    /// 
    /// </summary>
    public partial class CheckBoxList : UserControl, IPopupResponsive, IPopupEvents<object> //, IUserControlInPopup
    {
        public int checkedLength = 0;

        public event VoidT<object> ClickOK;
        public event VoidT<object> ClickCancel;

        public void ApplyColorTheme(ColorTheme ct)
        {
            ColorThemeHelper.ApplyColorTheme(border, ct);
        }

        private void OnClickOK(object sender, RoutedEventArgs e)
        {
            if (ClickOK != null)
            {
                ClickOK(null);
            }
        }

        private void OnClickCancel(object sender, RoutedEventArgs e)
        {
            if (ClickCancel != null)
            {
                ClickCancel(null);
            }
        }


        public Size MaxContentSize
        {
            get
            {
                return FrameworkElementHelper.GetMaxContentSize(this);
            }
            set
            {
                FrameworkElementHelper.SetMaxContentSize(this, value);
            }
        }


        public Brush PopupBorderBrush
        {
            set { border.BorderBrush = value; }
        }


        public Brush BackgroundBrush
        {
            set { border.Background = value; }
        }


        public Brush TitleForegroundBrush
        {
            set
            {
                //tbTitle.Foreground = value;
            }
        }

        public List<CheckBox> Checked()
        {
            List<CheckBox> chb = new List<CheckBox>(checkedLength);
            foreach (CheckBox item in spCheckBoxes.Children)
            {
                if (item.IsChecked.Value)
                {
                    chb.Add(item);
                }
            }
            return chb;
        }

        int pridano = 0;

        /// <summary>
        /// Při volání této metody je zároveň vytvořit slovník SunamoDictionaryWithKeysDependencyObject a do toho vkládat checkboxy a hodnoty k nim
        /// Nastaví chb.IsThreeState = false;
        /// </summary>
        /// <param name="chb"></param>
        public void AddCheckBox(CheckBox chb)
        {
            if (sunamo.BT.GetValueOfNullable( chb.IsChecked))
            {
                checkedLength++;
            }
            chb.IsThreeState = false;
            chb.Checked += Chb_Checked;
            chb.Unchecked += Chb_Unchecked;

            spCheckBoxes.Children.Add(chb);
            TurnOnOffButtonOk();
        }

        private void Chb_Unchecked(object sender, RoutedEventArgs e)
        {
            checkedLength--;
            TurnOnOffButtonOk();
        }

        private void Chb_Checked(object sender, RoutedEventArgs e)
        {
            checkedLength++;
            TurnOnOffButtonOk();
        }

        private void TurnOnOffButtonOk()
        {
            if (checkedLength == 0)
            {
                btnOk.IsEnabled = false;
            }
            else
            {
                btnOk.IsEnabled = true;
            }
        }

        public CheckBoxList()
        {
            InitializeComponent();
            SetContentToChbTickAll();
        }

        public CheckBoxList(params UIElement[] uie) : this()
        {
            

            foreach (var item in uie)
            {
                if (item != null)
                {
                    spCustomControls.Children.Add(item);
                }
            }
        }

        private void SetContentToChbTickAll()
        {
            if (chbTickAll.IsChecked.Value)
            {
                chbTickAll.Content = RL.GetString("UntickAll");
            }
            else
            {
                chbTickAll.Content = RL.GetString("TickAll");
            }
        }

        private void chbTickAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox item in spCheckBoxes.Children)
            {
                item.IsChecked = true;
            }
        }

        private void chbTickAll_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox item in spCheckBoxes.Children)
            {
                item.IsChecked = false;
            }
        }
    }
}
