using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webforms
{
    public class MSStoredProceduresI
    {
        public List<String> SelectGetAllTablesInDBStartedWith(MySitesShort p)
        {
            return SelectGetAllTablesInDBStartedWith(p.ToString());
        }

        static MSStoredProceduresI _ci = null;
        public static MSStoredProceduresI ci

        {
            get
            {
                if (_ci == null)
                {
                    _ci = new MSStoredProceduresI();
                }
                return _ci;
            }
        }

        public List<string> SelectGetAllTablesInDB()
        {
            List<string> vr = new List<string>();
            DataTable dt = global::MSStoredProceduresI.ci.SelectDataTableSelective("INFORMATION_SCHEMA.TABLES", "TABLE_NAME", "TABLE_TYPE", "BASE TABLE");
            foreach (DataRow item in dt.Rows)
            {
                vr.Add(item.ItemArray[0].ToString());
            }
            return vr;
        }

        public List<String> SelectGetAllTablesInDBStartedWith(string p)
        {
            List<string> vr = new List<string>();
            if (p == "Nope")
            {
                var d = SelectGetAllTablesInDB();
                var e = new List<string>(Enum.GetNames(typeof(MySitesShort)));
                for (int i = 0; i < e.Count; i++)
                {
                    e[i] = e[i] + "_";
                }

                foreach (var item in d)
                {
                    bool jeWeby = false;
                    foreach (var item2 in e)
                    {
                        if (item.StartsWith(item2))
                        {
                            jeWeby = true;
                        }
                    }

                    if (!jeWeby)
                    {
                        vr.Add(item);
                    }
                }
            }
            else
            {
                p = p + "_";

                SqlCommand comm = new SqlCommand(string.Format("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = @p0 AND TABLE_NAME LIKE @p1 + '%'"));
                global::MSStoredProceduresI.AddCommandParameter(comm, 0, "BASE TABLE");
                global::MSStoredProceduresI.AddCommandParameter(comm, 1, p);
                DataTable dt = global::MSStoredProceduresI.ci.SelectDataTable(comm);
                foreach (DataRow item in dt.Rows)
                {
                    vr.Add(item.ItemArray[0].ToString());
                }

            }
            return vr;
        }
    }
}
