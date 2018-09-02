
using System;
using System.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace apps
{
    
    public delegate void updateBorderBrushOfBorder(Border b, Brush br);
    public delegate Brush getBorderBrushOfBorder(Border b);
    public delegate void updateProgressBarWpf(ProgressBar pb, double value);
    public delegate void updateTextBlockText(TextBlock lbl, string text);
    public delegate void appendToTextBlock(TextBlock lbl, string text);

    public delegate void changeVisibilityUIElementWpf(UIElement uie, Visibility v);
    
    public delegate void appendToTextBox(TextBox lbl, string text);
    public delegate void insertToListBoxWpf(ListBox lb, int index, object o);
    public delegate void setDataContext(FrameworkElement fe, object o);
    public delegate object getDataContext(FrameworkElement fe);
    
    public delegate object getSelectedItemSelector(Selector cb);
    public delegate void setItemsSourceOfItemsControl(ItemsControl ic, IEnumerable o);
    public delegate void setCaretIndexOfTextBox(TextBox txt, int caretIndex);
    public delegate void focusTextBox(TextBox txt);
    public delegate string getTextOfTextBox(TextBox txt);
    
    public delegate object getItemAtIndexInSelector(Selector s, int dex);
    public delegate void setSelectedItemSelector(Selector s, object item);
    public delegate void updateLayoutOfUIElement(UIElement uie);
    //public delegate ListBoxItem getListBoxItemFromObject(ListBox lb, object )
    public partial class IH
    {
        public static changeVisibilityUIElementWpf delegateChangeVisibilityUIElementWpf = null;
        public static insertToListBoxWpf delegateInsertToListBoxWpf = null;
        
        public static updateBorderBrushOfBorder delegateUpdateBorderBrushOfBorder = null;
        public static getBorderBrushOfBorder delegateGetBorderBrushOfBorder = null;
        
        public static updateTextBlockText delegateUpdateTextBlockText = null;
        public static appendToTextBlock delegateAppendToTextBlock = null;
        
        public static appendToTextBox delegateAppendToTextBox = null;
        public static setDataContext delegateSetDataContext = null;
        public static getDataContext delegateGetDataContext = null;
        
        public static getSelectedItemSelector delegateGetSelectedItemSelector = null;
        public static setItemsSourceOfItemsControl delegateSetItemsSourceOfItemsControl = null;
        
        public static focusTextBox delegateFocusTextBox = null;
        public static getTextOfTextBox delegateGetTextOfTextBox = null;
        
        public static getItemAtIndexInSelector delegateGetItemAtIndexInSelector = null;
        public static setSelectedItemSelector delegateSetSelectedItemSelector = null;
        public static updateLayoutOfUIElement delegateUpdateLayoutOfUIElement = null;
        public static ListBoxItem getListBoxItemFromObject = null;
        //
        static IH()
        {
            delegateChangeVisibilityUIElementWpf = new changeVisibilityUIElementWpf(updateVisibility);
            delegateInsertToListBoxWpf = new insertToListBoxWpf(insertToListBoxWpfValue);
            
            delegateUpdateBorderBrushOfBorder = new updateBorderBrushOfBorder(updateBorderBrushOfBorderValue);
            delegateGetBorderBrushOfBorder = new getBorderBrushOfBorder(getBorderBrushOfBorderValue);
            
            delegateUpdateTextBlockText = new updateTextBlockText(updateTextBlockText);
            delegateAppendToTextBlock = new appendToTextBlock(appendToTextBlockText);
            
            delegateAppendToTextBox = new appendToTextBox(appendToTextBoxText);
            delegateSetDataContext = new setDataContext(setDataContextObject);
            delegateGetDataContext = new getDataContext(getDataContextObject);
            
            delegateGetSelectedItemSelector = new getSelectedItemSelector(getSelectedItemSelector);
            delegateSetItemsSourceOfItemsControl = new setItemsSourceOfItemsControl(setItemsSourceOfItemsControlM);
            
            delegateFocusTextBox = new focusTextBox(focusTextBox);
            delegateGetTextOfTextBox = new getTextOfTextBox(getTextOfTextBox);
            
            delegateGetItemAtIndexInSelector = new getItemAtIndexInSelector(getItemAtIndexInSelector);
            delegateSetSelectedItemSelector = new setSelectedItemSelector(setSelectedItemSelector);
            delegateUpdateLayoutOfUIElement = new updateLayoutOfUIElement(updateLayoutOfUIElement);

        }

        public static void updateLayoutOfUIElement(UIElement uie)
        {
            uie.UpdateLayout();
        }

        public static void setSelectedItemSelector(Selector s, object item)
        {
            s.SelectedItem = item;
        }

        public static object getItemAtIndexInSelector(Selector s, int dex)
        {
            return s.Items[dex];
        }

        

        public static string getTextOfTextBox(TextBox txt)
        {
            return txt.Text;
        }

        public static void focusTextBox(TextBox txt)
        {
            txt.Focus(FocusState.Programmatic);
        }

        

        

        public static void updateBorderBrushOfBorderValue(Border b, Brush br)
        {
            b.BorderBrush = br;
        }

        public static Brush getBorderBrushOfBorderValue(Border b)
        {
            return b.BorderBrush;
        }

        

        static void setItemsSourceOfItemsControlM(ItemsControl ic, IEnumerable o)
        {
            ic.ItemsSource = o;
        }

        
        public static void updateTextBlockText(TextBlock lbl, string text)
        {
            lbl.Text = text;
            UpdateTooltip(lbl, text);
        }

        public static void appendToTextBlockText(TextBlock lbl, string text)
        {
             text = lbl.Text + " " + text;
            lbl.Text = text;
            UpdateTooltip(lbl, text);
        }

        private static void UpdateTooltip(DependencyObject lbl, string text)
        {
            ToolTip t = new ToolTip();
            t.Content = text;
            ToolTipService.SetToolTip(lbl, text);
        }

        public static void appendToTextBoxText(TextBox tb, string text)
        {
            tb.Text = tb.Text + " " + text;
            UpdateTooltip(tb, tb.Text);
        }

        //
        public static void updateVisibility(UIElement ui, Visibility vis)
        {
            ui.Visibility = vis;
        }



        public static void insertToListBoxWpfValue(ListBox lb, int index, object o)
        {
            lb.Items.Insert(index, o);
        }

        public static void setDataContextObject(FrameworkElement fw, object dc)
        {
            fw.DataContext = dc;
        }

        public static object getDataContextObject(FrameworkElement fw)
        {
            return fw.DataContext;
        }

        

        public static object getSelectedItemSelector(Selector s)
        {
            return s.SelectedItem;
        }
    }
}
