using System;
using System.Collections.Generic;
using System.Text;


public class AspxConsts
{
    
    public static readonly string startAspxComment = "<%--";
    public static readonly string endAspxComment = "--%>";
    public static readonly string startHtmlComment = "<!--";
    public static readonly string endHtmlComment = "-->";

    public static readonly List<string> all = CA.ToListString(startAspxComment, endAspxComment, startHtmlComment, endHtmlComment, AllStrings.gt, AllStrings.lt);
}