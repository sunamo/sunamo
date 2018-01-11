using System;

using System.Collections.Generic;
 
 
 public class TableRowPageNewBase 
 {
 	 public int IDPage = -1 ; 
 	 public byte IDTable = 0 ; 
 	 public int IDItem = -1 ; 
 	 public DateTime Day = MSStoredProceduresI.DateTimeMinVal ; 
 	 public int Views = -1 ; 
 	public TableRowPageNewBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowPageNewBase ( )
	{
	}
	
	public TableRowPageNewBase ( byte IDTable, int IDItem, DateTime Day, int Views )
	{
	this.IDTable = IDTable;
	this.IDItem = IDItem;
	this.Day = Day;
	this.Views = Views;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.PageNew; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			IDPage = MSTableRowParse.GetInt(o,0);
 			IDTable = MSTableRowParse.GetByte(o,1);
 			IDItem = MSTableRowParse.GetInt(o,2);
 			Day = MSTableRowParse.GetDateTime(o,3);
 			Views = MSTableRowParse.GetInt(o,4);
 		}  
 	 	 }
 
 }
 
