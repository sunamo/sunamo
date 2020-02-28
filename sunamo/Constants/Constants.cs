using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Constants
{
    // TODO: Distribute to other because public class name is the same as namespace
    public const string AfterCloseNonCompletedSettingsWizard = "Pr\u016Fvodce nastaven\u00EDm nebyl dokon\u010Den. P\u0159ejete si jej p\u0159esto zav\u0159\u00EDt" + "?";
    public const int OneMB = 1048576;
    /// <summary>
    /// Připočítává 1 pro snadnější porovnání
    /// </summary>
    public const int OneMB1 = 1048577;


    #region MyRegion
    public const string sirkaNazev = "45%";
    // Bude obsahovat informaci o počtu prokliků
    public const string sirkaVoteCount = "10%";
    // Bude obsahovat Buttony vymazat, editovat, změnit barvu
    public const string sirkaButtony = "35%";
    /// <summary>
    /// -Stejné pro všechny weby
    /// </summary>
    public static int MaxLengthColumnWordInTablesWords = 60;
    public static object dialogSize;
    #endregion
}

