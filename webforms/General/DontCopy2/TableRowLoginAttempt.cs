using System;

using System.Collections.Generic;
 
 
 public class TableRowLoginAttempt : TableRowLoginAttemptBase 
 {
 		public TableRowLoginAttempt ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowLoginAttempt ( ) : base()
		{
		}
		
		public TableRowLoginAttempt ( string Login, DateTime DT, byte Count ) : base(Login,DT,Count)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "ID", ID);
		ParseRow(o);
  
 	 }
 
 	 public int InsertToTable() 
 	 {
 		ID = (int)MSStoredProceduresI.ci.Insert(TableName, typeof(int),"ID",Login,DT,Count);
		return ID; 
 	 }
 
 	 public int InsertToTable2() 
 	 {
 		ID=(int)MSStoredProceduresI.ci.Insert2(TableName,"ID",typeof(int),Login,DT,Count);		return ID; 
 	 }
 
 	 public void InsertToTable3(int ID) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, ID,Login,DT,Count); 
 	 }
 
 }
 
