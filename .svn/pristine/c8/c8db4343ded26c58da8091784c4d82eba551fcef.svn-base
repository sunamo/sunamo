using System;

using System.Collections.Generic;
 
 
 public class TableRowPageOldBase 
 {
 	 public int IDPage = -1 ; 
 	 public byte IDWeb = 0 ; 
 	 public int IDPageName = -1 ; 
 	 public int IDPageArg = -1 ; 
 	 public DateTime Day = MSStoredProceduresI.DateTimeMinVal ; 
 	 public int Views = -1 ; 
 	public TableRowPageOldBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowPageOldBase ( )
	{
	}
	
	public TableRowPageOldBase ( byte IDWeb, int IDPageName, int IDPageArg, DateTime Day, int Views )
	{
	this.IDWeb = IDWeb;
	this.IDPageName = IDPageName;
	this.IDPageArg = IDPageArg;
	this.Day = Day;
	this.Views = Views;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.PageOld; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			IDPage = MSTableRowParse.GetInt(o,0);
 			IDWeb = MSTableRowParse.GetByte(o,1);
 			IDPageName = MSTableRowParse.GetInt(o,2);
 			IDPageArg = MSTableRowParse.GetInt(o,3);
 			Day = MSTableRowParse.GetDateTime(o,4);
 			Views = MSTableRowParse.GetInt(o,5);
 		}  
 	 	 }
 
 }
 
