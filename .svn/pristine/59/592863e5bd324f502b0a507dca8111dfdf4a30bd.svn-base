using System;

using System.Collections.Generic;
 
 
 public class TableRowCommentSubscribe : TableRowCommentSubscribeBase 
 {
 		public TableRowCommentSubscribe ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowCommentSubscribe ( ) : base()
		{
		}
		
		public TableRowCommentSubscribe ( int IDUser, byte Serie ) : base(IDUser,Serie)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "IDPage", IDPage);
		ParseRow(o);
  
 	 }
 
 	 public int InsertToTable() 
 	 {
 		IDPage = (int)MSStoredProceduresI.ci.Insert(TableName, typeof(int),"IDPage",IDUser,Serie);
		return IDPage; 
 	 }
 
 	 public int InsertToTable2() 
 	 {
 		IDPage=(int)MSStoredProceduresI.ci.Insert2(TableName,"IDPage",typeof(int),IDUser,Serie);		return IDPage; 
 	 }
 
 	 public void InsertToTable3(int IDPage) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, IDPage,IDUser,Serie); 
 	 }
 
 }
 
