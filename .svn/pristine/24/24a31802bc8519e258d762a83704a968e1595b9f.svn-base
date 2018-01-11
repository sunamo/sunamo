using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace sunamo
{
    /// <summary>
    /// Class for generating and hashing passwords
    /// </summary>
    public class Password
    {
        private string _password;
        private int _salt;

        public Password(string strPassword, int nSalt)
        {
            _password = strPassword;
            _salt = nSalt;
        }

        public static string CreateRandomPassword(int PasswordLength)
        {
            String _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ23456789";
            Byte[] randomBytes = new Byte[PasswordLength];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)randomBytes[i] % allowedCharCount];
            }

            return new string(chars);
        }

        public static string CreateRandomStrongPassword()
        {
            int countCharsLower = 3;
            int countCharsUpper = 3;
            int countCharsNumbers = 3;
            int countCharsSpecial = 1;
            int PasswordLength = countCharsLower + countCharsUpper + countCharsNumbers + countCharsSpecial;
            List<char> allowedCharsLower = AllChars.lowerChars;
            List<char> allowedCharsUpper = AllChars.upperChars;
            List<char> allowedCharsNumbers = AllChars.numericChars;
            List<char> allowedCharsSpecial = AllChars.specialChars;

            
            Byte[] randomBytes = new Byte[3];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            
            StringBuilder vr = new StringBuilder(PasswordLength);

            rng.GetBytes(randomBytes);
            for (int y = 0; y < countCharsLower; y++)
            {
                vr.Insert(RandomHelper.RandomInt(0, vr.Length - 1), allowedCharsLower[(int)randomBytes[y] % allowedCharsLower.Count]);
            }

            rng.GetBytes(randomBytes);
            for (int y = 0; y < countCharsUpper; y++)
            {
                vr.Insert(RandomHelper.RandomInt(0, vr.Length - 1), allowedCharsUpper[(int)randomBytes[y] % allowedCharsUpper.Count]);
            }

            rng.GetBytes(randomBytes);
            for (int y = 0; y < countCharsNumbers; y++)
            {
                vr.Insert(RandomHelper.RandomInt(0, vr.Length - 1), allowedCharsNumbers[(int)randomBytes[y] % allowedCharsNumbers.Count]);
            }

            rng.GetBytes(randomBytes);
            for (int y = 0; y < countCharsSpecial; y++)
            {
                vr.Insert(RandomHelper.RandomInt(0, vr.Length - 1), allowedCharsSpecial[(int)randomBytes[y] % allowedCharsSpecial.Count]);
            }


            return vr.ToString();
        }

        public static int CreateRandomSalt()
        {
            Byte[] _saltBytes = new Byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(_saltBytes);

            return ((((int)_saltBytes[0]) << 24) + (((int)_saltBytes[1]) << 16) +
                (((int)_saltBytes[2]) << 8) + ((int)_saltBytes[3]));
        }

        public string ComputeSaltedHash()
        {
            // Create Byte array of password string
            ASCIIEncoding encoder = new ASCIIEncoding();
            Byte[] _secretBytes = encoder.GetBytes(_password);

            // Create a new salt
            Byte[] _saltBytes = new Byte[4];
            _saltBytes[0] = (byte)(_salt >> 24);
            _saltBytes[1] = (byte)(_salt >> 16);
            _saltBytes[2] = (byte)(_salt >> 8);
            _saltBytes[3] = (byte)(_salt);

            // append the two arrays
            Byte[] toHash = new Byte[_secretBytes.Length + _saltBytes.Length];
            Array.Copy(_secretBytes, 0, toHash, 0, _secretBytes.Length);
            Array.Copy(_saltBytes, 0, toHash, _secretBytes.Length, _saltBytes.Length);
            
            SHA1 sha1 = SHA1.Create();
            Byte[] computedHash = sha1.ComputeHash(toHash);

            return encoder.GetString(computedHash);
        }
    }
}
