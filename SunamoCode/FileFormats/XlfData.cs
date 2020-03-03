using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

/// <summary>
/// Trans-units in *.xlf file and others
/// </summary>
public class XlfData
{
    public XElement group = null;
    public XDocument xd = null;
    public List<XElement> trans_units = null;
}