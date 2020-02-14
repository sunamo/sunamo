using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows;


public partial class RuntimeHelper{ 
public static bool IsAdminUser()
    {
        return FS.ExistsDirectory(@"d:\vs\sunamo\");
    }
}