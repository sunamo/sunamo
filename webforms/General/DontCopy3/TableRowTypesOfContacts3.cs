using System.Collections.Generic;

public static class TableRowTypesOfContacts3
{
    public static string[] types = new string[] {
        "Mail", //0
	"ICQ", //1
	"Skype", //2
	"Jabber/XMPP", //3
	"Mobil O2", //4
	"Mobil T-Mobile", //5
	"Mobil Vodafone" //6
        };

    public static void InsertIntoDB(Dictionary<string, MSColumnsDB> generalLayerS)
    {
         MSStoredProceduresI.ci.DropAndCreateTable(Tables.TypesOfContacts, generalLayerS);
        foreach (var item in types)
        {
            TableRowTypesOfContacts trtc = new TableRowTypesOfContacts(item);
            trtc.InsertToTable();
        }
    }
}
