using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// cant derive from IUserControl because implements it also MainWindows etc.
/// </summary>
public interface IUserControlWithSizeChange // : IUserControl
    {
    /// <summary>
    /// new DesktopSize( columnGrowing.ActualWidth, rowGrowing.ActualHeight)
    /// </summary>
    /// <param name="maxSize"></param>
    void OnSizeChanged(DesktopSize maxSize);
    }