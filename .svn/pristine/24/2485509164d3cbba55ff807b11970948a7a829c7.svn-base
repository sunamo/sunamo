using System.Collections.Generic;
using System.Drawing;
public class DatabaseRows
{
    /// <summary>
    /// POZOR, čísluje se od 1 ..
    /// Ve klíči je ID pod kterým je barva v DB, v hodnotě pak ColorOnWeb který říká o hexadecimálním vyjádření a českém (mém) názvu barvy
    /// </summary>
    public static Dictionary<short, ColorOnWeb> colors = new Dictionary<short, ColorOnWeb>();

    

    public DatabaseRows()
    {

    }

    public static void Colors()
    {
        //MSStoredProceduresI.ci.DropAndCreateTable(Tables.Colors, GeneralLayer.s);
        colors.Clear();
        short i = 1;
        i = DictionaryHelper.AddToIndexAndReturnIncrementedShort<ColorOnWeb>(i, colors, new ColorOnWeb("#3FB1F0", "Bledě modrá"));
        i = DictionaryHelper.AddToIndexAndReturnIncrementedShort<ColorOnWeb>(i, colors, new ColorOnWeb("#D8306E", "Tmavě růžová"));
        i = DictionaryHelper.AddToIndexAndReturnIncrementedShort<ColorOnWeb>(i, colors, new ColorOnWeb("#47B62C", "Tmavě zelená"));
        //i = DictionaryHelper.AddToIndexAndReturnIncremented<ColorOnWeb>(i, colors, new ColorOnWeb("#F4C829", "Zlatá"));
        i = DictionaryHelper.AddToIndexAndReturnIncrementedShort<ColorOnWeb>(i, colors, new ColorOnWeb("#FE681B", "Oranžová"));
        i = DictionaryHelper.AddToIndexAndReturnIncrementedShort<ColorOnWeb>(i, colors, new ColorOnWeb("#EE312F", "Červená"));
        i = DictionaryHelper.AddToIndexAndReturnIncrementedShort<ColorOnWeb>(i, colors, new ColorOnWeb("#D9316F", "Růžová"));
        i = DictionaryHelper.AddToIndexAndReturnIncrementedShort<ColorOnWeb>(i, colors, new ColorOnWeb("#7220A9", "Fialová"));
        
    }
}
public class ColorOnWeb
{
    public string hex = "";
    public string czechName = "";
    public Color color = Color.Blue;

    public ColorOnWeb(string hex, string czechName)
    {
        this.hex = hex;
        this.czechName = czechName;
        this.color = StringHexColorConverter.ConvertFrom2(hex);
    }
}
