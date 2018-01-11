using System;

using System.Collections.Generic;
 
 
 public class TableRowFileExtsBase 
 {
 	 public short ID = -1 ; 
 	 public string Ext = "" ; 
 	public TableRowFileExtsBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowFileExtsBase ( )
	{
	}
	
	public TableRowFileExtsBase ( string Ext )
	{
	this.Ext = Ext;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.FileExts; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			ID = MSTableRowParse.GetShort(o,0);
 			Ext = MSTableRowParse.GetString(o,1);
 		}  
 	 	 }
 
 }
 
