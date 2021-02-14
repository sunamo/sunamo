using Lastfm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SunamoLastFmConsts
{
    public static Session session = new Session("041184e8b93e471d3672ad0199616245", "d02dce4180df782bbf9a2ea5a4adbb05");

    public static void InitLastFm()
    {

        try
        {
            session.Authenticate("sunamoDevProg", HashHelper.GetMd5Hash("sunamoDevProgLfmPw"));
        }
        catch (Exception ex)
        {
        }
    }
}
