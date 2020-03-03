using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface ICryptHelper
{
    List<byte> Decrypt(List<byte> v);
    List<byte> Encrypt(List<byte> v);
}

public interface ICryptString
{
    string Decrypt(string v);
    string Encrypt(string v);
}

public interface ICryptBytes : ICrypt
{
    List<byte> Decrypt(List<byte> v);
    List<byte> Encrypt(List<byte> v);
}

public interface ICrypt
{
    List<byte> s { set; get; }
    List<byte> iv { set; get; }
    string pp { set; get; }
}