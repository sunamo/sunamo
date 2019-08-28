public partial class LoggerBase{ 
public void TwoState(bool ret, params object[] toAppend)
    {
        WriteLine(ret.ToString() + AllStrings.comma + SH.Join(AllChars.comma, toAppend));
    }
}