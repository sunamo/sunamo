using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public interface IAsync
    {
    T GetResult<T>(Task<T> t);
    }