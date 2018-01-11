using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
    public interface ILogMessageAbstract<T,  Z> : ILogMessage 
    {
        Task<LogMessageAbstract<T, Z>> Initialize(DateTime datum, TypeOfMessage st, string zprava, T color);
        T Bg { get; set; }

    }

    public interface ILogMessage
    {
        string Ts { get; set; }
    }
}
