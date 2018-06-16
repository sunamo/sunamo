using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Interfaces
{
    public interface IAnotherLocation<T, ID>
    {
        T Root { get; set; }
        T ReturnRightLocation(ID id);
    }
}
