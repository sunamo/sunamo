using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webforms.Interfaces
{
    public interface IPageResources
    {
        string Js(object cl);
        string Css(object cl);
    }
}
