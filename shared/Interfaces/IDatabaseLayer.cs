using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IDatabaseLayer<SqlDbType>
{
    Dictionary<SqlDbType, string> usedTa { get; set; }
    Dictionary<SqlDbType, string> hiddenTa { get; set; }

}

