﻿using System;

using System.Collections.Generic;
 
 
 public class TableRowRegion : TableRowRegionBase 
 {
 		public TableRowRegion ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowRegion ( ) : base()
		{
		}
		
		public TableRowRegion ( string Name ) : base(Name)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "ID", ID);
		ParseRow(o);
  
 	 }
 
 	 public short InsertToTable() 
 	 {
 		ID = (short)MSStoredProceduresI.ci.Insert(TableName, typeof(short),"ID",Name);
		return ID; 
 	 }
 
 	 public short InsertToTable2() 
 	 {
 		ID=(short)MSStoredProceduresI.ci.Insert2(TableName,"ID",typeof(short),Name);		return ID; 
 	 }
 
 	 public void InsertToTable3(short ID) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, ID,Name); 
 	 }
 
 	 public static string GetRegionName(short id) 
 	 {
 		return MSStoredProceduresI.ci.SelectNameOfID(Tables.Region, id); 
 	 }
 
 }
 
