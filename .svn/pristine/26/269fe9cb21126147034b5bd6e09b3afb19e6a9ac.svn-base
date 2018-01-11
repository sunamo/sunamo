using System;

using System.Collections.Generic;
 
 
 public class TableRowIPAddress : TableRowIPAddressBase 
 {
 		public TableRowIPAddress ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowIPAddress ( ) : base()
		{
		}
		
		public TableRowIPAddress ( byte IP1, byte IP2, byte IP3, byte IP4, bool IsBlocked ) : base(IP1,IP2,IP3,IP4,IsBlocked)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "ID", ID);
		ParseRow(o);
  
 	 }
 
 	 public int InsertToTable() 
 	 {
 		ID = (int)MSStoredProceduresI.ci.InsertSigned(TableName, typeof(int),"ID",IP1,IP2,IP3,IP4,IsBlocked);
		return ID; 
 	 }
 
 	 public int InsertToTable2() 
 	 {
 		ID=(int)MSStoredProceduresI.ci.Insert2(TableName,"ID",typeof(int),IP1,IP2,IP3,IP4,IsBlocked);		return ID; 
 	 }
 
 	 public void InsertToTable3(int ID) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, ID,IP1,IP2,IP3,IP4,IsBlocked); 
 	 }
 
 }
 
