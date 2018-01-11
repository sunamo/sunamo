using System;

public static class CssGeneration
{
    public static string GetStyleTag(string content)
    {
        return "<style type='text/css'>" + content + "</style>";
    }

    public static string GetRoundedImage(int wh, string uri, int marginTopPx)
    {
        return "float: left; background-size: cover;display: block;width:"+wh+"px;height:"+wh+"px;background-image:url('"+uri+ "');border-radius:"+ wh / 2 +"px;margin-top:" + marginTopPx + "px";
    }

    public static string Responsive = "max-width: 100%;height: auto;";
}
