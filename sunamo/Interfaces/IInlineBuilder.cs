using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IInlineBuilder
{
    void Bold(string text);
    void Bullet(string p);
    void Error(string p);
    void H1(string text);
    void H1(string text, double maxWidth);
    void H2(string text);
    void H3(string text);
    void Hyperlink(string text, string uri);
    void Italic(string p);
    void KeyValue(string p1, string p2);
    void LineBreak();
    void Run(string p);
}