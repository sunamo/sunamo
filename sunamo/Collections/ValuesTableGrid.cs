using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Collections
{
    /// <summary>
    /// Allow make query to parallel collections as be one
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValuesTableGrid<T> : List<List<T>>
    {
        /// <summary>
        /// Row - wrapper - files 2
        /// Column - inner - apps 4
        /// </summary>
        private List<List<T>> exists;

        public ValuesTableGrid(List<List<T>> exists)
        {
            this.exists = exists;
        }

        public bool IsAllInColumn(int i, T value)
        {
            return CA.IsAllTheSame<T>(value, exists[i]);
        }

        public bool IsAllInRow(int i, T value)
        {
            foreach (var item in exists)
            {
                if (!EqualityComparer<T>.Default.Equals(item[i], value))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
