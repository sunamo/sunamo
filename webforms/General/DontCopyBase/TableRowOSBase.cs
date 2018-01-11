using System;

using System.Collections.Generic;
 
 
 public class TableRowOSBase 
 {
 	 public short ID = -1 ; 
 	 public string Name = "" ; 
 	public TableRowOSBase ( object[] o )
	{
	ParseRow(o);
	}
	
	public TableRowOSBase ( )
	{
	}
	
	public TableRowOSBase ( string Name )
	{
	this.Name = Name;
	}
	
	 	 protected string TableName 	 
 	 {
 	 	 get 
 	 	 {
 	 	 	 return Tables.OS; 
 	 	 }
 	 }
 
 	 	 protected void ParseRow(object[] o) 
 	 	 {
 		if (o != null)
 		{
 			ID = MSTableRowParse.GetShort(o,0);
 			Name = MSTableRowParse.GetString(o,1);
 		}  
 	 	 }
 
 }
 
