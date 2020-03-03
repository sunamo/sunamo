using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CryptData : ICrypt
{
    public List<byte> s { get; set; }
    public List<byte> iv { get; set; }
    public string pp { get; set; }
}