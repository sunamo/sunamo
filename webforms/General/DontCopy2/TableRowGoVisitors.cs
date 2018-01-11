using System;

using System.Collections.Generic;
 
 
 public class TableRowGoVisitors : TableRowGoVisitorsBase 
 {
 		public TableRowGoVisitors ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowGoVisitors ( ) : base()
		{
		}
		
		public TableRowGoVisitors ( DateTime DT, int IDGo, byte IDBrowser, long IDHostName, byte IDPlatform, byte IDLang, byte BrowserVersion ) : base(DT,IDGo,IDBrowser,IDHostName,IDPlatform,IDLang,BrowserVersion)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "ID", ID);
		ParseRow(o);
  
 	 }
 
 	 public long InsertToTable() 
 	 {
 		ID = (long)MSStoredProceduresI.ci.Insert(TableName, typeof(long),"ID",DT,IDGo,IDBrowser,IDHostName,IDPlatform,IDLang,BrowserVersion);
		return ID; 
 	 }
 
 	 public long InsertToTable2() 
 	 {
 		ID=(long)MSStoredProceduresI.ci.Insert2(TableName,"ID",typeof(long),DT,IDGo,IDBrowser,IDHostName,IDPlatform,IDLang,BrowserVersion);		return ID; 
 	 }
 
 	 public void InsertToTable3(long ID) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, ID,DT,IDGo,IDBrowser,IDHostName,IDPlatform,IDLang,BrowserVersion); 
 	 }
 
 }
 
