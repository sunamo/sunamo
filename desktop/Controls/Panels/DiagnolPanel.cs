
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

/// <summary>
/// panel uspořádájící elementy do V. Image on https://www.dotnetforall.com/understanding-measureoverride-and-arrangeoverride/
/// </summary>
public class DiagnolPanel : Panel
{
    /// <summary>
    /// Calculate optimal size with Measure 
    /// </summary>
    /// <param name="availableSize"></param>
    
    private int GetTheMiddleChild(int count)
    {
        int middleChild;
        if (count % 2 == 0)
        {
            middleChild = count / 2;
        }
        else
        {
            middleChild = (count / 2) + 1;
        }

        return middleChild;
    }
}