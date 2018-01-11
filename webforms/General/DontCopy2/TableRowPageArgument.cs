using System;

using System.Collections.Generic;
 
 
 public class TableRowPageArgument : TableRowPageArgumentBase 
 {
 		public TableRowPageArgument ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowPageArgument ( ) : base()
		{
		}
		
		public TableRowPageArgument ( string Arg ) : base(Arg)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "ID", ID);
		ParseRow(o);
  
 	 }
 
 	 public int InsertToTable() 
 	 {
 		ID = (int)MSStoredProceduresI.ci.InsertSigned(TableName, typeof(int),"ID",Arg);
		return ID; 
 	 }
 
 	 public int InsertToTable2() 
 	 {
 		ID=(int)MSStoredProceduresI.ci.Insert2(TableName,"ID",typeof(int),Arg);		return ID; 
 	 }
 
 	 public void InsertToTable3(int ID) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, ID,Arg); 
 	 }
 
 }
 
