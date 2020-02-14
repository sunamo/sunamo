using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PushSolutionsData
{
    public bool mergeAndFetch = false; 
    public bool addGitignore = false;
    public List<string> onlyThese = new List<string>();
    public void Set(bool mergeAndFetch, bool addGitignore = false)
    {
        this.mergeAndFetch = mergeAndFetch;
        this.addGitignore = addGitignore;
    }
}

