using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace sunamo
{
    public class ComparerRowsByColumn : IComparer<List<string>>
    {
        static Type type = typeof(ComparerRowsByColumn);

        private int _indexSloupceKterySeradit;
        private char _znakOddelovaci;
        private int _maximalniDelkaSloupce;

        public ComparerRowsByColumn(int indexSloupceKterySeradit, char znakOddelovaci, int maximalniDelkaSloupce)
        {
            _indexSloupceKterySeradit = indexSloupceKterySeradit;
            _znakOddelovaci = znakOddelovaci;
            _maximalniDelkaSloupce = maximalniDelkaSloupce;
        }

        public int Compare(List<string> x, List<string> y)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(),type, Exc.CallingMethod());
            return int.MinValue;
        }
    }
}