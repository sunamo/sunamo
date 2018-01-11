using sunamo;

public static class DrawingImagesHelper
{
    public static System.Drawing.Image MsAppx(bool enabled, AppPics appPic)
    {
        string cesta = "";
        if (enabled)
        {
            cesta = "i/d/";
        }
        else
        {
            cesta = "i/e/";
        }
        cesta += appPic.ToString() + ".png";
        return DrawingImageHelper.MsAppx(cesta);
    }
}
