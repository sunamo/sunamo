using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using sunamo.Enums;
using apps;
using Windows.Graphics.Display;
using apps.Popups;

namespace apps
{
    public class PopupHelper
    {
        public static void AssignNewSizePopupCenter(Size windowSize, Popup popup)
        {
            //Size s = new Size(p.RenderSize.Width, p.RenderSize.Height);
            Point center = ControlHelper.GetOnCenter(windowSize, new Size(popup.Width, popup.Height));
            popup.HorizontalOffset = center.X;
            popup.VerticalOffset = center.Y;
        }

        /// <summary>
        /// A3  NSN
        /// 
        /// </summary>
        /// <param name="w"></param>
        /// <param name="child"></param>
        /// <param name="controlSize"></param>
        /// <param name="show"></param>
        /// <param name="borderBrush"></param>
        /// <returns></returns>
        public static Popup GetPopupWholeScreen( Control child, bool show, Brush borderBrush)
        {
            Size w = PageHelper.WindowSize(false);
            //ip.xName = ControlNameGenerator.GetSeries(child.GetType());
            Popup p = new Popup();
            p.Width = w.Width;
            p.Height = w.Height;
            p.Margin = new Thickness(0);
            p.MinHeight = p.Height;
            p.MinWidth = p.Width;
            p.Name = ControlNameGenerator.GetSeries(p.GetType());
            //child.Name = ControlNameGenerator.GetSeries(child.GetType());
            if (child != null)
            {
                IPopupWholeScreen ip = (IPopupWholeScreen)child;
                ip.PopupBorderBrush = borderBrush;

                Size s = new Size(w.Width - 4, w.Height - 4);
                child.MinWidth = s.Width;
                child.MinHeight = s.Height;
                child.Width = s.Width;
                child.Height = s.Height;
                p.Child = child;
            }
            if (show)
            {
                p.IsOpen = true;
            }
            return p;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="w"></param>
        /// <param name="child"></param>
        /// <param name="controlSize"></param>
        /// <param name="show"></param>
        /// <param name="borderBrush"></param>
        /// <returns></returns>
        public static Popup GetPopupResponsive(Control child, bool show, Brush borderBrush)
        {
            Popup p = new Popup();
            p.IsLightDismissEnabled = false;
            p.Margin = new Thickness(0);
            p.Name = ControlNameGenerator.GetSeries(p.GetType());
            //Size maxContentSize =  Size.Empty;
            Size s = PageHelper.WindowSize(false);
            Size sm4 = ControlHelper.GetMinimumHeightMinimumWidth(child, ControlHelper.SizePositiveInfinity);
            
            
            //sm4 = SizeH.Divide(sm4, 2);
            sm4 = SizeH.Plus(sm4, 50);
            if (sm4.Width > s.Width || sm4.Height > s.Height)
            {
                sm4 = s;
            }
            sm4 = SizeH.Minus(sm4, 4);
            child.MaxHeight = sm4.Height;
            child.MaxWidth = sm4.Width;
            child.HorizontalAlignment = HorizontalAlignment.Center;
            child.VerticalAlignment = VerticalAlignment.Center;
            child.Tag = (sm4.Width / DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel) - 50;
            
            if (child != null)
            {
                child.Name = ControlNameGenerator.GetSeries(child.GetType());
                p.Child = child;
                
               
                IPopupResponsive ips = (IPopupResponsive)child;
                ips.PopupBorderBrush = borderBrush;

                if (false) // UniversalInterop.IsLargeScreen())
                {
                    //Size sm = ControlHelper.GetMinimumHeightMinimumWidth(child, ControlHelper.SizePositiveInfinity);
                    Point center = ControlHelper.GetOnCenter(s, sm4);
                    p.HorizontalOffset = center.X;// / scaleFactor;
                    p.VerticalOffset = center.Y;// / scaleFactor;
                }
                else
                {
                    if (UniversalInterop.IsPortrait(true))
                    {
                        var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
                        Size ssss = s;
                        Size smo = ControlHelper.GetMinimumHeightMinimumWidth(child, ControlHelper.SizePositiveInfinity);
                        Size sm = smo.RecalculateSizeWithScaleFactor();
                        sm = SizeH.Minus(sm, 2);
                        Point center = ControlHelper.GetOnCenter(ssss, sm);
                        p.HorizontalOffset = center.X / scaleFactor;


                        double addY = 0;
                        if (!UniversalInterop.IsLargeScreen())
                        {
                            addY = 24;
                        }
                        //48 je již dvojnásobek
                        addY = addY / scaleFactor;
                        p.VerticalOffset = ((center.Y + addY) / scaleFactor) + 2;
                    }
                    else
                    {
                        var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
                        Size ssss = s;
                        Size sm = ControlHelper.GetMinimumHeightMinimumWidth(child, ControlHelper.SizePositiveInfinity).RecalculateSizeWithScaleFactor();
                        sm = SizeH.Minus(sm, 2);
                        Point center = ControlHelper.GetOnCenter(ssss, sm);
                        p. VerticalOffset = ((center.Y) / scaleFactor) - 4;


                        double addX = 0;
                        if (!UniversalInterop.IsLargeScreen())
                        {
                            addX = 88;
                        }
                        //48 je již dvojnásobek
                        addX = addX / scaleFactor;
                        p.HorizontalOffset = ((center.X + addX + 2) / scaleFactor);
                    }
                }
            }

            if (show)
            {
                p.IsOpen = true;
            }
            return p;
        }

        private static void P_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private static void P_LayoutUpdated(object sender, object e)
        {
            
        }


    }
}
