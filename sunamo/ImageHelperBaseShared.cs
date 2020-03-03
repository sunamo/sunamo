using System;
using System.Collections.Generic;
using System.Text;


public  abstract partial class ImageHelperBase<ImageSource, ImageControl>
{
    public abstract ImageControl ReturnImage(ImageSource bs);
    public abstract ImageControl ReturnImage(ImageSource bs, double width, double height);
}