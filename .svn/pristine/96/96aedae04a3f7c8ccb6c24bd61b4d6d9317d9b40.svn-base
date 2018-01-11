using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace desktop
{
    public enum TextImageRelation
    {
        ImageAboveText,
        ImageBeforeText
    }

    public class ToolStripButton : ToggleButton
    {
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(ToolStripButton));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ToolStripButton));
        //public static readonly DependencyProperty CheckedProperty = DependencyProperty.Register("Checked", typeof(bool), typeof(ToolStripButton));
        public static readonly DependencyProperty CheckOnClickProperty = DependencyProperty.Register("CheckOnClick", typeof(bool), typeof(ToolStripButton));
        //public static readonly DependencyProperty EnabledProperty = DependencyProperty.Register("Enabled", typeof(bool), typeof(ToolStripButton));
        public static readonly DependencyProperty TextImageRelationProperty = DependencyProperty.Register("TextImageRelation", typeof(TextImageRelation), typeof(ToolStripButton), new PropertyMetadata(TextImageRelation.ImageAboveText));
        public static readonly DependencyProperty FixedImageSizeProperty = DependencyProperty.Register("FixedImageSize", typeof(Size), typeof(ToolStripButton), new PropertyMetadata(null));
        StackPanel sp = new StackPanel();
        Image image = null;
        TextBlock tb = null;

        static ToolStripButton()
        {

        }

        protected override void OnClick()
        {
            base.OnClick();

            
        }

        protected override void OnChecked(RoutedEventArgs e)
        {
            base.OnChecked(e);

            if (CheckOnClick)
            {
                //IsChecked = !IsChecked;
            }
        }


        #region MyRegion
        #endregion

        public ToolStripButton(ImageSource image, string text)
        {
            Image = image;
            Text = text;
        }

        public ToolStripButton(ImageSource image)
        {
            Image = image;
        }

        public ToolStripButton(string text)
        {
            Text = text;
        }

        public ImageSource Image
        {
            get
            {
                return (ImageSource)GetValue(ImageProperty);
            }
            set
            {
                
                SetValue(ImageProperty, value);
                if (Image != null)
                {
                    image = new Image();
                    image.Source = value;
                }
                else
                {
                    image = null;
                }
                Invalidate();
            }
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
                if (Text != null)
                {
                    tb = new TextBlock();
                    tb.TextAlignment = TextAlignment.Center;
                    tb.Text = value;
                }
                else
                {
                    tb = null;
                }
                Invalidate();
            }
        }

        public bool CheckOnClick
        {
            get
            {
                return (bool)GetValue(CheckOnClickProperty);
            }
            set
            {
                SetValue(CheckOnClickProperty, value);
            }
        }

        public TextImageRelation TextImageRelation
        {
            get
            {
                return (TextImageRelation)GetValue(TextImageRelationProperty);
            }
            set
            {
                SetValue(TextImageRelationProperty, value);
                Invalidate();
            }
        }

        private void Invalidate()
        {
            switch (TextImageRelation)
            {
                case TextImageRelation.ImageAboveText:
                    sp.Children.Clear();
                    //sp.Height = 300;
                    sp.Orientation = Orientation.Vertical;
                    if (image != null)
                    {
                        image.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                        //image.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        image.Width = FixedImageSize.Width;
                        image.Height = FixedImageSize.Height;
                        sp.Children.Add(image);
                    }
                    if (tb != null)
                    {
                        tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                        //tb.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        sp.Children.Add(tb);
                    }
                    else
                    {

                    }
                    break;
                case TextImageRelation.ImageBeforeText:
                    break;
                default:
                    break;
            }
            sp.Margin = new Thickness(5);
            Content = sp;
            //base.InvalidateVisual();
        }

        public Size FixedImageSize
        {
            get
            {
                return (Size)GetValue(FixedImageSizeProperty);
            }
            set
            {
                SetValue(FixedImageSizeProperty, value);
                
                Invalidate();
            }
        }
    }
}
