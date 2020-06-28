using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
public class ErrorMessageGenerator
{
    private StringBuilder _vypis = new StringBuilder();
    private StringBuilder _triTecky = new StringBuilder();

    public string Visible
    {
        get
        {
            return _vypis.ToString();
        }
    }

    public string Collapse
    {
        get
        {
            return _triTecky.ToString();
        }
    }

    public ErrorMessageGenerator(List<string> chybneSoubory, List<FileExceptions> chyby, int i)
    {
        if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "cs")
        {
            _vypis.AppendLine("  t\u011Bchto souborech se vyskytly tyto chyby: ");
        }
        else
        {
            _vypis.AppendLine(sess.i18n(XlfKeys.InTheseFilesTheFollowingErrorsOccurred) + ": ");
        }

        if (chybneSoubory.Count < i)
        {
            i = chybneSoubory.Count;
        }
        int y = 0;
        for (; y < i; y++)
        {
            string em = GetErrorMessage(chyby[y]);
            _vypis.AppendLine(chybneSoubory[y] + AllStrings.swda + em);
        }

        string priChybe = null;
        if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "cs")
        {
            priChybe = "Pokud si mysl\u00EDte \u017Ee to je chyba aplikace, po\u0161lete pros\u00EDm mi email na adresu kter\u00E1 je uvedena v dialogu O aplikaci";
        }
        else
        {
            priChybe = sess.i18n(XlfKeys.IfYouThinkThatThisIsApplicationErrorPleaseSendMeAnEmailAtTheAddressThatIsListedInTheAboutApp);
        }

        if (y == chybneSoubory.Count)
        {
            _triTecky.AppendLine(priChybe);
        }
        else
        {
            for (; y < chybneSoubory.Count; y++)
            {
                string em = GetErrorMessage(chyby[y]);
                _triTecky.AppendLine(chybneSoubory[i] + AllStrings.swda + em);
            }
            _triTecky.AppendLine(priChybe);
        }
    }

    static Type type = typeof(ErrorMessageGenerator);

    private string GetErrorMessage(FileExceptions fileExceptions)
    {
        if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "cs")
        {
            switch (fileExceptions)
            {
                case FileExceptions.None:
                    // Žádná chyba
                    break;
                case FileExceptions.FileNotFound:
                    return sess.i18n(XlfKeys.FileNotFound);
                case FileExceptions.UnauthorizedAccess:
                    return "Program z\u0159ejm\u011B nem\u00E1 p\u0159\u00EDstup k souboru";
                case FileExceptions.General:
                    return "Nezn\u00E1m\u00E1 nebo obecn\u00E1 chyba";
                default:
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Neimplementovan\u00E1 v\u011Btev");
                    return null;
            }
        }
        else
        {
            switch (fileExceptions)
            {
                case FileExceptions.None:
                    // Žádná chyba
                    break;
                case FileExceptions.FileNotFound:
                    return sess.i18n(XlfKeys.FileNotFound);
                case FileExceptions.UnauthorizedAccess:
                    return sess.i18n(XlfKeys.TheProgramDoesNotHaveAccessToTheFile);
                case FileExceptions.General:
                    return sess.i18n(XlfKeys.UnknownOrGeneralError);
                default:
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),sess.i18n(XlfKeys.NotImplementedCase));
                    return null;
            }
        }


        return "";
    }
}