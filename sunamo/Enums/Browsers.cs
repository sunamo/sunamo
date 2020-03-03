/// <summary>
/// Jsou dělané přesně podle Request.Browser.Browser
/// </summary>
public enum Browsers : byte
{
    // Nic zde nikdy nesmíš měnit, můžeš maximálně přidávat nové prohlížeče
    Other = 0,
    Chrome = 1,
    Firefox = 2,
    InternetExplorer = 3,
    Opera = 4,
    Edge = 5,
    Vivaldi = 6,
    ChromeCanary = 7
}