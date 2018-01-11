using System;

using System.Collections.Generic;
 
 
 public class TableRowFavorites : TableRowFavoritesBase 
 {
 		public TableRowFavorites ( object[] o )
		{
		ParseRow(o);
		}
		
		public TableRowFavorites ( ) : base()
		{
		}
		
		public TableRowFavorites ( int IDUsers ) : base(IDUsers)
		{
		}
		
		 	 public void SelectInTable() 
 	 {
 		object[] o = MSStoredProceduresI.ci.SelectOneRowForTableRow(TableName, "IDPages", IDPages);
		ParseRow(o);
  
 	 }
 
 	 public int InsertToTable() 
 	 {
 		IDPages = (int)MSStoredProceduresI.ci.Insert(TableName, typeof(int),"IDPages",IDUsers);
		return IDPages; 
 	 }
 
 	 public int InsertToTable2() 
 	 {
 		IDPages=(int)MSStoredProceduresI.ci.Insert2(TableName,"IDPages",typeof(int),IDUsers);		return IDPages; 
 	 }
 
 	 public void InsertToTable3(int IDPages) 
 	 {
 		MSStoredProceduresI.ci.Insert4(TableName, IDPages,IDUsers); 
 	 }
 
 }
 
