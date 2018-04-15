using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class StoredProceduresI : MSStoredProceduresIBase
    {
        public static MSStoredProceduresIBase ci = null;

        public StoredProceduresI()
        {
            // TODO: Must use conn. string
            //ci = new MSStoredProceduresIBase();
        }
    }

