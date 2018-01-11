using System;

using System.Collections.Generic;
 
 
 public class TableRowColors : TableRowColorsBase 
 {
 		public TableRowColors ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowColors ( ) : base()
		{
		}
		
		public TableRowColors ( string Name, byte R, byte G, byte B ) : base(Name,R,G,B)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "ID", ID);
		ParseRow(o);
  
 	 }
 
 	 public short InsertToTable() 
 	 {
 		ID = (short)MSStoredProceduresI.ci.Insert(TableName, typeof(short),"ID",Name,R,G,B);
		return ID; 
 	 }
 
 	 public short InsertToTable2() 
 	 {
 		ID=(short)MSStoredProceduresI.ci.Insert2(TableName,"ID",typeof(short),Name,R,G,B);		return ID; 
 	 }
 
 	 public void InsertToTable3(short ID) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, ID,Name,R,G,B); 
 	 }
 
 	 public static string GetColorName(short id) 
 	 {
 		return MSStoredProceduresI.ci.SelectNameOfID(Tables.Colors, id); 
 	 }
 
 }
 
