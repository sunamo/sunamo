using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.IO;


    /// <summary>
    /// Ukazuje na jedinou hodnotu. 
    /// Nabizi M ktere automaticky spojuji a rozdeluji.
    /// Nabizi GS Metody. Sama o sob� nezapisuje nic do registru.
    /// </summary>
    public class RegistryEntry //: IUroven
    {
        #region DPP
        string _Hodnota;

        /// <summary>
        /// GS hodnotu polozky
        /// </summary>
        public string Value
        {
            get { return _Hodnota; }
            set
            {
                _Hodnota = value;
            }
        }
        string _Polozka;

        /// <summary>
        /// GS jaky ma Hodnota nazev.
        /// </summary>
        public string Item
        {
            get { return _Polozka; }
            set { _Polozka = value; }
        }

        string _Cesta;

        /// <summary>
        /// GS cestu k polozce
        /// </summary>
        public string PathToItem
        {
            get { return _Cesta; }
            set { _Cesta = value; }
        }

        string cestapolozka;

        /// <summary>
        /// GS spolecne cestu i jmeno polozky
        /// </summary>
        public string FullPath
        {
            get { return Path.Combine(PathToItem, Item); }
            set 
            { 
                cestapolozka = value;
                
            }
        }

        object objekt;
        string cesta;
        public static bool throwExceptionIfNotGettingValues = false;
        #endregion

        #region base
        /// <summary>
        /// EK, OOp.
        /// </summary>
        /// <param name="objekt"></param>
        /// <param name="cesta"></param>
        /// <param name="polozka"></param>
        public RegistryEntry(object objekt, string cesta, string polozka)
        {
            this.objekt = objekt;
            this.cesta = cesta;
            this.Item = polozka;
        }

        /// <summary>
        /// EK, OOP. objekt se vyplni sama - dle st. VP vyhazovatVyjimkyPriNeziskaniHodnoty je mozne vyhodit vyjimku, pokud se nezdari.
        /// </summary>
        /// <param name="cesta"></param>
        /// <param name="polozka"></param>
        public RegistryEntry(string cesta, string polozka)
        {
            this.Item = Item;
            this.cesta = cesta;
            objekt = Registry.GetValue(FullPath, polozka, null);
            if (objekt == null)
            {
                if (throwExceptionIfNotGettingValues)
                {
                    throw new Exception("Failed to get the item from the registry.");
                }
            }
        } 
        #endregion

        #region IUroven Members
        /// <summary>
        /// G token pro cestu v tomto registru.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="vstup"></param>
        /// <returns></returns>
        public string ReturnInLevel(int index, string vstup)
        {
            string[] tokeny = SH.Split(vstup, "\\");
            
            return tokeny[index];
        }
        #endregion
    }

