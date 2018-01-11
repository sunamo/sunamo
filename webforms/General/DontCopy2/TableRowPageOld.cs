using System;

using System.Collections.Generic;
 
 
 public class TableRowPageOld : TableRowPageOldBase 
 {
 		public TableRowPageOld ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowPageOld ( ) : base()
		{
		}
		
		public TableRowPageOld ( byte IDWeb, int IDPageName, int IDPageArg, DateTime Day, int Views ) : base(IDWeb,IDPageName,IDPageArg,Day,Views)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "IDPage", IDPage);
		ParseRow(o);
  
 	 }
 
 	 public int InsertToTable() 
 	 {
 		IDPage = (int)MSStoredProceduresI.ci.InsertSigned(TableName, typeof(int),"IDPage",IDWeb,IDPageName,IDPageArg,Day,Views);
		return IDPage; 
 	 }
 
 	 public int InsertToTable2() 
 	 {
 		IDPage=(int)MSStoredProceduresI.ci.Insert2(TableName,"IDPage",typeof(int),IDWeb,IDPageName,IDPageArg,Day,Views);		return IDPage; 
 	 }
 
 	 public void InsertToTable3(int IDPage) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, IDPage,IDWeb,IDPageName,IDPageArg,Day,Views); 
 	 }
 
 }
 
