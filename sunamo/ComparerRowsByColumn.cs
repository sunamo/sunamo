using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace sunamo
{
    public class ComparerRowsByColumn : IComparer<List<string>>
    {
        private int indexSloupceKterySeradit;
        private char znakOddelovaci;
        private int maximalniDelkaSloupce;

        public ComparerRowsByColumn(int indexSloupceKterySeradit, char znakOddelovaci, int maximalniDelkaSloupce)
        {
            this.indexSloupceKterySeradit = indexSloupceKterySeradit;
            this.znakOddelovaci = znakOddelovaci;
            this.maximalniDelkaSloupce = maximalniDelkaSloupce;
        }

        public int Compare(List<string> x, List<string> y)
        {
            throw new NotImplementedException();
        }
    }
}
