using System;

using System.Collections.Generic;
 
 
 public class TableRowPage : TableRowPageBase 
 {
 		public TableRowPage ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowPage ( ) : base()
		{
		}
		
		public TableRowPage ( bool IsOld, int OverallViews, bool AllowNewComments ) : base(IsOld,OverallViews,AllowNewComments)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "ID", ID);
		ParseRow(o);
  
 	 }
 
 	 public int InsertToTable() 
 	 {
 		ID = (int)MSStoredProceduresI.ci.InsertSigned(TableName, typeof(int),"ID",IsOld,OverallViews,AllowNewComments);
		return ID; 
 	 }
 
 	 public int InsertToTable2() 
 	 {
 		ID=(int)MSStoredProceduresI.ci.Insert2(TableName,"ID",typeof(int),IsOld,OverallViews,AllowNewComments);		return ID; 
 	 }
 
 	 public void InsertToTable3(int ID) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, ID,IsOld,OverallViews,AllowNewComments); 
 	 }
 
 }
 
