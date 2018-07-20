using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace desktop.Helpers.Backend
{
    public class FoundedFileUCBackend : IKeysHandler<KeyEventArgs>
    {
        public List<string> OldRoots = null;

        public bool HandleKey(KeyEventArgs e)
        {
            throw new NotImplementedException();
        }


    }
}
