using System;

using System.Collections.Generic;
 
 
 public class TableRowUsers : TableRowUsersBase 
 {
 		public TableRowUsers ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowUsers ( ) : base()
		{
		}
		
		public TableRowUsers ( string SessionID, string Login, string Email, DateTime LastSeen, string SecQue, string SecAns, bool Sex, DateTime DateBorn, byte MailSettings, byte IDState, short IDRegion, byte IDDistrict, int IDCity, byte Status, bool BlockNewComment, string FacebookNick, string TwitterNick, string GoogleNick, short WidthUserPage, string HtmlAbout ) : base(SessionID,Login,Email,LastSeen,SecQue,SecAns,Sex,DateBorn,MailSettings,IDState,IDRegion,IDDistrict,IDCity,Status,BlockNewComment,FacebookNick,TwitterNick,GoogleNick,WidthUserPage,HtmlAbout)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "ID", ID);
		ParseRow(o);
  
 	 }
 
 	 public int InsertToTable() 
 	 {
 		ID = (int)MSStoredProceduresI.ci.Insert(TableName, typeof(int),"ID",SessionID,Login,Email,LastSeen,SecQue,SecAns,Sex,DateBorn,MailSettings,IDState,IDRegion,IDDistrict,IDCity,Status,BlockNewComment,FacebookNick,TwitterNick,GoogleNick,WidthUserPage,HtmlAbout);
		return ID; 
 	 }
 
 	 public int InsertToTable2() 
 	 {
 		ID=(int)MSStoredProceduresI.ci.Insert2(TableName,"ID",typeof(int),SessionID,Login,Email,LastSeen,SecQue,SecAns,Sex,DateBorn,MailSettings,IDState,IDRegion,IDDistrict,IDCity,Status,BlockNewComment,FacebookNick,TwitterNick,GoogleNick,WidthUserPage,HtmlAbout);		return ID; 
 	 }
 
 	 public void InsertToTable3(int ID) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, ID,SessionID,Login,Email,LastSeen,SecQue,SecAns,Sex,DateBorn,MailSettings,IDState,IDRegion,IDDistrict,IDCity,Status,BlockNewComment,FacebookNick,TwitterNick,GoogleNick,WidthUserPage,HtmlAbout); 
 	 }
 
 }
 
