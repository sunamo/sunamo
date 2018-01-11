using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient; 
 
 public class TableRowCzechNameDays : TableRowCzechNameDaysBase 
 {
 		public TableRowCzechNameDays ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowCzechNameDays ( ) : base()
		{
		}
		
		public TableRowCzechNameDays ( string Name, byte Day, byte Month ) : base(Name,Day,Month)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "ID", ID);
		ParseRow(o);
  
 	 }
 
 	 public short InsertToTable() 
 	 {
 		ID = (short)MSStoredProceduresI.ci.Insert(TableName, typeof(short),"ID",Name,Day,Month);
		return ID; 
 	 }
 
 	 public short InsertToTable2() 
 	 {
 		ID=(short)MSStoredProceduresI.ci.Insert2(TableName,"ID",typeof(short),Name,Day,Month);		return ID; 
 	 }
 
 	 public void InsertToTable3(short ID) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, ID,Name,Day,Month); 
 	 }
 
 	 public static string GetCzechNameDayName(short id) 
 	 {
 		return MSStoredProceduresI.ci.SelectNameOfID(Tables.CzechNameDays, id); 
 	 }
 
 }
 
