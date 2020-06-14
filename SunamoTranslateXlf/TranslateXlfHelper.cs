using System;
using System.Collections.Generic;
using System.Text;

public class TranslateXlfHelper
{
    public static bool initializeTranslating = false;

    /// <summary>
    /// Into A1 can be passed 
    /// </summary>
    /// <param name="auth"></param>
    public static void InitializeTranslating(Action auth)
    {
        if (!initializeTranslating)
        {
            initializeTranslating = true;

            auth();
            TrimerTags.InitTagsWrapping();
            TranslateHelper.Instance.Init();
            XlfEngine.Instance.InitializeMultilingualResources();
            TextLang.Init();
        }
    }
}