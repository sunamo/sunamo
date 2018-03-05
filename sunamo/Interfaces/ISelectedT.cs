using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Interfaces
{
    public interface ISelectedT<T>
    {
        T Selected { get; }
    }
}
