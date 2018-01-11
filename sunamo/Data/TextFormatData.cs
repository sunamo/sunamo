using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class TextFormatData : List<CharFormatData>
    {
    /// <summary>
    /// Přesná požadovaná délka, nesmí být ani menší, ani větší
    /// Pokud je -1, text může mít jakoukoliv délku
    /// </summary>
    public int requiredLength = -1;
    public bool trimBefore = false;

    /// <summary>
    /// Zadej do A2 -1 pokud text může mít jakoukoliv délku
    /// </summary>
    /// <param name="trimBefore"></param>
    /// <param name="requiredLength"></param>
    /// <param name="a"></param>
        public TextFormatData(bool trimBefore, int requiredLength, params CharFormatData[] a)
        {
        this.trimBefore = trimBefore;
        AddRange(a);
        }
    }

