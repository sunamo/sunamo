using System;

using System.Collections.Generic;
 
 
 public class TableRowCities : TableRowCitiesBase 
 {
 		public TableRowCities ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowCities ( ) : base()
		{
		}
		
		public TableRowCities ( string Name ) : base(Name)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "ID", ID);
		ParseRow(o);
  
 	 }
 
 	 public int InsertToTable() 
 	 {
 		ID = (int)MSStoredProceduresI.ci.Insert(TableName, typeof(int),"ID",Name);
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
 
 	 public static string GetCityName(int id) 
 	 {
 		return MSStoredProceduresI.ci.SelectNameOfID(Tables.Cities, id); 
 	 }
 
 }
 
