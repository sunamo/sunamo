using System.Collections.Generic;
public class SunamoSearchDataLong : List<WordLong>
{
    public string tableChar = null;
    public long idEntity = 0;



    public static bool HasWord(SunamoSearchDataLong ssd, string p)
    {
        foreach (WordLong item in ssd)
        {
            if (item .word == p)
            {
                return true;
            }
        }
        return false;
    }
}

public class SunamoSearchDataInt : List<WordInt>
{
    public string tableChar = null;
    public int idEntity = 0;



    public static bool HasWord(SunamoSearchDataInt ssd, string p)
    {
        foreach (WordInt item in ssd)
        {
            if (item.word == p)
            {
                return true;
            }
        }
        return false;
    }
}

public class WordLong
{
    public long id = 0;
    public string word = null;
}

public class WordInt
{
    public int id = 0;
    public string word = null;
}

public class WordsCountSearchTargetInt //: SunamoSearchTarget
{
    public static int CompareByWordsCount(WordsCountSearchTargetInt x, WordsCountSearchTargetInt y)
    {
        if (x.count > y.count) return -1;
        else if (y.count > x.count) return 1;
        else return 0;
    }

    #region MyRegion
    /// <summary>
    /// Oddělené mezerou
    /// </summary>
    public string words = null;
    public int count = 0; 
    #endregion

    #region SearchTarget
    public string tableChar = null;
    public int idEntity = 0;
    #endregion
}

public class WordsCountSearchTargetLong //: SunamoSearchTarget
{
    public static int CompareByWordsCount(WordsCountSearchTargetLong x, WordsCountSearchTargetLong y)
    {
        if (x.count > y.count) return -1;
        else if (y.count > x.count) return 1;
        else return 0;
    }

    #region MyRegion
    /// <summary>
    /// Oddělené mezerou
    /// </summary>
    public string words = null;
    public int count = 0;
    #endregion

    #region SearchTarget
    public string tableChar = null;
    public long idEntity = 0;
    #endregion
}

