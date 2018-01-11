using System;

using System.Collections.Generic;
 
 
 public class TableRowHostnames : TableRowHostnamesBase 
 {
 		public TableRowHostnames ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowHostnames ( ) : base()
		{
		}
		
		public TableRowHostnames ( string Name ) : base(Name)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "ID", ID);
		ParseRow(o);
  
 	 }
 
 	 public long InsertToTable() 
 	 {
 		ID = (long)MSStoredProceduresI.ci.Insert(TableName, typeof(long),"ID",Name);
		return ID; 
 	 }
 
 	 public long InsertToTable2() 
 	 {
 		ID=(long)MSStoredProceduresI.ci.Insert2(TableName,"ID",typeof(long),Name);		return ID; 
 	 }
 
 	 public void InsertToTable3(long ID) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, ID,Name); 
 	 }
 
 	 public static string GetHostnameName(long id) 
 	 {
 		return MSStoredProceduresI.ci.SelectNameOfID(Tables.Hostnames, id); 
 	 }
 
 }
 
