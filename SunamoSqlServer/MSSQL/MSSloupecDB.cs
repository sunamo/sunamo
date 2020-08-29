using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MSSloupecDB : SloupecDBBase< MSSloupecDB, SqlDbType2>
{
    public MSSloupecDB() : base()
    {
        databaseLayer = MSDatabaseLayer.ci;
    }

    static MSSloupecDB()
    {
        SloupecDBBase< MSSloupecDB, SqlDbType2>.databaseLayer = MSDatabaseLayer.ci;
        // could set up here also factory column DB
    }
}