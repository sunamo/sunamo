using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for HtmlInjector
/// </summary>
public class HtmlInjection
{
	public HtmlInjection()
	{
	}

    /// <summary>
    /// Toto se nepoužívá protože mi jej ASP.NET vždycky zaenkódují, například apostrof na &#39; nebo uvozovky na &quot;
    /// </summary>
    /// <param name="prehratNaYouTube"></param>
    /// <param name="script"></param>
    public static void AddOnClickParameter(HtmlButton prehratNaYouTube, string script)
    {
        prehratNaYouTube.Attributes.Add("onclick", script);
    }

}
