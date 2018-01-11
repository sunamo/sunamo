using System;

using System.Collections.Generic;
 
 
 public class TableRowUsersReactivates : TableRowUsersReactivatesBase 
 {
 		public TableRowUsersReactivates ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowUsersReactivates ( ) : base()
		{
		}
		
		public TableRowUsersReactivates ( string Code, string Email, DateTime DateChanged, byte ChangedTimes ) : base(Code,Email,DateChanged,ChangedTimes)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "IDUsers", IDUsers);
		ParseRow(o);
  
 	 }
 
 	 public int InsertToTable() 
 	 {
 		IDUsers = (int)MSStoredProceduresI.ci.Insert(TableName, typeof(int),"IDUsers",Code,Email,DateChanged,ChangedTimes);
		return IDUsers; 
 	 }
 
 	 public int InsertToTable2() 
 	 {
 		IDUsers=(int)MSStoredProceduresI.ci.Insert2(TableName,"IDUsers",typeof(int),Code,Email,DateChanged,ChangedTimes);		return IDUsers; 
 	 }
 
 	 public void InsertToTable3(int IDUsers) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, IDUsers,Code,Email,DateChanged,ChangedTimes); 
 	 }
 
 }
 
