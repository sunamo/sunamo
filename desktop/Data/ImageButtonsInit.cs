using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ImageButtonsInit
{
    public object copyToClipboard = null; public object clear = null; public object add = null; public object selectAll = null; public object deselectAll = null;

    public ImageButtonsInit(object copyToClipboard, object clear, object add, object selectAll, object deselectAll)
    {
        this.copyToClipboard = copyToClipboard;
        this.clear = clear;
        this.add = add;
        this.selectAll = selectAll;
        this.deselectAll = deselectAll;
    }

    /// <summary>
    /// visible: add, selectAll, deselectAll
    /// </summary>
    //public static ImageButtonsInit DefaultButtons = new ImageButtonsInit(false, false, new VoidString(ColButtons_Added), true, true);
    public static ImageButtonsInit HideAllButtons = new ImageButtonsInit(false, false, false, false, false);
    public static ImageButtonsInit OnlySelect = new ImageButtonsInit(false, false, false, true, true);
}

