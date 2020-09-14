using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ItemWithDt<T> : IItemWithDt<T>
{
    public T t { get; set; } = default(T);
    public DateTime Dt { get; set; }
}