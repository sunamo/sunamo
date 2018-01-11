using System;
using System.Collections.Generic;
using System.Linq;



    public class TableRowStates3
    {
        public const string all = "Všechny";

        /// <summary>
        /// Pokud bude 0, vrátí text "Všechny"
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string GetStateNameAdvanced(byte p)
        {
            if (p == 0)
            {
                return all;
            }
            return TableRowState.GetStateName(p);
        }
    }
