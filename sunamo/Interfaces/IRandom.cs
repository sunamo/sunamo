using System;
using System.Collections.Generic;
using System.Text;

public interface IRandom <T>
{
     T GetRandom();
    int LenghtOfPpk { get;}
}
