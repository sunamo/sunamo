﻿
using System.Collections.Generic;
using System.Text;

public static class MetroUIHelper
{
    

    /// <summary>
    /// Získanou třídu pak stačí pouze přidat do atributu class html elementu, u kterého chci mít podbarvení
    /// </summary>
    /// <returns></returns>
    public static FgBgColors RandomColor()
    {
        string[][] colors = new string[][] {
        fgBlack, fgWhite, fgBlackAndWhite
    };
        string bw = RandomHelper.RandomInt2(0, 2) == 0 ? "fg-black" : "fg-white";
        FgBgColors vr = new FgBgColors();
        int number = RandomHelper.RandomInt2(0, colors.Length);
        if (number == 0)
        {
            vr.fg = "fg-black";
        }
        else if (number == 1)
        {
            vr.fg = "fg-white";
        }
        else
        {
            vr.fg = bw;
        }

        vr.bg = RandomHelper.RandomElementOfArray(colors[number]);
        return vr;
    }

    /// <summary>
    /// Barvy pozadí které by měli mít popředí bílé
    /// </summary>
    public static string[] fgWhite = new string[] {
        "bg-blackAllStrings.commabg-emerald", "bg-tealAllStrings.commabg-cobaltAllStrings.commabg-indigoAllStrings.commabg-violetAllStrings.commabg-crimsonAllStrings.commabg-brown",
"bg-olive",
"bg-steel",
"bg-mauve",
"bg-taupe",
"bg-gray",
"bg-dark",
"bg-darker",
"bg-darkBrown",
"bg-darkCrimson",
"bg-darkMagenta",
"bg-darkIndigo",
"bg-darkCyan",
"bg-darkCobalt",
"bg-darkTeal",
"bg-darkEmerald",
"bg-darkGreen",
"bg-darkOrange",
"bg-darkRed",
"bg-darkPink",
"bg-darkViolet",
"bg-darkBlue",
"bg-grayDark",
"bg-grayDarker"
    };

    /// <summary>
    /// Barvy popředí tmavé, které by měli popředí bílé
    /// </summary>
    public static string[] fgBlack = new string[] {
        //"bg-white",
        "bg-yellow",
        "bg-lightTeal",
        "bg-lightPink"
        //"bg-grayLighter"
    };

    public static string[] fgBlackAndWhite = new string[] {
        "bg-limeAllStrings.commabg-greenAllStrings.commabg-cyanAllStrings.commabg-pinkAllStrings.commabg-magentaAllStrings.commabg-redAllStrings.commabg-orangeAllStrings.commabg-amber",
"bg-lightBlue",
"bg-lightRed",
"bg-lightGreen",
"bg-lighterBlue",
"bg-lightOlive",
"bg-lightOrange",
"bg-grayLight"
    }; 
}