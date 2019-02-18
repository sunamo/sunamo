using System.Collections.Generic;
using System.Diagnostics;
public class FromTo
{
    public FromTo()
    {

    }

    public FromTo(int from, int to)
    {
        this.from = from;
        this.to = to;
    }

    public int from = 0;
    public int to = 0;
}

public class FromToWord
{
    public int from = 0;
    public int to = 0;
    public string word = "";
}
