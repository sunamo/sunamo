using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ProjectsNames
{
    public const string sunamoWeb = "sunamo.web";
    public const string sunamoWithoutDep = "sunamoWithoutDep";
    public const string sunamo = "sunamo";
    public const string CredentialsWithoutDep = "CredentialsWithoutDep";

    public static List<string> All = CA.ToList<string>(sunamoWeb, sunamoWithoutDep, sunamo, CredentialsWithoutDep);
}