using System.Drawing;

public class ImageWithPath
{
    public string path = "";
    public Image image = null;

    public ImageWithPath(string path)
    {
        this.path = path;
        image = Image.FromFile(path);
    }

    public ImageWithPath(string path, Image image)
    {
        this.path = path;
        this.image = image;
    }

    public override string ToString()
    {
        return path;
    }
}
