public class TableRowSRDistricts3
{
    public const string all = "Všechny";

    

    public static string GetDistrictName(byte idState, short idRegion, byte idDistrict)
    {
            return MSStoredProceduresI.ci.SelectCellDataTableStringOneRow(Tables.Location, "District", CA.ToArrayT<AB>( AB.Get("IDState", idState), AB.Get("IDRegion", idRegion), AB.Get("SerieDistrict", idDistrict)), null);
    }

    public static string GetDistrictNameAdvanced(byte idState, short idRegion, byte idDistrict)
    {
        if (idDistrict == 0)
        {
            return all;
        }
        return GetDistrictName(idState, idRegion, idDistrict);
    } 
}
