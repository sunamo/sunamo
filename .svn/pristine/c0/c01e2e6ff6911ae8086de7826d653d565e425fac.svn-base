using System;

using System.Collections.Generic;
 
 
 public class TableRowPlatforms : TableRowPlatformsBase 
 {
 		public TableRowPlatforms ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowPlatforms ( ) : base()
		{
		}
		
		public TableRowPlatforms ( string Name ) : base(Name)
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
 
 	 public static string GetPlatformName(byte id) 
 	 {
 		return MSStoredProceduresI.ci.SelectNameOfID(Tables.Platforms, id); 
 	 }
 
 }
 
