using System;
using System.Collections.Generic;
using System.Text;


    public class L<T> : List<T>
    {
        public int Length => Count;

    public L()
    {

    }

    public L(IEnumerable<T> collection) : base(collection)
    {

    }

    public L(int capacity) : base(capacity)
    {

    }

    public L<T> ToList()
    {
        return this;
    }
}

