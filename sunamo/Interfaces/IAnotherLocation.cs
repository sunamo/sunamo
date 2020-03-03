using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Interfaces
{
    public interface IAnotherLocation<T, U>
    {
        T Root { get; set; }
        U ReturnRightLocation(T id);
    }
}