﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public partial class ImageHelper
    {
        /// <summary>
        /// Toto je jediné místo kde je tato proměnná a to proto že je na něho navázaná metoda SetAssemblyNameForWpfApps, která se musí volat ve WPF(ale ne Windows Store apps) aplikacích
        /// </summary>
        public static string protocol = "pack://application:,,,";
        public static string protocolRoot = "pack://application:,,,/";

        /// <summary>
        /// Nevolá se ve Windows Store Apps aplikacích
        /// </summary>
        /// <param name="assemblyName"></param>
        public static void SetAssemblyNameForWpfApps(string assemblyName)
        {
            protocol += AllStrings.slash + assemblyName + ";component/";
            //protocol = "pack://" + assemblyName + ";component/";
        }

        public static void RegisterPackProtocol()
        {
            if (!UriParser.IsKnownScheme("pack"))
                new System.Windows.Application();
        }
    }