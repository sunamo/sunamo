/// <summary>
/// Datová třída, obsahující pouze český text a jeho odpovídající anglický překlad
/// </summary>
public class Translation
{
    public string En = null;
    public string Cs = null;

    public Translation(string en, string cs)
    {
        En = en;
        Cs = cs;
    }
}
