﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class RuntimeHelper
{
    static Type type = typeof(RuntimeHelper);

    public static List<Delegate> GetInvocationList(Delegate e)
    {
        if (e == null)
        {
            return new List<Delegate>();
        }

        return e.GetInvocationList().ToList();
    }

    public static bool HasEventHandler(Delegate e)
    {
        return GetInvocationList(e).Count() > 0;
    }

    /// <summary>
    /// Default is true for automatically avoiding errors
    /// </summary>
    /// <param name="controlWithResult"></param>
    /// <param name="a"></param>
    /// <param name="throwException"></param>
    public static void AttachChangeDialogResult(IControlWithResultDebug controlWithResult, VoidBoolNullable a, bool throwException = true) 
    {
        var count = controlWithResult.CountOfHandlersChangeDialogResult();
        
        if (count > 0)
        {
            if (throwException)
            {
                ThrowExceptions.Custom(type, RH.CallingMethod(), "ChangeDialogResult has alredy registered handler");
            }
            else
            {
                // Do nothing
            }
        }
        else
        {
             controlWithResult.ChangeDialogResult +=  a;
        }
    }
}
