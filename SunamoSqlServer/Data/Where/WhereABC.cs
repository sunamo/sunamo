using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class WhereABC
{
    public ABC whereIs;
    public ABC whereIsNot;
    public ABC greaterThan;
    public ABC lowerThan;

    public WhereABC(ABC whereIs, ABC whereIsNot, ABC greaterThan, ABC lowerThan)
    {
        this.whereIs = whereIs;
        this.whereIsNot = whereIsNot;
        this.greaterThan = greaterThan;
        this.lowerThan = lowerThan;
    }
}