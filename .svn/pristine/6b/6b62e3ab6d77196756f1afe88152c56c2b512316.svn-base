using System;
public static class WebSwitchs
{
    public static int ReturnMaxAllowedItemsInLeftUpperbar(MySites ms)
    {
        switch (ms)
        {
            case MySites.GeoCaching:
                return 4;
            case MySites.BibleServer:
            case MySites.Kocicky:
            case MySites.Apps:                
            case MySites.Photos:              
            case MySites.Lyrics:               
            case MySites.Developer:              
            case MySites.Nope:             
            //case MySites.Build:             
            case MySites.ThunderBrigade:            
            case MySites.CasdMladez:          
            case MySites.Shortener:          
            case MySites.Shared:         
            case MySites.Eurostrip:
                return int.MaxValue;
            default:
                throw new Exception("Nebyl zadán maximální počet horizontálních odkazů v liště pro doménu " + ms.ToString());
        }
    }
}
