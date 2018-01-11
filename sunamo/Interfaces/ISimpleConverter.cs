using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
    public interface ISimpleConverter<TypeInClassName, U>
    {
        TypeInClassName ConvertTo(U u);
        U ConvertFrom(TypeInClassName t);
    }

    public interface ISimpleConverter : ISimpleConverter<string, string>
    {
    }
}
