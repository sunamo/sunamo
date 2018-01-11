using System;

using System.Collections.Generic;
 
 
 public class TableRowUsersActivates : TableRowUsersActivatesBase 
 {
 		public TableRowUsersActivates ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowUsersActivates ( ) : base()
		{
		}
		
		public TableRowUsersActivates ( string Code, string Login, string Olseh, string Email, DateTime DeleteOn, string SecQue, string SecAns, bool Sex, DateTime DateBorn, byte MailSettings, byte IDState, short IDRegion, byte IDDistrict, int IDCity ) : base(Code,Login,Olseh,Email,DeleteOn,SecQue,SecAns,Sex,DateBorn,MailSettings,IDState,IDRegion,IDDistrict,IDCity)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "ID", ID);
		ParseRow(o);
  
 	 }
 
 	 public int InsertToTable() 
 	 {
 		ID = (int)MSStoredProceduresI.ci.Insert(TableName, typeof(int),"ID",Code,Login,Olseh,Email,DeleteOn,SecQue,SecAns,Sex,DateBorn,MailSettings,IDState,IDRegion,IDDistrict,IDCity);
		return ID; 
 	 }
 
 	 public int InsertToTable2() 
 	 {
 		ID=(int)MSStoredProceduresI.ci.Insert2(TableName,"ID",typeof(int),Code,Login,Olseh,Email,DeleteOn,SecQue,SecAns,Sex,DateBorn,MailSettings,IDState,IDRegion,IDDistrict,IDCity);		return ID; 
 	 }
 
 	 public void InsertToTable3(int ID) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, ID,Code,Login,Olseh,Email,DeleteOn,SecQue,SecAns,Sex,DateBorn,MailSettings,IDState,IDRegion,IDDistrict,IDCity); 
 	 }
 
 }
 
