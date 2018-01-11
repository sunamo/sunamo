
using sunamo;
using sunamo.Html;
using web;

public class PageSnippet
{
    public string _description = "";
    public string _image = "";
    public string _title = "";

    public string description
    {
        get
        {
            return _description;
        }
        set
        {
            _description = SH.ShortForLettersCountThreeDots(HtmlHelper.PrepareToAttribute(value), 300);
        }
    }
    public string image
    {
        get
        {
            return _image;
        }
        set
        {
            _image = HtmlHelper.PrepareToAttribute(value);
        }
    }
    public string title
    {
        get
        {
            return _title;
        }
        set
        {
            _title = HtmlHelper.PrepareToAttribute(value);
        }
    }
    public PageSnippet()
    {

    }

    public PageSnippet(string title, string description, string image)
    {
        this.title = title;
        this.description = description;
        this.image = image;
    }

    public PageSnippet(SunamoPage sp, string title, string description, MySites image)
    {
        this.title = title;
        this.description = description;
        this.image = web.UH.GetWebUri3(sp, "img/" + image.ToString() + "/WordCloud.jpg");
    }

}
