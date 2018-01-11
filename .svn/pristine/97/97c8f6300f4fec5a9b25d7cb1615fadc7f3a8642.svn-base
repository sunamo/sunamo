using System;

using System.Collections.Generic;
 
 
 public class TableRowFileExts : TableRowFileExtsBase 
 {
 		public TableRowFileExts ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowFileExts ( ) : base()
		{
		}
		
		public TableRowFileExts ( string Ext ) : base(Ext)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "ID", ID);
		ParseRow(o);
  
 	 }
 
 	 public short InsertToTable() 
 	 {
 		ID = (short)MSStoredProceduresI.ci.Insert(TableName, typeof(short),"ID",Ext);
		return ID; 
 	 }
 
 	 public short InsertToTable2() 
 	 {
 		ID=(short)MSStoredProceduresI.ci.Insert2(TableName,"ID",typeof(short),Ext);		return ID; 
 	 }
 
 	 public void InsertToTable3(short ID) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, ID,Ext); 
 	 }
 
 }
 
