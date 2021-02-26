using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ImageButtonsInit
{
    public object copyToClipboard = null; public object clear = null; public object add = null; public object selectAll = null; public object deselectAll = null;

    /// <summary>
    /// If everything is null
    /// </summary>
    public ImageButtonsInit()
    {

    }

    /// <summary>
    /// all is object = can be true, false or new VoidString(ColButtons_Added) for example
    /// </summary>
    /// <param name="copyToClipboard"></param>
    /// <param name="clear"></param>
    /// <param name="add"></param>
    /// <param name="selectAll"></param>
    /// <param name="deselectAll"></param>
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

    /// <summary>
    /// A1 can be new VoidString(ColButtons_Added)
    /// </summary>
    /// <param name="vs"></param>
    /// <returns></returns>
    public static ImageButtonsInit ShowAllButtons(VoidString ColButtons_Added)
    {
        return new ImageButtonsInit(true, true, ColButtons_Added, true, true);
    }
}