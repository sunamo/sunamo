using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webforms.Interfaces
{
    /// <summary>
    /// 99% of pages need just Page_Load
    /// </summary>
    public interface IPageCs
    {
        
        void SunamoPageCs_Unload(object sender, EventArgs e);
        void SunamoPageCs_SaveStateComplete(object sender, EventArgs e);
        void SunamoPageCs_PreRender(object sender, EventArgs e);
        void SunamoPageCs_LoadComplete(object sender, EventArgs e);
        void SunamoPageCs_PreLoad(object sender, EventArgs e);
        void SunamoPageCs_InitComplete(object sender, EventArgs e);
        void SunamoPageCs_Init(object sender, EventArgs e);
        void SunamoPage_PreInit(object sender, EventArgs e);
    }
}
