class WebPage2
{
    public byte IDWeb = 8;
    public string Name = null;

    public WebPage2(MySites IDWeb, string Name)
    {
        this.IDWeb = MySitesConverter.ConvertFrom(IDWeb.ToString().ToLower());
        this.Name = Name.ToLower().Replace(".aspx", "") + ".aspx";
    }
}
