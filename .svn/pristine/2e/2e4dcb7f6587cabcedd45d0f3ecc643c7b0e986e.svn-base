using System.Text;
using System;
public class ConvertPascalConvention //: IConvertConvention
{
    /// <summary>
    /// NI
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string FromConvention(string p)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Převede na pascalskou konvenci, to znamená že tam budou pouze velké a malé písmena a 
    /// písmena za odebranými znaky budou velké.
    /// hello world = helloWorld
    /// Hello world = HelloWorld
    /// helloWorld = helloWorld
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string ToConvention(string p)
    {
        StringBuilder sb = new StringBuilder();
        bool dalsiVelke = false;
        foreach (char item in p)
        {
            if (dalsiVelke)
            {
                if (char.IsUpper(item))
                {
                    dalsiVelke = false;
                    sb.Append(item);
                    continue;
                }
                else if (char.IsLower(item))
                {
                    dalsiVelke = false;
                    sb.Append(char.ToUpper(item));
                    continue;
                }
                else
                {
                    continue;
                }
            }
            if (char.IsUpper(item))
            {
                sb.Append(item);
            }
            else if (char.IsLower(item))
            {
                sb.Append(item);
            }
            else
            {
                dalsiVelke = true;
            }
        }
        return sb.ToString();
    }
}

public class ConvertPascalConventionWithNumbers //: IConvertConvention
{
    /// <summary>
    /// NI
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string FromConvention(string p)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Převede na pascalskou konvenci, to znamená že tam budou pouze velké a malé písmena a 
    /// písmena za odebranými znaky budou velké.
    /// hello world = helloWorld
    /// Hello world = HelloWorld
    /// helloWorld = helloWorld
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string ToConvention(string p)
    {
        StringBuilder sb = new StringBuilder();
        bool dalsiVelke = false;
        foreach (char item in p)
        {
            if (dalsiVelke)
            {
                if (char.IsUpper(item))
                {
                    dalsiVelke = false;
                    sb.Append(item);
                    continue;
                }
                else if (char.IsLower(item))
                {
                    dalsiVelke = false;
                    sb.Append(char.ToUpper(item));
                    continue;
                }
                else if (char.IsDigit(item))
                {
                    dalsiVelke = true;
                    sb.Append(item);
                    continue;
                }
                else
                {
                    continue;
                }
            }
            if (char.IsUpper(item))
            {
                sb.Append(item);
            }
            else if (char.IsLower(item))
            {
                sb.Append(item);
            }
            else if (char.IsDigit(item))
            {
                sb.Append(item);
            }
            else
            {
                dalsiVelke = true;
            }
        }
        return sb.ToString();
    }
}
