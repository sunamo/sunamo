using System;
using System.Collections.Generic;
using System.Text;


public partial class CryptHelper : ICryptHelper
{
    public class Rijndael : ICryptString
    {
        public RijndaelBytes rijndaelBytes = new RijndaelBytes();

        public string Decrypt(string v)
        {
            return BTS.ConvertFromBytesToUtf8(rijndaelBytes.Decrypt(BTS.ConvertFromUtf8ToBytes(v)));
        }

        public string Encrypt(string v)
        {
            return BTS.ConvertFromBytesToUtf8(rijndaelBytes.Encrypt(BTS.ConvertFromUtf8ToBytes(v)));
        }
    }
}