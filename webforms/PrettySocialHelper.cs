using System;
using System.Collections.Generic;
using System.Linq;

using System.Web.UI;
using System.Web.UI.HtmlControls;


    public class PrettySocialHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Init( Page p, string title, string description, HtmlAnchor google, HtmlAnchor facebook, HtmlAnchor twitter)
        {
            SetToControl(p, title, description, google);
            SetToControl(p, title, description, facebook);
            SetToControl(p, title, description, twitter);
        }

        private static void SetToControl(Page p, string title, string description, HtmlAnchor google)
        {
            google.Attributes.Add("data-title", title);
            google.Attributes.Add("data-description", description);
            google.Attributes.Add("data-url",  p.Request.Url.ToString());
        }
    }

