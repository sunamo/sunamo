using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace desktop
{
    public class SunamoVariableSizedWrapGrid2 : Panel
    {
        private Dictionary<int, Dictionary<int, UIElement>> controls = new Dictionary<int, Dictionary<int, UIElement>>();
        public SunamoVariableSizedWrapGrid2()
        {
            
        }

        protected override int VisualChildrenCount
        {
            get
            {
                int vr = 0;
                
                foreach (KeyValuePair<int, Dictionary<int, UIElement>> item in controls)
                {
                    vr += item.Value.Count;
                    
                }
                return vr;
            }
        }

        protected override System.Windows.Media.Visual GetVisualChild(int index)
        {
            if (index >= VisualChildrenCount)
            {
                throw new Exception();
            }

            int i = 0;
            foreach (KeyValuePair<int, Dictionary<int, UIElement>> item in controls)
            {
                foreach (var item2 in item.Value)
                {
                    if (i == index)
                    {
                        return item2.Value;
                    }
                    i++;
                }
            }
            throw new Exception();
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            double w = 0;
            double h = 0;
            double w2 = 0;
            double h2 = 0;
            for (int i = 0; i < InternalChildren.Count; i++)
            {
                
                UIElement item = InternalChildren[i];
                if (IsFirst(item))
                {
                    w2 = 0;
                    h2 = 0;
                }
                    item.Measure(availableSize);
                    w2 = item.DesiredSize.Width;
                    if (h2 < item.DesiredSize.Height)
                    {
                        h2 = item.DesiredSize.Height;    
                    }  
                //}
                    if (IsLast(item))
                    {
                        if (w < w2)
                        {
                            w = w2;
                        }
                        h += h2;
                    }
            }
            //return availableSize;
            return new Size(w, h);
            //return base.MeasureOverride(availableSize);
        }

        private bool IsLast(UIElement item3)
        {
            foreach (var item in controls)
            {
                UIElement i4 = null;
                foreach (var item2 in item.Value)
                {
                    i4 = item2.Value;
                }
                if (i4 == item3)
                {
                    return true;

                }
                break;
            }
            return false;
        }

        private bool IsFirst(UIElement item3)
        {
            foreach (var item in controls)
            {
                foreach (var item2 in item.Value)
                {
                    if (item2.Value == item3)
                    {
                        return true;
                        
                    }
                    break;
                }
            }
            return false;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double overallHeight = 0;
            double overallWidth = 0;
            //Size vr = new Size();
            double startHeight = 0;
            double startWidth = 0;
            for (int i = 0; i < InternalChildren.Count; i++)
            {
                var item = InternalChildren[i];
                if (IsFirst(item))
                {
                    startHeight = 0;
                    startWidth = 0;
                }
                    item.Arrange(new Rect(new Point(startWidth, overallHeight), item.DesiredSize));
                    item.InvalidateVisual();
                    if (item.RenderSize.Height > startHeight)
                    {
                        startHeight = item.RenderSize.Height;
                    }
                    startHeight += item.RenderSize.Height;
                    startWidth += item.RenderSize.Width;
                //}
                    if (IsLast(item))
                    {
                        if (startWidth > overallWidth)
                {
                    overallWidth = startWidth;
                }
                overallHeight += startHeight;
                    }    
            }

            return new Size(overallWidth, overallHeight);
            //return base.ArrangeOverride(finalSize);
        }

        void SunamoVariableSizedWrapGrid_SizeChanged(object sender, SizeChangedEventArgs sizeInfo)
        {
            //base.OnRenderSizeChanged(sizeInfo);
            bool start = false;
            if (controls.Count == 0)
            {
                Dictionary<int, UIElement> n = new Dictionary<int, UIElement>();
                controls.Add(0, n);
                StackPanel sp = new StackPanel();
                sp.SizeChanged += sp_SizeChanged;
                //sps.Add(0, sp);
                this.Children.Insert(0, sp);
                int i = 0;
                foreach (UIElement item in ((StackPanel)Children[0]).Children)
                {
                    n.Add(i, item);
                    RemoveLogicalChild(item);
                    RemoveVisualChild(item);

                    i++;
                }
                start = true;
            }

            if (start)
            {
                AfterGrowth(sizeInfo.NewSize.Width);
                AfterShrink(sizeInfo.NewSize.Width);
            }
        }

        void sp_SizeChanged(object sender, SizeChangedEventArgs sizeInfo)
        {
            if (sizeInfo.NewSize.Width > sizeInfo.PreviousSize.Width)
            {
                AfterGrowth(sizeInfo.NewSize.Width);
            }
            else
            {
                AfterShrink(sizeInfo.NewSize.Width);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        private void AfterGrowth(double p)
        {
            for (int i = 0; i < controls.Count; i++)
            {
                int odKterehoMusimOdebrat = -1;
                double widthOfUIElements = GetWidthOfUIElements(controls[i], p, out odKterehoMusimOdebrat);
                // Zde nevím zda to bude fungovat ta první podmínka
                if (!(odKterehoMusimOdebrat >= controls[i].Count) && odKterehoMusimOdebrat != -1)
                {
                    int dexDalsihoSP = i + 1;
                    if (IsStackPanelInSeries(dexDalsihoSP))
                    {
                        bool zastavit = false;
                        for (int z = dexDalsihoSP; z < controls.Count; z++)
                        {
                            for (int y = 0; y < controls[z + 1].Count; y++)
                            {
                                UIElement c = controls[dexDalsihoSP][y];
                                double d = GetWidthOfUIElement(c);
                                int odKterehoMusimOdebrat2 = -1;
                                if (d < p - GetWidthOfUIElements(controls[i], p, out odKterehoMusimOdebrat2))
                                {
                                    controls[dexDalsihoSP].Remove(y);
                                    controls[i].Add(controls[i].Count, c);
                                    GetStackPanelOnIndex(dexDalsihoSP).Children.RemoveAt(y);
                                    RemoveLogicalChild(c);
                                    RemoveVisualChild(c);
                                    GetStackPanelOnIndex(i).Children.Add(c);
                                }
                                // Na konec posuneme indexy všech zbývajících UIElementů
                                if (GetStackPanelOnIndex(dexDalsihoSP).Children.Count == 0)
                                {
                                    //sps.Remove(dexDalsihoSP);
                                    this.Children.RemoveAt(dexDalsihoSP);
                                    controls.Remove(dexDalsihoSP);
                                }
                                zastavit = true;
                                break;
                            }
                            if (zastavit)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        private bool IsStackPanelInSeries(int dexDalsihoSP)
        {
            return dexDalsihoSP < this.Children.Count;

        }

        

    

        

      



        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        private void AfterShrink(double p)
        {
            for (int i = 0; i < controls.Count; i++)
            {
                int odKterehoMusimOdebrat = -1;
                double widthOfUIElements = GetWidthOfUIElements(controls[i], p, out odKterehoMusimOdebrat);
                if (odKterehoMusimOdebrat != 0 && odKterehoMusimOdebrat != -1)
                {
                    List<UIElement> c = new List<UIElement>();
                    for (int y = controls.Count ; y >= odKterehoMusimOdebrat; y--)
                    {
                        c.Add(controls[i][y]);
                        controls[i].Remove(y);

                    }
                    int vkladatDo = i + 1;
                    if (!IsStackPanelInSeries(vkladatDo))
                    {
                        //sps.Add(vkladatDo, new StackPanel());
                        this.Children.Insert(vkladatDo, new StackPanel());
                        controls.Add(vkladatDo, new Dictionary<int, UIElement>());
                    }
                    int y2 = 0;
                    for (int y = odKterehoMusimOdebrat - 1; y >= 0;y--)
                    {
                        RemoveLogicalChild(c[y]);
                        RemoveVisualChild(c[y]);
                        //sps.Remove(y);
                        StackPanel v = GetStackPanelOnIndex(i);
                        v.Children.Remove(c[y]);
                        GetStackPanelOnIndex(vkladatDo).Children.Insert(y, c[y]);
                        if (!controls.ContainsKey(vkladatDo))
                        {
                            controls.Add(vkladatDo, new Dictionary<int, UIElement>());
                        }
                        y2 = (y * -1) + 1;
                        controls[vkladatDo].Add(y2, c[i]);
                    }
                    for (int y = 0; y < c.Count; y++)
                    {
                        
                    }
                    // A nyní musím všechny indexy posunout
                    y2 = Math.Abs(y2);
                    if (y2 != 1)
                    {
                        for (int y = controls[vkladatDo].Count - 1; y >= y2; y--)
                        {
                            controls[vkladatDo].Add(y + y2, controls[vkladatDo][y]);
                            controls[vkladatDo].Remove(y);
                        }   
                    }
                }
            }
        }

        private StackPanel GetStackPanelOnIndex(int i)
        {
            return (StackPanel)this.Children[i];
        }

        private double GetWidthOfUIElements(Dictionary<int, UIElement> dictionary, double maxWidth, out int odKterehoMusimOdebrat)
        {
            odKterehoMusimOdebrat = -1;
            double vr = 0;
            int nt = 0;
            foreach (var item in dictionary)
            {
                vr += GetWidthOfUIElement(item.Value);
                if (vr >= maxWidth)
                {
                    odKterehoMusimOdebrat = nt;
                }
                nt++;
            }
            return vr;
        }

        private double GetWidthOfUIElement(UIElement control)
        {
            double left = 0;
            if (control is FrameworkElement)
            {
                FrameworkElement control2 = control as FrameworkElement;
                 left = control2.Margin.Left;
            }
            if (control is Control)
            {
                Control control2 = control as Control;
                left +=  control2.Padding.Left;
            }
            double right = 0;
            if (control is FrameworkElement)
            {
                FrameworkElement control2 = control as FrameworkElement;
                right = control2.Margin.Left;
            }
            if (control is Control)
            {
                Control control2 = control as Control;
                right += control2.Padding.Right;
            }
            return left  + control.RenderSize.Width+ right;

        }
    }
}
