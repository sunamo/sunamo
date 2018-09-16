public class TableRowSRegions3
{
    public const string all = "Všechny";

    public static string GetRegionNameAdvanced(short idRegion)
    {
        if (idRegion == short.MaxValue)
        {
            return all;
        }
        return GeneralCells.NameOfRegion( idRegion);
    }
}
