using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


    public class InsertedRows
    {
        public int quantity = 0;
        public DataTable insertedRows = null;
        public string error = null;

        public InsertedRows()
        {

        }

        public InsertedRows(string Chyba)
        {
            this.error = Chyba;
        }
    }

