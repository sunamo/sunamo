using System;

using System.Collections.Generic;
 
 
 public class TableRowState : TableRowStateBase 
 {
 		public TableRowState ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowState ( ) : base()
		{
		}
		
		public TableRowState ( string Name ) : base(Name)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "ID", ID);
		ParseRow(o);
  
 	 }
 
 	 public byte InsertToTable() 
 	 {
 		ID = (byte)MSStoredProceduresI.ci.Insert(TableName, typeof(byte),"ID",Name);
		return ID; 
 	 }
 
 	 public byte InsertToTable2() 
 	 {
 		ID=(byte)MSStoredProceduresI.ci.Insert2(TableName,"ID",typeof(byte),Name);		return ID; 
 	 }
 
 	 public void InsertToTable3(byte ID) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, ID,Name); 
 	 }
 
 	 public static string GetStateName(byte id) 
 	 {
 		return MSStoredProceduresI.ci.SelectNameOfID(Tables.State, id); 
 	 }
 
 }
 
