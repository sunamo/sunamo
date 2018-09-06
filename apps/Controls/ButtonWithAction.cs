using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace apps
{
    /// <summary>
    /// Pokud nebude specifikována akce, vytvoří pouze TextBlock, pokud bude, vytvoří Button s daným handlerem a do něj TextBlock
    /// </summary>
    public  class ButtonWithAction : UserControl, IIdentificator
    {
        TaskObject action = null;
        FrameworkElement fe = null;
        //sunamo.Action action = sunamo.Action.Nope;
        public object Id
        {
            get; set;
        }
        public ButtonWithAction InitButtonWithAction(bool visible, double width, double height, TaskObject action, object content, object idObject)
        {
            if (visible)
            {
                this.Id = idObject;
                string c = "";
                if (content != null)
                {
                    c = content.ToString();
                }
                this.action = action;
                if (action == null)
                {
                    fe = new TextBlock();
                    TextBlock textBlock = fe as TextBlock;
                    textBlock.HorizontalAlignment = HorizontalAlignment.Stretch;
                    textBlock.VerticalAlignment = VerticalAlignment.Center;
                    textBlock.Text = c;
                }
                else
                {
                    fe = new Button();
                    Button button = fe as Button;
                    button.Margin = new Thickness(5, 0, 5, 0);
                    button.Click += Button_Click;
                    button.HorizontalContentAlignment = HorizontalAlignment.Center;
                    button.VerticalContentAlignment = VerticalAlignment.Center;
                    
                    TextBlock textBlock = new TextBlock();
                    textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    textBlock.VerticalAlignment = VerticalAlignment.Center;
                    textBlock.Text = c;
                    
                    button.Width = width;
                    button.Height = height;

                    button.Content = textBlock;
                    //SetButtonContent();
                }
                
                Content = fe;
            }
            return this;
        }


        protected void SetButtonContent(object content)
        {
            if (fe is Button)
            {
                //this.content = content;
                Button button = fe as Button;
                button.Content = content;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (action != null)
            {
                await action.Invoke(Id);
            }
        }
    }
}
