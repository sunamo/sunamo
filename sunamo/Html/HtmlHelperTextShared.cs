public partial class HtmlHelperText{ 
public static string ReplacePairTag(string input, string tag, string forWhat)
    {
        input = input.Replace("<" + tag + ">", "<" + forWhat + ">");
        input = input.Replace("<" + tag  + " ", "<" + forWhat+ " ");
        input = input.Replace("</" + tag + ">", "</" + forWhat + ">");
        return input;
    }
}