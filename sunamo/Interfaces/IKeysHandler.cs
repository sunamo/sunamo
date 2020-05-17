using System;
using System.Collections.Generic;
using System.Text;



    /// <summary>
    /// MainWindow_PreviewKeyDown
    /// </summary>
    /// <typeparam name="KeyArg"></typeparam>
public interface IKeysHandler<KeyArg>
    {
        /*
if (keysHandler != null)
            {
                if (keysHandler.HandleKey(e))
                {
                    e.Handled = true;
                }
                else
                {
                }
            }
         */

        bool HandleKey(KeyArg e);
    }