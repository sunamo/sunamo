using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CmdBootStrapArgs
{
    #region Cant be null
    public string appName;
    public IClipboardHelper clipboardHelperWin;
    public Action runInDebug;
    public Func<Dictionary<string, VoidVoid>> AddGroupOfActions;
 public Dictionary<string, VoidVoid> allActions;
 public bool askUserIfRelease;
    #endregion

    public Action InitSqlMeasureTime;
}