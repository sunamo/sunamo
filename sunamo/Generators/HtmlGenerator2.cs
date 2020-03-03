using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public partial class HtmlGenerator2 : HtmlGenerator
{
    public static string GenerateTreeWithCheckBoxes(NTree<string> tree)
    {
        HtmlGenerator hg = new HtmlGenerator();

        //hg.WriteTag(HtmlTags.ol);

        int inner = 0;
        AddTree(ref inner, hg, tree);



        //hg.TerminateTag(HtmlTags.ol);

        return hg.ToString();
    }

    private static void AddTree(ref int inner, HtmlGenerator hg, NTree<string> tree)
    {
        inner++;

        //hg.WriteTag(HtmlTags.li);
        hg.WriteTag(HtmlTags.ol);
        hg.WriteRaw(HtmlGenerator2.CheckBox(tree.data));

        foreach (var item in tree.children)
        {
            hg.WriteTag(HtmlTags.li);
            hg.WriteRaw(HtmlGenerator2.CheckBox(item.data));

            foreach (var item2 in item.children)
            {
                AddTree(ref inner, hg, item2);
            }

            hg.TerminateTag(HtmlTags.li);
        }


        hg.TerminateTag(HtmlTags.ol);
        //hg.TerminateTag(HtmlTags.li);
    }

    public static string CheckBox(string data)
    {
        if (!string.IsNullOrEmpty(data))
        {
            return "<input type=\"checkbox\" />" + data + "<br />";
        }
        return string.Empty;
    }
}