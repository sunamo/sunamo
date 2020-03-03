using System;
using System.Collections.Generic;
using System.Text;

public class ListWithElements<T> : L<T>
{
    public ListWithElements(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Add(default(T));
        }
    }
}