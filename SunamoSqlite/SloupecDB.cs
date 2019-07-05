using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;


public class SloupecDB : SloupecDBBase< SloupecDB, TypeAffinity>
{
    static SloupecDB()
    {
        SloupecDBBase<MSSloupecDB, TypeAffinity>.databaseLayer = DatabaseLayer.ci;
    }


}

