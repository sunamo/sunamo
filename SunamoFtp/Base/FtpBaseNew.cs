using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoFtp
{
    public abstract class FtpBaseNew : FtpAbstract
    {
        public abstract void DebugAllEntries();
        public abstract void DebugDirChmod(string dir);
    }
}
