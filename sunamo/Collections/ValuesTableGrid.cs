using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Collections
{
    public class ValuesTableGrid<T>
    {
        List<List<T>> data;

        public ValuesTableGrid(List<List<T>> data)
        {
            this.data = data;
        }

        public bool IsAllInRow(int row, T value)
        {
            foreach (var column in data[row])
            {
                
                    if (!EqualityComparer<T>.Default.Equals(column, value))
                    {
                        return false;
                    }
            }

            return true;
        }
    }
}
