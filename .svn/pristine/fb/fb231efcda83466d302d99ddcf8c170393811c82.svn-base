using System;

using System.Collections.Generic;
 
 
 public class TableRowHostnamesBase 
 {
 	 public long ID = -1 ; 
 	 public string Name = "" ; 
 	public TableRowHostnamesBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowHostnamesBase ( )
	{
	}
	
	public TableRowHostnamesBase ( string Name )
	{
	this.Name = Name;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.Hostnames; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			ID = MSTableRowParse.GetLong(o,0);
 			Name = MSTableRowParse.GetString(o,1);
 		}  
 	 	 }
 
 }
 
