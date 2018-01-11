using System;

using System.Collections.Generic;
 
 
 public class TableRowGo : TableRowGoBase 
 {
 		public TableRowGo ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowGo ( ) : base()
		{
		}
		
		public TableRowGo ( int IDUsers, string Code, byte Protocol, string Uri, bool Enabled, short ViewCount, DateTime CreatedDT, string Comment ) : base(IDUsers,Code,Protocol,Uri,Enabled,ViewCount,CreatedDT,Comment)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "ID", ID);
		ParseRow(o);
  
 	 }
 
 	 public int InsertToTable() 
 	 {
 		ID = (int)MSStoredProceduresI.ci.Insert(TableName, typeof(int),"ID",IDUsers,Code,Protocol,Uri,Enabled,ViewCount,CreatedDT,Comment);
		return ID; 
 	 }
 
 	 public int InsertToTable2() 
 	 {
 		ID=(int)MSStoredProceduresI.ci.Insert2(TableName,"ID",typeof(int),IDUsers,Code,Protocol,Uri,Enabled,ViewCount,CreatedDT,Comment);		return ID; 
 	 }
 
 	 public void InsertToTable3(int ID) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, ID,IDUsers,Code,Protocol,Uri,Enabled,ViewCount,CreatedDT,Comment); 
 	 }
 
 }
 
