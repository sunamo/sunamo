using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public interface IResourceHelper
{
    string GetString(string name);
    Stream GetStream(string name);
}