using System.Text;
/// <summary>
/// InstantSB(can specify own delimiter, check whether dont exists)
/// TextBuilder(implements Undo, save to Sb or List)
/// HtmlSB(Same as InstantSB, use br)
/// </summary>
public class HtmlSB : InstantSB
{
    public HtmlSB() : base("<br /")
    {
    }
}