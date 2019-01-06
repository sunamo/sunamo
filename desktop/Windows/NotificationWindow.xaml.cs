using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

public partial class NotificationWindow : Window
{


    public static NotificationWindow Instance = new NotificationWindow();

    private NotificationWindow()
    {
        InitializeComponent();

        ShowInTaskbar = false;
        Loaded += NotificationWindow_Loaded;
    }

    public void Show(object content)
    {
        

        if (content is UIElement)
        {
            var ui = (UIElement)content;
            sp.Children.Clear();
            sp.Children.Add(ui);
        }
        else
        {
            tb.Text = content.ToString();
        }

        Show();

        


    }



    private void NotificationWindow_Loaded(object sender, RoutedEventArgs e)
    {


        Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
        {
            var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;



            var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
            var corner = transform.Transform(new Point(workingArea.Right, workingArea.Bottom));

            this.Left = corner.X - this.ActualWidth - 100;
            this.Top = corner.Y - this.ActualHeight;
        }));

    }
}