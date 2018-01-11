using System;
public static class TinyMCEHelper
{
    /// <summary>
    /// Volá se předtím než budeš chtít získat data z tinymce
    /// </summary>
    /// <param name="txtTinymce"></param>
    public static string Clean(string txtTinymce)
    {
        txtTinymce = txtTinymce.Trim();
        if (txtTinymce != "")
        {
            txtTinymce = SecurityHelper.TreatHtmlCode(txtTinymce);
            txtTinymce = txtTinymce.Replace("<p>&nbsp;</p>", "");
            txtTinymce = SH.JoinNL( SH.RemoveDuplicates(txtTinymce, Environment.NewLine));
            txtTinymce = txtTinymce.Trim();
            if (txtTinymce.EndsWith(Environment.NewLine))
            {
                txtTinymce = txtTinymce.Substring(0, txtTinymce.Length - Environment.NewLine.Length);
            }
        }
        return txtTinymce;
    }
}
