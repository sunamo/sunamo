using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo
{
    public abstract class ImageHelperBase<ImageSource, ImageControl>
    {
        public abstract ImageControl ReturnImage(ImageSource bs);
        public abstract ImageControl ReturnImage(ImageSource bs, double width, double height);
        public abstract ImageControl MsAppxI(string appPic2);
        public abstract ImageControl MsAppx(string relPath);
        public abstract ImageControl MsAppx(bool disabled, AppPics appPic);
    }
}
