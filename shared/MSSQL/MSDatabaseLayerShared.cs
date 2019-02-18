using System.IO;
using System.Collections.Generic;
using System;
using System.Text;
using System.Data.SqlClient;
using System.Web;

public partial class MSDatabaseLayer{ 
public static void AssignConnectionStringScz(HttpApplication app)
    {
        
        AssignConnectionString("Data Source=.;Database=sunamo.cz;Integrated Security=True;MultipleActiveResultSets=True;");
    }
}