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
using System.Linq;
using static CryptHelper;

/// <summary>
/// K převodu z a na bajty BTS.ConvertFromBytesToUtf8 a BTS.ConvertFromUtf8ToBytes
/// 
/// Wrapper aroung CryptHelper2 class - 
/// Instead of CryptHelper2 use string
/// </summary>
public partial class CryptHelper : ICryptHelper
{
    

    


    

    /// <summary>
    /// DES use length of key 56 bit which make it vunverable to hard attacks
    /// Very slow, AES/Rijandel is too much better
    /// </summary>
    public class TripleDES : ICryptString
    {
        private List<byte> _s = null;
        private List<byte> _iv = null;
        private string _pp = null;

        public List<byte> s
        {
            set { _s = value; }
        }

        public List<byte> iv
        {
            set { _iv = value; }
        }

        public string pp
        {
            set { _pp = value; }
        }

        public string Decrypt(string v)
        {
            return BTS.ConvertFromBytesToUtf8(CryptHelper2.DecryptTripleDES(BTS.ConvertFromUtf8ToBytes(v), _pp, _s, _iv));
        }



        public string Encrypt(string v)
        {
            return BTS.ConvertFromBytesToUtf8(CryptHelper2.EncryptTripleDES(BTS.ConvertFromUtf8ToBytes(v), _pp, _s, _iv));
        }
    }

    /// <summary>
    /// Designed by Ronald R. Rivest in 1987 which designed another: RC4, RC5, RC6
    /// In 1996 was source code published, the same as in RC4
    /// then use is not recomended
    /// </summary>
    public class RC2 : ICrypt
    {
 

        public List<byte> s
        { set; get; }

        public List<byte> iv
        {
            set; get;
        }

        public string pp
        {
            set; get;
        }

        public string Decrypt(string v)
        {
            return BTS.ConvertFromBytesToUtf8(CryptHelper2.DecryptRC2(BTS.ConvertFromUtf8ToBytes(v), pp, s, iv));
        }

        public string Encrypt(string v)
        {
            return BTS.ConvertFromBytesToUtf8(CryptHelper2.EncryptRC2(BTS.ConvertFromUtf8ToBytes(v), pp, s, iv));
        }
    }

    /// <summary>
    /// Not working great
    /// Must convert to bytes and transfer in bytes, also through network
    /// </summary>
    public class RijndaelString : ICryptString
    {
        Rijndael rijndael = new Rijndael();

        public static RijndaelString Instance = new RijndaelString();

        public string Encrypt(string v)
        {
            return rijndael.Encrypt(v);
        }

        public string Decrypt(string v)
        {
            return rijndael.Decrypt(v);
        }
    }


    

    public List<byte> Decrypt(List<byte> v)
    {
        return _crypt.Decrypt(v);
    }

    public List<byte> Encrypt(List<byte> v)
    {
        return _crypt.Encrypt(v);
    }
}