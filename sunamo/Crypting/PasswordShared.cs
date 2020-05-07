using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

public partial class Password
{
    
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
}