using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Can use global variable TextBoxState as is in TextBoxBackend
/// </summary>
public interface IShowSearchResults
{
    void SetTbSearchedResult(int actual, int count);
    void SetTextBoxState(string s = null);
}