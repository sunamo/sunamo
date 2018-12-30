﻿using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmd.Essential
{
    public class CmdApp
    {
        /// <summary>
        /// Dont ask in console, load from Clipboard
        /// </summary>
        public static bool loadFromClipboard = false;

        public static void EnableDesktopLogging(bool v)
        {
            if (v)
            {
                // because method was called two times 
                ThisApp.StatusSetted -= ThisApp_StatusSetted;
                ThisApp.StatusSetted += ThisApp_StatusSetted;
            }
            else
            {
                ThisApp.StatusSetted -= ThisApp_StatusSetted;
            }


        }

        private static void ThisApp_StatusSetted(TypeOfMessage t, string message)
        {
            InitApp.TypedLogger.WriteLine(t, message);
        }
    }
}
