using System;

public class XPathPart
{
    public string tag = null;
    public string attName = null;
    public string attValue = "";
    

    public XPathPart(string part)
    {
        int dexStartSquareBracket = part.IndexOf('[');
        int dexEndSquareBracket = part.IndexOf(']');
        if (dexStartSquareBracket != -1 && dexEndSquareBracket != -1)
        {
            tag = part.Substring(0, dexStartSquareBracket);
            string attr = SH.Substring(part, dexStartSquareBracket + 1, dexEndSquareBracket - 1);
            if (attr != "")
            {
                if (attr[0] == '@')
                {
                    string[] nameValue = SH.Split(attr.Substring(1), '"', '\'', '=');
                    if (nameValue.Length == 2)
                    {
                        if (nameValue[0] != "")
                        {
                            attName = nameValue[0];
                            attValue = nameValue[1];
                        }
                    }
                }
            }
        }
        else if (dexStartSquareBracket == -1 && dexEndSquareBracket == -1)
        {
            tag = part;
        }
        else if (dexStartSquareBracket == -1 || dexEndSquareBracket == -1)
        {
            throw new Exception("Neukončená závorka v metodě XPathPart.ctor");
        }
    }
}
