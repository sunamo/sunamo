﻿public partial class TableRowHelper{ 
/// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static string BasicTest(string name, string value, int maxlenght)
    {
        if (value.Trim().Length == 0)
        {
            return "Do políčka " + name + " musíte zadat nějaký text";
        }

        if (value.Length > maxlenght)
        {
            return name + AllStrings.space + "musí být kratší než/rovno " + maxlenght + " znaků";
        }

        return "";
    }
}