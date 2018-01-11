using System.Diagnostics;
using System.Text;
using System.IO;
using System;
using System.Security.Cryptography;
using System.Security;
using System.Text.RegularExpressions;

using System.Xml;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// K převodu z a na bajty BT.ConvertFromBytesToUtf8 a BT.ConvertFromUtf8ToBytes
/// </summary>
public class CryptHelper : ICryptHelper
{
    ICrypt crypt = null;

    public CryptHelper(Provider provider, byte[] s, byte[] iv, string pp)
    {
        switch (provider)
        {
            case Provider.DES:
                throw new NotSupportedException("Symetrické šifrování DES není podporováno.");
                break;
            case Provider.RC2:
                crypt = new CryptHelper.RC2();
                break;
            case Provider.Rijndael:
                crypt = new CryptHelper.Rijndael();
                break;
            case Provider.TripleDES:
                crypt = new CryptHelper.TripleDES();
                break;
            default:
                throw new NotImplementedException("");
                break;
        }
        crypt.iv = iv;
        crypt.pp = pp;
        crypt.s = s;
    }

    class TripleDES : ICrypt
    {
        byte[] _s = null;
        byte[] _iv = null;
        string _pp = null;

        public byte[] s
        {
            set { _s = value; }
        }

        public byte[] iv
        {
            set { _iv = value; }
        }

        public string pp
        {
            set { _pp = value; }
        }

        public string Decrypt(string v)
        {
            return Encoding.UTF8.GetString(CryptHelper2.DecryptTripleDES(Encoding.UTF8.GetBytes(v), _pp, _s, _iv));
        }

        public string Encrypt(string v)
        {
            return Encoding.UTF8.GetString(CryptHelper2.EncryptTripleDES(Encoding.UTF8.GetBytes(v), _pp, _s, _iv));
        }
    }

    class RC2 : ICrypt
    {
        byte[] _s = null;
        byte[] _iv = null;
        string _pp = null;

        public byte[] s
        {
            set { _s = value; }
        }

        public byte[] iv
        {
            set { _iv = value; }
        }

        public string pp
        {
            set { _pp = value; }
        }
        public string Decrypt(string v)
        {
            return Encoding.UTF8.GetString(CryptHelper2.DecryptRC2(Encoding.UTF8.GetBytes(v), _pp, _s, _iv));
        }

        public string Encrypt(string v)
        {
            
            return Encoding.UTF8.GetString(CryptHelper2.EncryptRC2(Encoding.UTF8.GetBytes(v), _pp, _s, _iv));
        }
    }

    class Rijndael : ICrypt
    {
        public Rijndael()
        {

        }

        public string Decrypt(string v)
        {
            return Encoding.UTF8.GetString(CryptHelper2.DecryptRijndael(Encoding.UTF8.GetBytes(v), _pp, _s, _iv));
        }

        public string Encrypt(string v)
        {
            return Encoding.UTF8.GetString(CryptHelper2.EncryptRijndael(Encoding.UTF8.GetBytes(v), _pp, _s, _iv));
        }

        byte[] _s = null;
        byte[] _iv = null;
        string _pp = null;

        public byte[] s
        {
            set { _s = value; }
        }

        public byte[] iv
        {
            set { _iv = value; }
        }

        public string pp
        {
            set { _pp = value; }
        }


    }

    public string Decrypt(string v)
    {
        return crypt.Decrypt(v);
    }

    public string Encrypt(string v)
    {
        return crypt.Encrypt(v);
    }
}





