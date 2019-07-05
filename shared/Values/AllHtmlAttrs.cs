﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

public class AllHtmlAttrs
{
    //

    public static List<string> list = null;

    public static void Initialize()
    {
        if (list == null)
        {

            list = new List<string>();

            foreach (var item in Enum.GetNames(typeof(HtmlTextWriterAttribute)))
            {
                list.Add(item.ToLower());
            }
        }
    }
}
