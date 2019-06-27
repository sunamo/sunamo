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
    ICryptBytes crypt = null;

    public CryptHelper(Provider provider, List<byte> s, List<byte> iv, string pp)
    {
        switch (provider)
        {
            case Provider.DES:
                throw new NotSupportedException("Symetrické šifrování DES není podporováno" + ".");
            case Provider.RC2:
                //crypt = new CryptHelper.RC2();
                break;
            case Provider.Rijndael:
                crypt = new CryptHelper.RijndaelBytes();
                break;
            case Provider.TripleDES:
                //crypt = new CryptHelper.TripleDES();
                break;
            default:
                throw new NotImplementedException("");
        }
        crypt.iv = iv;
        crypt.pp = pp;
        crypt.s = s;
    }

    /// <summary>
    /// DES use length of key 56 bit which make it vunverable to hard attacks
    /// Very slow, AES/Rijandel is too much better
    /// </summary>
    public class TripleDES : ICryptString
    {
        List<byte> _s = null;
        List<byte> _iv = null;
        string _pp = null;

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
            return  BTS.ConvertFromBytesToUtf8(CryptHelper2.DecryptTripleDES(BTS.ConvertFromUtf8ToBytes(v), _pp, _s, _iv));
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
        List<byte> _s = null;
        List<byte> _iv = null;
        string _pp = null;

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
            return BTS.ConvertFromBytesToUtf8(CryptHelper2.DecryptRC2(BTS.ConvertFromUtf8ToBytes(v), _pp, _s, _iv));
        }

        public string Encrypt(string v)
        {
            return BTS.ConvertFromBytesToUtf8(CryptHelper2.EncryptRC2(BTS.ConvertFromUtf8ToBytes(v), _pp, _s, _iv));
        }
    }

    

    /// <summary>
    /// Used for common apps settings
    /// Fast
    /// Rijndael was code name, actually is calling as Advanced Encryption Standard(AES)
    /// was in 2001 approved by NIST, in 2002 was started to use as federal standard USA
    /// 
    /// </summary>
    public class RijndaelBytes : ICryptBytes, ICrypt
    {
        static RijndaelBytes()
        {
            Instance = new RijndaelBytes();

            Instance.iv = CryptData.ivRijn;
            Instance.pp = CryptData.pp;
            Instance.s = CryptData.s32;
        }

        public static RijndaelBytes Instance = null;
        
        List<byte> _s = null;
        List<byte> _iv = null;
        string _pp = null;

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

        public List<byte> Decrypt(List<byte> v)
        {
            return CryptHelper2.DecryptRijndael(v,_pp, _s, _iv);
        }

        public List<byte> Encrypt(List<byte> v)
        {
            return CryptHelper2.EncryptRijndael(v, _pp, _s, _iv);
        }
    }

    public List<byte> Decrypt(List<byte> v)
    {
        return crypt.Decrypt(v);
    }

    public List<byte> Encrypt(List<byte> v)
    {
        return crypt.Encrypt(v);
    }
}





