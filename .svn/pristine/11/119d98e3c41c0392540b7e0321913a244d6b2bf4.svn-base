using System;
using System.Collections.Generic;
using System.Text;


    /// <summary>
    /// Obsahuje cestyu, polozky a hodntoy vztahujici se k registru.
    /// Hodnoty je mozne pridavat postupne pomoci M nebo e priradit., protoze pole jsou verejne.
    /// Lze stanovit velikost pole a pri jejim prekroceni VV
    /// Pokud je nastaveno pres VP, nelze pridavat postupne.
    /// </summary>
    public class PolozkyRegistru //: ListViewItem
    {
        #region DPP
        public string[] Datas;
        public string[] Values;
        /// <summary>
        /// Jedna se sice o pole, ale je u vsech stejna
        /// </summary>
        public string[] Paths; 
        bool pridavatPostupne = false;
        /// <summary>
        /// Kolik polozek bylo naplneno.
        /// </summary>
        int i = 0;
        #endregion

        #region base 
        /// <summary>
        /// EK, OOP. Vytvori novei  [] o vel. A1.
        /// </summary>
        /// <param name="pocet"></param>
        /// <param name="pridavatPostupne"></param>
        public PolozkyRegistru(int pocet, bool pridavatPostupne)
        {
            Datas = new string[pocet];
            Values = new string[pocet];
            Paths = new string[pocet];
            this.pridavatPostupne = pridavatPostupne;
        }
        #endregion 

        #region -!Hotovo!-
        /// <summary>
        /// Prida novou hodnotu do this. VV Pri plnem naplneni
        /// </summary>
        /// <param name="Hodnota"></param>
        /// <param name="Polozka"></param>
        /// <param name="Cesta"></param>
        public void AddValue(string Hodnota, string Polozka, string Cesta)
        {
            if (!pridavatPostupne)
            {
                throw new Exception("Pokousite se upravit PP kdyz mate nastaveno hromadne");
            }
            if (i + 1 != Hodnota.Length)
            {
                this.Values[i] = Polozka;
                this.Datas[i] = Hodnota;
                this.Paths[i] = Cesta;
            }
            else
            {
                throw new Exception("Dostali jste pres index kolekce");
            }
        } 
        #endregion  
    }

