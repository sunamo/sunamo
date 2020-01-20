using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

public partial class NotificationWindow : Window
{

    PresentationSource presentationSource = null;
    static UIElement thenFocusTo = null;

    private NotificationWindow()
    {
        InitializeComponent();

        //ShowInTaskbar = false;
        Loaded += NotificationWindow_Loaded;
    }

    /// <summary>
    /// A2 = this
    /// </summary>
    /// <param name="content"></param>
    /// <param name="thenFocusTo2"></param>
    public static void Show(object content, UIElement thenFocusTo2)
    {
        thenFocusTo = thenFocusTo2;
        NotificationWindow window = new NotificationWindow();

        if (content is UIElement)
        {
            var ui = (UIElement)content;
            window.sp.Children.Clear();
            window.sp.Children.Add(ui);
        }
        else
        {
            window.tb.Text = content.ToString();
        }

        window.Show();
        //window.Focus();
        window.Activate();

        //Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
        //{
        //    if (PresentationSource.FromVisual(this) != null)
        //    {
        //NotificationWindow_Loaded(null, null);
        //    }
        //}));

        
    }



    private void NotificationWindow_Loaded(object sender, RoutedEventArgs e)
    {


        Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
        {
            var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;

            if (presentationSource == null)
            {
                presentationSource = PresentationSource.FromVisual(this);
            }
            if (presentationSource != null)
            {


                var transform = presentationSource.CompositionTarget.TransformFromDevice;
                var corner = transform.Transform(new Point(workingArea.Right, workingArea.Bottom));

                this.Left = corner.X - this.ActualWidth - 100;
                this.Top = corner.Y - this.ActualHeight;

                //Show();
                //Activate();
                //Nezobrazi se
                //Close();

                LimitedTimer timer = new LimitedTimer(5000, 1, () => Dispatcher.Invoke(() =>
                {
                    Close(); if (thenFocusTo.Focusable)
                    {
                        thenFocusTo.Focus();
                    }  } ));
                
            }
        }));

    }
}