using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Collections
{
    /// <summary>
    /// Může být:
    /// Každý sloupec řádku unikátní
    /// Každý řádek sloupce unikátní
    /// Všechny sloupce jako celek odlišné
    /// Všechny řádky jako celkem odlišné
    /// </summary>
    public class UniqueTableInWhole
    {
        string[,] rows = null;
        int actualRow = 0;
        int cells = 0;

        public UniqueTableInWhole(int c, int r)
        {
            cells = c;
            rows = new string[r, c];
        }

        /// <summary>
        /// Vrátí zda je každá hodnota v sloupci A1 unikátní
        /// Nekontroluje zda je index A1 správný, musí to dělat volající metoda
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public bool IsRowsInColumnUnique(int columnIndex)
        {
            return false;


        }

        private bool IsColumnUnique(int columnIndex, int rowsCount)
        {
            HashSet<string> hs = new HashSet<string>();
            for (int r = 0; r < rowsCount; r++)
            {
                hs.Add(rows[r, columnIndex]);
            }

            return hs.Count == rowsCount;
        }

        private bool IsRowUnique(int rowIndex, int columnsCount)
        {
            HashSet<string> hs = new HashSet<string>();
            for (int c = 0; c < columnsCount; c++)
            {
                hs.Add(rows[rowIndex, c]);
            }

            return hs.Count == columnsCount;
        }

        /// <summary>
        /// Pokud A1, musí být všechny sloupce jako celek zvlášť unikátní
        /// Pokud A2, musí být všechny řádky jako celek zvlášť unikátní
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="rows"></param>
        public bool IsUniqueAsRowsOrColumns(bool columns, bool rows)
        {
            if (!columns && !rows)
            {
                throw new Exception("Both column and row arguments in UniqueTableInWhole.IsUniqueAsRowOrColumn() was false.");
            }

            int rowsCount = this.rows.GetLength(0);
            int columnsCount = this.rows.GetLength(1);

            if (columns)
            {
                for (int r = 0; r < rowsCount; r++)
                {
                    if (!IsRowUnique(r, columnsCount))
                    {
                        return false;
                    }
                }
            }

            if (rows)
            {
                for (int c = 0; c < columnsCount; c++)
                {
                    if (!IsColumnUnique(c, rowsCount))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void AddCells(string[] c)
        {
            if (c.Length != cells)
            {
                throw new Exception("Different count input elements of array in UniqueTableInWhole.AddCells");
            }

            for (int i = 0; i < c.Length; i++)
            {
                rows[actualRow, i] = c[i];
            }

            actualRow++;
        }
    }
}
