/// <summary>
/// Jakékoliv změny v této složce musíš projevit i v stejně pojmenované třídě v sunamo projektu
/// </summary>
//[Preserve]
public class ExternalLoginResult //: ExternalLoginResult
{
    public int IdUser
    {
        get;
        set;
    }

    public string Sc
    {
        get;
        set;
    }

    /// <summary>
    /// int.MaxValue for never asking
    /// </summary>
    public string P1
    {
        get;
        set;
    }
}

//public interface ExternalLoginResult
//{
//     int IdUser
//    {
//        get;
//        set;
//    }

//     string Sc
//    {
//        get;
//        set;
//    }
//}