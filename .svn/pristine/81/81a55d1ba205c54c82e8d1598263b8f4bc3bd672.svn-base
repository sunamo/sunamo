using System;

using System.Collections.Generic;
 
 
 public class TableRowPageName : TableRowPageNameBase 
 {
 		public TableRowPageName ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowPageName ( ) : base()
		{
		}
		
		public TableRowPageName ( string Name ) : base(Name)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "ID", ID);
		ParseRow(o);
  
 	 }
 
 	 public int InsertToTable() 
 	 {
 		ID = (int)MSStoredProceduresI.ci.InsertSigned(TableName, typeof(int),"ID",Name);
		return ID; 
 	 }
 
 	 public int InsertToTable2() 
 	 {
 		ID=(int)MSStoredProceduresI.ci.Insert2(TableName,"ID",typeof(int),Name);		return ID; 
 	 }
 
 	 public void InsertToTable3(int ID) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, ID,Name); 
 	 }
 
 	 public static string GetPageNameName(int id) 
 	 {
 		return MSStoredProceduresI.ci.SelectNameOfID(Tables.PageName, id); 
 	 }
 
 }
 
