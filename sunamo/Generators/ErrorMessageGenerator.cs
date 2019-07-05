using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
public  class ErrorMessageGenerator
{
    StringBuilder vypis = new StringBuilder();
    StringBuilder triTecky = new StringBuilder();

    public string Visible
    {
        get
        {
            return vypis.ToString();
        }
    }

    public string Collapse
    {
        get
        {
            return triTecky.ToString();
        }
    }

    public ErrorMessageGenerator(List<string> chybneSoubory, List<FileExceptions> chyby, int i)
    {
        if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "cs")
        {
            vypis.AppendLine(" " + " " + "těchto souborech se vyskytly tyto chyby" + ": ");
        }
        else
        {
            vypis.AppendLine("In these files the following errors occurred" + ": ");
        }

        if (chybneSoubory.Count < i)
        {
            i = chybneSoubory.Count;
        }
        int y = 0;
        for (; y < i; y++)
        {
            string em = GetErrorMessage(chyby[y]);
            vypis.AppendLine(chybneSoubory[y] + AllStrings.swda + em);
        }

        string priChybe = null;
        if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "cs")
        {
            priChybe = "Pokud si myslíte že to je chyba aplikace, pošlete prosím mi email na adresu která je uvedena v dialogu O aplikaci";
        }
        else
        {
            priChybe = "If you think that this is application error, please send me an email at the address that is listed in the About app";
        }

        if (y == chybneSoubory.Count)
        {
            triTecky.AppendLine(priChybe);
        }
        else
        {
            for (; y < chybneSoubory.Count; y++)
            {
                string em = GetErrorMessage(chyby[y]);
                triTecky.AppendLine(chybneSoubory[i] + AllStrings.swda + em);
            }
            triTecky.AppendLine(priChybe);
        }
    }

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
                    return "File not found";
                case FileExceptions.UnauthorizedAccess:
                    return "Program zřejmě nemá přístup k souboru";
                case FileExceptions.General:
                    return "Neznámá nebo obecná chyba";
                default:
                    throw new Exception("Neimplementovaná větev");
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
                    return "File not found";
                case FileExceptions.UnauthorizedAccess:
                    return "The program does not have access to the file";
                case FileExceptions.General:
                    return "Unknown or general error";
                default:
                    throw new Exception("Not implemented case");
            }
        }


        return "";
    }


}
