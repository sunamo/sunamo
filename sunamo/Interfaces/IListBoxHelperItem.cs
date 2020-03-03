using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Interfaces
{
    public interface IListBoxHelperItem
    {
        string RunOne { get; }
        /// <summary>
        /// For use in AllProjectsSearch - in LBH im working just with var and SolutionFolders are many. 
        /// Then I have to use very abstract ShortName or LongName
        /// </summary>
        string ShortName { get; }
        string LongName { get; }
    }
}