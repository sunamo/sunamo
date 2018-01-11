using System;

using System.Collections.Generic;
 
 
 public class TableRowPageNew : TableRowPageNewBase 
 {
 		public TableRowPageNew ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowPageNew ( ) : base()
		{
		}
		
		public TableRowPageNew ( byte IDTable, int IDItem, DateTime Day, int Views ) : base(IDTable,IDItem,Day,Views)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "IDPage", IDPage);
		ParseRow(o);
  
 	 }
 
 	 public int InsertToTable() 
 	 {
 		IDPage = (int)MSStoredProceduresI.ci.InsertSigned(TableName, typeof(int),"IDPage",IDTable,IDItem,Day,Views);
		return IDPage; 
 	 }
 
 	 public int InsertToTable2() 
 	 {
 		IDPage=(int)MSStoredProceduresI.ci.Insert2(TableName,"IDPage",typeof(int),IDTable,IDItem,Day,Views);		return IDPage; 
 	 }
 
 	 public void InsertToTable3(int IDPage) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, IDPage,IDTable,IDItem,Day,Views); 
 	 }
 
 }
 
