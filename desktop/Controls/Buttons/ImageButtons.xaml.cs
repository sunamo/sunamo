﻿using desktop.AwesomeFont;
using sunamo.Essential;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace desktop.Controls.Buttons
{
    /// <summary>
    /// Contains all buttons which show only AwesomeIcon
    /// </summary>
    public partial class ImageButtons : UserControl
    {
        static Type type = typeof(ImageButtons);
        List<Button> allButtons = null;
        public event VoidString Added;
        EnterOneValueWindow eov = null;
        public event VoidVoid CopyToClipboard;
        public event VoidVoid ClearAll;
        public event VoidVoid SelectAll;
        public event VoidVoid UnselectAll;

        public const string awesomeFontPath = "/Fonts/FontAwesome.otf#FontAwesome";

        async void SetAwesomeIcons()
        {
            // In serie how is written in xaml
            await AwesomeFontControls.SetAwesomeFontSymbol(btnCopyToClipboard, "\uf0c5");
            await AwesomeFontControls.SetAwesomeFontSymbol(btnClear, "\uf00d");
            await AwesomeFontControls.SetAwesomeFontSymbol(btnAdd, "\uf055");
            await AwesomeFontControls.SetAwesomeFontSymbol(btnSelectAll, "\uf05d");
            await AwesomeFontControls.SetAwesomeFontSymbol(btnUnselectAll, "\uf10c");
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void BtnCopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            CopyToClipboard();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            eov = new EnterOneValueWindow("item to insert (one on line)");
            eov.enterOneValueUC.ChangeDialogResult += EnterOneValueUC_ChangeDialogResult;
            eov.IsMultiline = true;
            eov.ShowDialog();
        }

        private void EnterOneValueUC_ChangeDialogResult(bool? b)
        {
            if (b.HasValue && b.Value)
            {
                data = eov.enterOneValueUC.txtEnteredText.Text;
                Handler(btnAdd, null);
            }
        }

        /// <summary>
        /// A1 can be VoidVoid or bool(Visibility). 
        /// </summary>
        /// <param name="copyToClipboard"></param>
        /// <param name="clear"></param>
        public ImageButtons()
        {
            InitializeComponent();

            SetAwesomeIcons();

            
        }

        public double HeightOfFirstVisibleButton()
        {
            foreach (var item in allButtons)
            {
                if (item.ActualHeight != 0)
                {
                    return item.ActualHeight;
                }
            }

            return 0;
        }

        /// <summary>
        /// 1. handlers RoutedEventHandler,VoidString directly to A1. or object - then will be use default action
        /// 2. bool and set handlers, elements has public FieldModifier
        /// </summary>
        /// <param name="copyToClipboard"></param>
        /// <param name="clear"></param>
        public void Init(ImageButtonsInit i)
        {
            SetVisibility(btnCopyToClipboard, i.copyToClipboard);
            SetVisibility(btnClear, i.clear);
            SetVisibility(btnAdd, i.add);
            SetVisibility(btnSelectAll, i.selectAll);
            SetVisibility(btnUnselectAll, i.deselectAll);

            allButtons = CA.ToList<Button>(btnCopyToClipboard, btnClear, btnAdd, btnSelectAll, btnUnselectAll);
            this.Visibility = this.IsAllCollapsed() ? Visibility.Collapsed : Visibility.Visible;

            foreach (var item in allButtons)
            {
                item.HorizontalAlignment = HorizontalAlignment.Center;
                item.VerticalAlignment = VerticalAlignment.Center;
            }
        }

        public bool IsAllCollapsed()
        {
            foreach (var item in allButtons)
            {
                if (item.Visibility != Visibility.Collapsed)
                {
                    return false;
                }
            }
            return true;
        }

        object data;
        

        private void SetVisibility(Button btn, object copyToClipboard)
        {
            string methodName = "SetVisibility";

            if (copyToClipboard == null)
            {
                btn.Visibility = Visibility.Collapsed;
            }
            else
            {
                btn.Visibility = Visibility.Visible;
                var t = copyToClipboard.GetType();
                if (t == typeof(bool))
                {
                    UIElementHelper.SetVisibility((bool)copyToClipboard, btn);
                }
                else if (t == TypesDesktop.tRoutedEventHandler)
                {
                    var d = (RoutedEventHandler)copyToClipboard;
                    btn.Click += d;
                }
                else if (t == Types.tVoidString)
                {
                    var voidString = (VoidString)copyToClipboard;
                    btn.Tag = voidString;
                    // If is not RoutedEventHandler, button have own handler which will call Handler()
                    //btn.Click += Handler;
                }
                else
                {
                    ThrowExceptions.NotImplementedCase(type, methodName, t);
                }
            }
        }

        void Handler(object o, RoutedEventArgs e)
        {
            string methodName = RLData.en["Handler"];

            Button btn = (Button)o;
            var t = btn.Tag.GetType();
            if (t == Types.tVoidString)
            {
                var voidString = (VoidString)btn.Tag;
                voidString.Invoke(data.ToString());
            }
            else
            {
                ThrowExceptions.NotImplementedCase(type, methodName, t);
            }
        }

        private void BtnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            SelectAll();
        }

        private void BtnUnselectAll_Click(object sender, RoutedEventArgs e)
        {
            UnselectAll();
        }
    }
}
