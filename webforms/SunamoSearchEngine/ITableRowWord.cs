public interface ITableRowWordLong : ITableRow<long>
{

     long iD
    {
        get;
        set;
    }
     string word
    {
        get;
        set;
    }
}
public interface ITableRowWordInt : ITableRow<int>
{

    int iD
    {
        get;
        set;
    }
    string word
    {
        get;
        set;
    }
}
