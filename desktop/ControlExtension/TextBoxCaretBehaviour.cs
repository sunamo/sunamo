using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace desktop.ControlExtension
{
    /// <summary>
    /// In TextBox is needed AcceptsReturn="True" desktopControlExtension:TextBoxCaretBehaviour.ObserveCaret="True"
    /// </summary>
    public class TextBoxCaretBehaviour
    {
        public static readonly DependencyProperty ObserveCaretProperty =
        DependencyProperty.RegisterAttached
        (
            "ObserveCaret",
            typeof(bool),
            typeof(TextBoxCaretBehaviour),
            new UIPropertyMetadata(false, OnObserveCaretPropertyChanged)
        );

        public static bool GetObserveCaret(DependencyObject obj)
        {
            return (bool)obj.GetValue(ObserveCaretProperty);
        }

        public static void SetObserveCaret(DependencyObject obj, bool value)
        {
            obj.SetValue(ObserveCaretProperty, value);
        }

        private static void OnObserveCaretPropertyChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = dpo as TextBox;
            if (textBox != null)
            {
                if ((bool)e.NewValue == true)
                {
                    textBox.SelectionChanged += textBox_SelectionChanged;
                }
                else
                {
                    textBox.SelectionChanged -= textBox_SelectionChanged;
                }
            }
        }

        static void textBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int caretIndex = textBox.CaretIndex;
            SetCaretIndex(textBox, caretIndex);
            SetLineIndex(textBox, textBox.GetLineIndexFromCharacterIndex(caretIndex));
        }

        private static readonly DependencyProperty CaretIndexProperty =
            DependencyProperty.RegisterAttached("CaretIndex", typeof(int), typeof(TextBoxCaretBehaviour));

        public static void SetCaretIndex(DependencyObject element, int value)
        {
            element.SetValue(CaretIndexProperty, value);
        }

        public static int GetCaretIndex(DependencyObject element)
        {
            return (int)element.GetValue(CaretIndexProperty);
        }

        private static readonly DependencyProperty LineIndexProperty =
            DependencyProperty.RegisterAttached("LineIndex", typeof(int), typeof(TextBoxCaretBehaviour));

        public static void SetLineIndex(DependencyObject element, int value)
        {
            element.SetValue(LineIndexProperty, value);
        }

        public static int GetLineIndex(DependencyObject element)
        {
            return (int)element.GetValue(LineIndexProperty);
        }
    }
}
