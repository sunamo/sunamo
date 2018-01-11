/// <summary>
/// Používá se pro mnoho serverů pro ukládání do DB, proto hodnotu žádné z těchto výčtových hodnot nemůžeš měnit, protože by ti pak nefungovala práce s DB
/// </summary>
public enum MySites : byte
{
    //Ggdag = 0,
    BibleServer =1,
    Kocicky = 2,
    GeoCaching = 3,
    Apps = 4,
    Photos = 5,
    Lyrics = 6,
    Developer = 7,
    // Hlavně neměň tuto hodnotu, neposouvej ji vždy až na poslední místo, protože  pak by nefungovala práce s DB, ve které(třeba v tabulce Pages) se používá i hodnota None
    Nope = 8,
    //Build = 9,
    ThunderBrigade = 10,
    CasdMladez = 11,
    Shortener = 12,
    Shared = 13,
    Eurostrip = 14,
    Widgets = 15,
    WindowsMetroControls = 16,
    WindowsStoreApps = 17,
    MediaServis = 18,
    None = 255
}
