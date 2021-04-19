using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StoreParsedApp
{
    public string name = null;
    public string uri = null;

    public const string Name = "Name";
    public const string Category = "Category";
    public const string Uri = "Uri";
    public const string CountOfRatings = "Count of ratings";
    public const string AverageRating = "Average rating";
    public const string OverallUsersInThousandsK = "Overall users in thousands (k)";
    public const string Price = "Price";
    public const string InAppPurchases = "In-app purchases";
    public const string LastUpdated = "Last updated";
    public const string RunTest = "Run test";
    public const string FinalOfficialWeb = "Final - Official Web";
    public const string FurtherTest = "Further test";
    public const string PriceForYearSubs = "Price for year subs";
    public const string PriceForLifelongSubs = "Price for lifelong subs";

    public string GetValueForRow(string fc)
    {
        switch (fc)
        {
            case Name:
                return name;
            case Uri:
                return uri;
            default:
                return string.Empty;
        }
    }
}