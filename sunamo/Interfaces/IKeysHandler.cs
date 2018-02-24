using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Interfaces
{
    public interface IKeysHandler<KeyArg>
    {
        bool HandleKey(KeyArg e);
    }
}
