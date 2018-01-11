public static class TableRowFileExts3
{
    /// <summary>
    /// Do A1 se vkládá bez tečky - přesně tak jak se ukládá do DB
    /// </summary>
    /// <returns></returns>
    public static short GetIdOrInsert(string ext)
    {
        short idExt = GeneralCells.IDOfFileExts_Ext(ext);
        if (idExt == -1)
        {
            TableRowFileExts fileExt = new TableRowFileExts(ext);
            idExt = fileExt.InsertToTable();
        }
        return idExt;
    }
}
