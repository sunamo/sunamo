using System;
using System.Threading.Tasks;
using System.Windows.Media;

namespace sunamo
{
    public class LogMessage : LogMessageAbstract<Color, string>
    {


        public Task<LogMessageAbstract<Color, string>> Initialize(DateTime now, TypeOfMessage st, string status, Color color)
        {
            throw new NotImplementedException();
        }
    }
}
