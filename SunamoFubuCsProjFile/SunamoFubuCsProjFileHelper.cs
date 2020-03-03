 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FubuCsProjFile;

public class SunamoFubuCsProjFileHelper
{
    public static List<string> GetProjectsInSlnFile(string item, ref Solution sln)
    {
        sln = Solution.LoadFrom(item);
        
        var s = sln.Projects.Select(d => d.Project.FileName).ToList() ;
        CA.ChangeContent(s, FS.AbsoluteFromCombinePath);
        return s;
    }

    public static Guid GetProjectTypeFromSln(Solution sln, string v)
    {
        foreach (var item in sln.Projects)
        {
            if (item.ProjectName == v)
            {
                return item.ProjectType;
            }
        }
        return Guid.Empty;
    }

    public static Guid GetProjectIdFromSln(Solution sln, string v)
    {
        foreach (var item in sln.Projects)
        {
            if (item.ProjectName == v)
            {
                return item.ProjectGuid;
            }
        }
        return Guid.Empty;
    }

    public static void AddXmlns(string csproj, XNamespace ns, bool add)
    {
        if (add)
        {
            XDocument xml = XDocument.Load(csproj);
            foreach (var element in xml.Descendants().ToList())
            {
                element.Name = ns + element.Name.LocalName;
            }
            xml.Root.SetAttributeValue("xmlns", ns.ToString());

            xml.Save(csproj);
        }
        else
        {
            var text = TF.ReadFile(csproj);
            var xmlns = "xmlns=\"" + ns.ToString() + "\"";
            text = SH.ReplaceOnce(text, xmlns, string.Empty);
            TF.SaveFile(text, csproj);
        }
    }
}