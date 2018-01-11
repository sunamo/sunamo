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
    /// <summary>
    /// Jedná se o horizontální StackPanel který obsahuje další StackPanely, které nemají nastavenou Orientaci
    /// Hodí se pokud prvky mají fixní velikost
    /// Automaticky naskládá tolik controlů do jednoho SP aby se to na šířku vlezlo.
    /// Umí počítat s různými Controly, které mají samostatně definované vlatnosti Width
    /// </summary>
    public class SunamoVariableSizedWrapGrid : StackPanel
    {
        public SunamoVariableSizedWrapGrid()
        {
            this.Orientation = System.Windows.Controls.Orientation.Horizontal;
            this.SizeChanged += SunamoVariableSizedWrapGrid_SizeChanged;
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
                foreach (UIElement item in Children)
                {
                    n.Add(i, item);
                    RemoveLogicalChild(item);
                    RemoveVisualChild(item);

                    if (item != sp)
                    {
                        sp.Children.Add(item);
                    }
                    

                    i++;
                }
                start = true;
            }

            if (start)
            {
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

        private Dictionary<int, Dictionary<int, UIElement>> controls = new Dictionary<int, Dictionary<int, UIElement>>();
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
