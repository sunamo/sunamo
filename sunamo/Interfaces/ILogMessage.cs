
using sunamo.LoggerAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
    public interface ILogMessage<Color, StorageClass>
    {
        Task<LogMessageAbstract<Color, StorageClass>> Initialize(DateTime datum, TypeOfMessage st, string zprava, Color color);
        Color Bg { get; set; }
        string Message { get;  }
    }
}
