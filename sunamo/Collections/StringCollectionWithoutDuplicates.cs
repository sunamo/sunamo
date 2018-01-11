using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

   public class StringCollectionWithoutDuplicates : CollectionWithoutDuplicates<string>
{
    public void TrimAll()
    {
        c = CA.TrimList(c);
    }
}
