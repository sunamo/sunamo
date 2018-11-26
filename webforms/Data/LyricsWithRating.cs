public class LyricsWithRating
{
    public int IDUser = -1; 
    public float Rating = -1;
    public bool IsTranslate = false;

    public LyricsWithRating(object[] o)
    {
        IDUser = MSTableRowParse.GetInt(o, 0);
        Rating = MSTableRowParse.GetFloat(o, 1);
        IsTranslate = MSTableRowParse.GetBool(o, 2);
    }
}
