using System;
using System.Collections.Generic;
using System.Text;


public class SunamoNTextCatConsts
{
    static string fileCzechWord = AppData.ci.GetFileCommonSettings("czechWords.txt");
    public static PpkOnDrive czechWords = new PpkOnDrive(fileCzechWord);
}

