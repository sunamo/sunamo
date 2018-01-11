using System;

using System.Collections.Generic;
 
 
 public class TableRowLocation : TableRowLocationBase 
 {
 		public TableRowLocation ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowLocation ( ) : base()
		{
		}
		
		public TableRowLocation ( byte IDState, short IDRegion, byte SerieDistrict, string District ) : base(IDState,IDRegion,SerieDistrict,District)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "ID", ID);
		ParseRow(o);
  
 	 }
 
 	 public short InsertToTable() 
 	 {
 		ID = (short)MSStoredProceduresI.ci.Insert(TableName, typeof(short),"ID",IDState,IDRegion,SerieDistrict,District);
		return ID; 
 	 }
 
 	 public short InsertToTable2() 
 	 {
 		ID=(short)MSStoredProceduresI.ci.Insert2(TableName,"ID",typeof(short),IDState,IDRegion,SerieDistrict,District);		return ID; 
 	 }
 
 	 public void InsertToTable3(short ID) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, ID,IDState,IDRegion,SerieDistrict,District); 
 	 }
 
 }
 
